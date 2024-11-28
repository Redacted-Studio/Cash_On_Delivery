using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadManagers : MonoBehaviour
{
    [SerializeField] List<Road> roads;

    public static RoadManagers _Instance;

    private void Awake()
    {
        _Instance = this;
    }

    public static RoadManagers Instance
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

    public void RegisterRoad(Road road)
    {
        roads.Add(road);
    }
}