using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    House,
    Office,
    Restaurant,
    PublicServices
}

public class Building : MonoBehaviour
{
    [Header("Bangunan")]
    [SerializeField] BuildingType buildingType;
    [SerializeField] string BuildingName;
    Transform spawnPoint;
    [SerializeField] NPC owner;
    public Transform tempatNerimaPaket;

    [Header("Jalanan")]
    [SerializeField] Road Jalanan;
    [SerializeField] int BuildingNumber;
    

    void Start()
    {
        Jalanan.RegisterBuildingRoad(this);
        SetBuildingName();
        BuildingManager.Instance.InsertBuilding(this);
    }



    protected void SetBuildingName()
    {

        BuildingName = owner.Nama + " " + buildingType.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("PUNYA " + owner.Nama);
        if (owner.OwnBuilding == false)
        {
            //Debug.Log("TANPA PEMILIK");
            owner = NPCManager.Instance.GetHomelessNPC();
            owner.OwnBuilding = true;
            owner.Rumah = this;
        }

    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void SetBuildingNumber(int number)
    {
        BuildingNumber = number;
    }

    public int GetBuildingNumber()
    {
        return BuildingNumber;
    }

    public string GetAlamat()
    {
        return Jalanan.Alamat();
    }

    public NPC GetOwner()
    {
        return owner;
    }
}

