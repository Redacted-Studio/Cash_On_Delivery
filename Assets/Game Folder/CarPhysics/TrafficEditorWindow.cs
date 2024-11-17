using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TrafficEditorWindow : EditorWindow
{
    public static bool showCarDestination = true;
    public static bool showWayPointInformation = true;
    public static bool showWayPointConnectionInformation = true;
    public static bool showWayPointConnections = true;

    [MenuItem("Window/MS/TrafficEditor")]
    public static void InitializeEditor()
    {
        EditorWindow.GetWindow<TrafficEditorWindow>("MS - Traffic Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("MaixenStudio - Traffic Editor", EditorStyles.boldLabel);

        // Car Destination

        GUILayout.BeginHorizontal();
        GUILayout.Label("Car Destination                 ");
        GUILayout.FlexibleSpace();
        if (showCarDestination)
        {
            GUI.contentColor = Color.green;
            GUILayout.Label("On", EditorStyles.boldLabel);
        }
        else
        {
            GUI.contentColor = Color.red;
            GUILayout.Label("Off", EditorStyles.boldLabel);
        }
        GUI.contentColor = Color.white;
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Change"))
        {
            showCarDestination = !showCarDestination;
        }
        GUILayout.EndHorizontal();

        // Way Point Information

        GUILayout.BeginHorizontal();
        GUILayout.Label("Waypoint Information      ");
        GUILayout.FlexibleSpace();
        if (showWayPointInformation)
        {
            GUI.contentColor = Color.green;
            GUILayout.Label("On", EditorStyles.boldLabel);
        }
        else
        {
            GUI.contentColor = Color.red;
            GUILayout.Label("Off", EditorStyles.boldLabel);
        }
        GUI.contentColor = Color.white;
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Change"))
        {
            showWayPointInformation = !showWayPointInformation;
        }
        GUILayout.EndHorizontal();

        // Way Point Connections

        GUILayout.BeginHorizontal();
        GUILayout.Label("Waypoint Connections    ");
        GUILayout.FlexibleSpace();
        if (showWayPointConnections)
        {
            GUI.contentColor = Color.green;
            GUILayout.Label("On", EditorStyles.boldLabel);
        }
        else
        {
            GUI.contentColor = Color.red;
            GUILayout.Label("Off", EditorStyles.boldLabel);
        }
        GUI.contentColor = Color.white;
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Change"))
        {
            showWayPointConnections = !showWayPointConnections;
        }
        GUILayout.EndHorizontal();

        // Way Point Connection Information

        GUILayout.BeginHorizontal();
        GUILayout.Label("Way Point C. Information");
        GUILayout.FlexibleSpace();
        if (showWayPointConnectionInformation && !showWayPointConnections)
        {
            GUI.contentColor = Color.yellow;
            GUILayout.Label("Nw", EditorStyles.boldLabel);
        }
        else if (showWayPointConnectionInformation && showWayPointConnections)
        {
            GUI.contentColor = Color.green;
            GUILayout.Label("On", EditorStyles.boldLabel);
        }
        else
        {
            GUI.contentColor = Color.red;
            GUILayout.Label("Off", EditorStyles.boldLabel);
        }
        GUI.contentColor = Color.white;
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Change"))
        {
            showWayPointConnectionInformation = !showWayPointConnectionInformation;
        }
        GUILayout.EndHorizontal();
    }
}
