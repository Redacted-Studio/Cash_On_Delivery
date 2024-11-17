using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TrafficWayPioint : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField] private Transform[] nextWaypoints = new Transform[4];

    public Transform GetRandomNextWaypoint()
    {
        List<Transform> availableWaypoints = new List<Transform>();
        int randomNumber;

        foreach (var waypoint in nextWaypoints)
        {
            if (waypoint != null)
            {
                availableWaypoints.Add(waypoint);
            }
        }

        if (availableWaypoints.Count == 0) { return null; }

        randomNumber = Random.Range(0, availableWaypoints.Count);

        return availableWaypoints[randomNumber];
    }

    private void OnDrawGizmos()
    {
        if (TrafficEditorWindow.showWayPointConnectionInformation && TrafficEditorWindow.showWayPointConnections)
            foreach (var waypoint in nextWaypoints)
            {
                if (waypoint != null)
                    GizmoDrawLine(transform.position, waypoint.position, Color.yellow, gameObject.name + " -> " + waypoint.name, Color.black); 
            }
        else if (TrafficEditorWindow.showWayPointConnections)
            foreach (var waypoint in nextWaypoints)
            {
                if (waypoint != null)
                    GizmoDrawLine(transform.position, waypoint.position, Color.yellow, "", Color.black);
            }

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.black;
        if (TrafficEditorWindow.showWayPointInformation)
            Handles.Label(transform.position, gameObject.name, style);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        foreach (var waypoint in nextWaypoints)
        {
            if (waypoint != null)
                Gizmos.DrawWireSphere(waypoint.transform.position, 1.5f);
        }
    }

    // Following VOID made by CodeMonkey on YouTube
    private void GizmoDrawLine(Vector3 from, Vector3 to, Color lineColor, string text, Color textColor)
    {
        Handles.color = lineColor;
        Handles.DrawAAPolyLine(5f, from, to);

        Vector3 dir = (to - from).normalized;
        float distance = Vector3.Distance(from, to);

        for (float i = 0; i < distance; i += 1f)
        {
            Handles.DrawAAPolyLine(
                5f,
                from + dir * i,
                from + (dir * (i - .5f)) + Quaternion.AngleAxis(Time.realtimeSinceStartup * 360f, dir.normalized * 300f) * Vector3.up * .25f
            );
            Handles.DrawAAPolyLine(
                5f,
                from + dir * i,
                from + (dir * (i - .5f)) + Quaternion.AngleAxis(Time.realtimeSinceStartup * 360f + 180, dir.normalized * 300f) * Vector3.up * .25f
            );
        }

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = textColor;
        Handles.Label(from + (dir * distance * .5f), text, style);
    }
}
