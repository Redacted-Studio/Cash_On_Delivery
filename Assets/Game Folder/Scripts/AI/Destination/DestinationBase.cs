using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationBase : MonoBehaviour
{
    public Vector3 Destination;

    private void Awake()
    {
        onInit();
    }

    virtual public void onInit()
    {
        Destination = transform.position;
    }
}
