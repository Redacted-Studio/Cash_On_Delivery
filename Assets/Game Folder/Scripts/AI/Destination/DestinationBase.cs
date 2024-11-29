using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DestinationBase : MonoBehaviour
{
    public Vector3 Destination;
    public float Radius;
    public Vector3 Location;

    private void Awake()
    {
        onInit();
    }

    virtual public void onInit()
    {
        Destination = transform.position;
    }
}
