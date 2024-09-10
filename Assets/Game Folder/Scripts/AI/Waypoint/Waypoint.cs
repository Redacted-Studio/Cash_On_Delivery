using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> nextAvailWaypoint;

    private void OnDrawGizmos()
    {
        foreach (var waypoint in nextAvailWaypoint) 
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, waypoint.transform.position);
        }        
    }

    public Waypoint GetRandomNextWaypoint()
    {
        if (nextAvailWaypoint == null) return null;

        if (nextAvailWaypoint.Count > 1)
        {
            return nextAvailWaypoint[Random.Range(0,nextAvailWaypoint.Count - 1)];
        }

        return nextAvailWaypoint[0];
    }
}
