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
    [SerializeField] Transform spawnPoint;

    [Header("Jalanan")]
    [SerializeField] Road Jalanan;
    [SerializeField] int BuildingNumber;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
}

