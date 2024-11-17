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

    [SerializeField] PlayerStateMovement playerStates;

    VPStandardInput vehicleController;
    VPVisualEffects visualEffectsVeh;


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
                    break;
                }
            case PlayerStateMovement.CAR:
                {
                    MainCams.transform.position = CameraAnchorCar.position;
                    MainCams.transform.rotation = CameraAnchorCar.rotation;
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

    }
}
