using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NgecekInventory : MonoBehaviour
{
    public GameObject UISet;
    [SerializeField] VehicleInventory Inventory;
    [SerializeField] GameObject contentPreset;
    public List<InventoryContent> InventoryContent;
    List<GameObject> inventory;
    public PlayerToVehicle playerRef;
    // Start is called before the first frame update
    void Start()
    {
        Inventory.setInvs(this);
    }

    public void BukaInventory()
    {
        if (UISet.activeSelf == true)
        {
            UISet.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerRef.playerStates = PlayerStateMovement.WALK;
        }
        else
        {
            UISet.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            playerRef.playerStates = PlayerStateMovement.INVENTORY;
        }
    }
}
