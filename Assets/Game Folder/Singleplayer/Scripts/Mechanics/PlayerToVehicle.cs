using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;
using VehiclePhysics.UI;

public enum PlayerStateMovement { 
    WALK,
    CAR,
    INVENTORY
}

public class PlayerToVehicle : MonoBehaviour
{
    [SerializeField] GameObject Vehicle;
    [SerializeField] Transform CameraAnchorWalk;
    [SerializeField] Transform CameraAnchorCar;
    [SerializeField] Transform GetOutCarPos;

    public PlayerStateMovement playerStates;

    VPVehicleController vehcon;
    VPStandardInput vehicleController;
    VPVisualEffects visualEffectsVeh;
    VPTelemetry telemetryVeh;
    VehicleBase VehicleBase;
    [SerializeField] int key;
    bool masukMobil;


    SUPERCharacterAIO superCharCont;
    CapsuleCollider CharacterColl;
    Rigidbody playerRB;

    // Camera Handler
    [SerializeField] Camera MainCams;

    void Start()
    {
        Debug.Log("Vehicle Component " + Vehicle.GetComponentCount());
        vehicleController = Vehicle.GetComponent<VPStandardInput>();
        visualEffectsVeh = Vehicle.GetComponent<VPVisualEffects>();
        telemetryVeh = Vehicle.GetComponent<VPTelemetry>();
        vehcon = Vehicle.GetComponent<VPVehicleController>();
        VehicleBase = Vehicle.GetComponent<VehicleBase>();
        if (vehicleController)
        {
            vehicleController.enabled = false;
            visualEffectsVeh.enabled = false;
        }
        superCharCont = GetComponent<SUPERCharacterAIO>();
        CharacterColl = GetComponent<CapsuleCollider>();
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerStates)
        {
            case PlayerStateMovement.WALK:
                {
                    superCharCont.enabled = true;
                    superCharCont.enableCameraControl = true;
                    MainCams.transform.position = CameraAnchorWalk.position;
                    superCharCont.EnambleCameraMovement = true;
                    superCharCont.enableMovementControl = true;
                    masukMobil = false;
                    //MainCams.transform.rotation = CameraAnchorWalk.rotation;
                    break;
                }
            case PlayerStateMovement.CAR:
                {

                    //superCharCont.enabled = false;
                    if (Input.GetKey(KeyCode.LeftAlt))
                    {
                        superCharCont.enableCameraControl = true;
                    }
                    else
                    {
                        transform.rotation = CameraAnchorCar.rotation;
                        superCharCont.enableCameraControl = false;
                        MainCams.transform.rotation = CameraAnchorCar.rotation;
                    }
                    superCharCont.enableMovementControl = false;
                    gameObject.transform.position = GetOutCarPos.position;
                    MainCams.transform.position = CameraAnchorCar.position;
                    //MainCams.transform.rotation = CameraAnchorCar.rotation;
                    key = VehicleBase.data.Get(Channel.Input, InputData.Key);
                    //Debug.Log(telemetryVeh.vehicle.localAcceleration.magnitude.ToString());

                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        VehicleBase.data.Set(Channel.Input, InputData.Key, -1);
                    }
                    if (Input.GetKeyDown(KeyCode.E) && telemetryVeh.vehicle.localAcceleration.magnitude < 1 && masukMobil == true)
                        GetOutCar();
                    masukMobil = true;
                    break;
                }
            case PlayerStateMovement.INVENTORY:
                {
                    superCharCont.EnambleCameraMovement = false;
                    superCharCont.enableMovementControl = false;
                    break;
                }
        }
    }

    protected void DisableVehicleInput()
    {
        if (vehicleController == null) return;

        vehicleController.enabled = false;
    }

    public void GetInCar()
    {
        Debug.Log("masuk Mobil");
        vehicleController.enabled = true;
        visualEffectsVeh.enabled= true;
        //playerRB.isKinematic = true;
        CharacterColl.enabled = false;
        playerStates = PlayerStateMovement.CAR;
        
    }

    public void GetOutCar()
    {
        Debug.Log("keluar Mobil");
        vehicleController.enabled = false;
        visualEffectsVeh.enabled = false;
        playerRB.isKinematic = false;
        CharacterColl.enabled = true;
        playerStates = PlayerStateMovement.WALK;
        
    }

    public void evaluateOutInCar()
    {
        switch (playerStates)
        {
            case PlayerStateMovement.WALK:
                {
                    GetInCar();
                    break;
                }
            case PlayerStateMovement.CAR:
                {
                    GetOutCar();
                    break;
                }
        }
    }
}
