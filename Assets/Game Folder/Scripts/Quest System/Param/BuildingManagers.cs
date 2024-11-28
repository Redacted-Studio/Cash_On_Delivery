using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingParam : MonoBehaviour
{
    public static BuildingParam _Instance;

    [SerializeField] List<Building> buildingList;

    private void Awake()
    {
        _Instance = this;
    }

    public static BuildingParam Instance
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
}
