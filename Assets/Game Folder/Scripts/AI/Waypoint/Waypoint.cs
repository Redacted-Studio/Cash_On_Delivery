using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> nextAvailWaypoint;
    public List<Waypoint> previousAvailWaypoint;
    public bool isEndPoint;

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

    public Waypoint GetClosestWaypoint(Vector3 PlaceToGo)
    {
        if (isEndPoint) return this;

        float initial = Vector3.Distance(PlaceToGo, nextAvailWaypoint[nextAvailWaypoint.Count - 1].transform.position);
        int keBerapa = nextAvailWaypoint.Count - 1;

        for (int i = 0; i < nextAvailWaypoint.Count; i++)
        {
            float distance = Vector3.Distance(PlaceToGo, nextAvailWaypoint[i].transform.position);
            if (distance < initial)
            {
                initial = distance;
                keBerapa = i;
            }
        }

        return nextAvailWaypoint[keBerapa];
    }
}
