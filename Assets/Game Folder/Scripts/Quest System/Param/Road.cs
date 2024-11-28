using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] string RoadName;
    [SerializeField] List<Building> buildings;
    
    void Start()
    {
        RoadManagers.Instance.RegisterRoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterBuildingRoad(Building build)
    {
        buildings.Add(build);
        build.SetBuildingNumber(buildings.Count);
    }

    public string Alamat()
    {
        return RoadName;
    }
}
