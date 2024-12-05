using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VehiclePhysics;
using VehiclePhysics.UI;

public enum PlayerStateMovement { 
    WALK,
    CAR,
    INVENTORY,
    PHONE,
    BELANJA
}

public class PlayerToVehicle : MonoBehaviour
{
    static PlayerToVehicle _instance;
    [SerializeField] GameObject Vehicle;
    [SerializeField] Transform CameraAnchorWalk;
    [SerializeField] Transform CameraAnchorCar;
    [SerializeField] Transform GetOutCarPos;

    public GameObject Hape;
    public GameObject BahariUI;

    public PlayerStateMovement playerStates;
    public PlayerStateMovement before;

    VPVehicleController vehcon;
    VPStandardInput vehicleController;
    VPVisualEffects visualEffectsVeh;
    VPTelemetry telemetryVeh;
    VehicleBase VehicleBase;
    [SerializeField] int key;
    bool masukMobil;

    public GameObject minimapCamera;

    SUPERCharacterAIO superCharCont;
    CapsuleCollider CharacterColl;
    Rigidbody playerRB;
    public NgecekInventory invents;

    // Camera Handler
    [SerializeField] Camera MainCams;

    [SerializeField] Image Hunger;
    [SerializeField] Image Thrist;

    private void Awake()
    {
        _instance = this;
    }

    public static PlayerToVehicle Instance
    {
        get
        {
            return _instance;
        }
    }

    protected void HungerThirstUIHandler()
    {
        Hunger.fillAmount = superCharCont.currentSurvivalStats.Hunger / 100f;
        Thrist.fillAmount = superCharCont.currentSurvivalStats.Hydration / 100f;

    }

    public void Makan(float val)
    {
        superCharCont.currentSurvivalStats.Hunger += val;
    }

    public void Minum(float val)
    {
        superCharCont.currentSurvivalStats.Hydration += val;
    }

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
        minimapCamera.transform.position = MainCams.transform.position + (Vector3.up * 600);
        switch (playerStates)
        {
            case PlayerStateMovement.WALK:
                {
                    if (Input.GetKey(KeyCode.P))
                    {
                        BukaHP();
                    }
                    superCharCont.enabled = true;
                    superCharCont.enableCameraControl = true;
                    MainCams.transform.position = CameraAnchorWalk.position;
                    superCharCont.EnambleCameraMovement = true;
                    superCharCont.enableMovementControl = true;
                    masukMobil = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
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
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
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
                    if (Input.GetKeyDown(KeyCode.Escape))
                        invents.BukaInventory();
                    superCharCont.EnambleCameraMovement = false;
                    superCharCont.enableMovementControl = false;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    break;
                }
            case PlayerStateMovement.PHONE:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        NutupHP();
                    }
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    superCharCont.enabled = false;
                    superCharCont.enableCameraControl = false;
                    superCharCont.EnambleCameraMovement = false;
                    superCharCont.enableMovementControl = false;
                    masukMobil = false;
                    break;
                }
            case PlayerStateMovement.BELANJA:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        KeluarBelanja();
                    }
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    superCharCont.enabled = false;
                    superCharCont.enableCameraControl = false;
                    superCharCont.EnambleCameraMovement = false;
                    superCharCont.enableMovementControl = false;
                    masukMobil = false;
                    break;
                }
        }
        HungerThirstUIHandler();

        //superCharCont.currentSurvivalStats.Hunger
        //superCharCont.currentSurvivalStats.Hydration
        //superCharCont.Stamina
    }

    protected void BukaHP()
    {
        if (Hape.activeInHierarchy == true) return;
        before = playerStates;
        playerStates = PlayerStateMovement.PHONE;
        Hape.SetActive(true);
    }

    protected void NutupHP()
    {
        playerStates = before;
        Hape.SetActive(false);
    }

    protected void KeluarBelanja()
    {
        playerStates = PlayerStateMovement.WALK;
        BahariUI.SetActive(false);
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
