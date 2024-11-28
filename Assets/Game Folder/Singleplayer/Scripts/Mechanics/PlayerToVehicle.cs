using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;

public enum PlayerStateMovement { 
    WALK,
    CAR
}

public class PlayerToVehicle : MonoBehaviour
{
    [SerializeField] GameObject Vehicle;
    [SerializeField] Transform CameraAnchorWalk;
    [SerializeField] Transform CameraAnchorCar;
    [SerializeField] Transform GetOutCarPos;

    [SerializeField] PlayerStateMovement playerStates;

    VPStandardInput vehicleController;
    VPVisualEffects visualEffectsVeh;
    VPTelemetry telemetryVeh;


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
                    MainCams.transform.position = CameraAnchorWalk.position;
                    //MainCams.transform.rotation = CameraAnchorWalk.rotation;
                    break;
                }
            case PlayerStateMovement.CAR:
                {
                    gameObject.transform.position = GetOutCarPos.position;
                    MainCams.transform.position = CameraAnchorCar.position;
                    MainCams.transform.rotation = CameraAnchorCar.rotation;
                    Debug.Log(telemetryVeh.vehicle.localAcceleration.magnitude.ToString());
                    if (Input.GetKeyDown(KeyCode.E) && telemetryVeh.vehicle.localAcceleration.magnitude < 1)
                        GetOutCar();
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
        superCharCont.enabled = false;
        playerRB.isKinematic = true;
        CharacterColl.enabled = false;
        playerStates = PlayerStateMovement.CAR;
    }

    public void GetOutCar()
    {
        Debug.Log("keluar Mobil");
        vehicleController.enabled = false;
        visualEffectsVeh.enabled = false;
        superCharCont.enabled = true;
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
