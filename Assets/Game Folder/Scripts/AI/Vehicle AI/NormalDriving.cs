using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class NormalDriving : AIBase
{
    // PROPERTIES
    [SerializeField] int safeDistance, spacing;

    // WAYPOINT REFRENCES
    [SerializeField] Waypoint nextWaypoint;

    // ON INITIALIZTION
    private void Start()
    {
    }

    private void Update()
    {

    }
}
