using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NgecekInventory : MonoBehaviour
{
    public GameObject UISet;
    [SerializeField] GameObject contentPreset;
    List<GameObject> inventory;
    public PlayerToVehicle playerRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BukaInventory()
    {
        if (UISet.activeSelf == true)
        {
            UISet.SetActive(false);
            playerRef.playerStates = PlayerStateMovement.WALK;
        }
        else
        {
            UISet.SetActive(true);
            playerRef.playerStates = PlayerStateMovement.INVENTORY;
        }
    }
}
