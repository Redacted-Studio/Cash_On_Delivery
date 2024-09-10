using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.AI.Navigation.Editor;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class NormalDriving : AIBase
{
    // AI TYPE ON VEHICLE
    NavMeshAgent agent;
    NavMeshPath path;

    // ON INITIALIZTION
    private void Start()
    {
        agent = base.getNavmeshAgent();
        path = new NavMeshPath();
    }

    private void Update()
    { 

    }
}
