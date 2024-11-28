using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager _Instance;

    [SerializeField] List<Building> buildingList;

    private void Awake()
    {
        _Instance = this;
    }

    public static BuildingManager Instance
    {
        get
        {
            return _Instance;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Building InsertBuilding(Building build)
    {
        buildingList.Add(build);
        return build;
    }

    public Building GetRandomBuilding()
    {
        return buildingList[Random.Range(0, buildingList.Count - 1)];
    }
}
