using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public Material greenMaterial;
    public Material redMaterial;

    public Material yellowMaterial;
    public Material greyMaterial;

    MeshRenderer rend;
    public bool bolehJalan;
    public bool keganti;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Material[] mats = rend.materials;
        if (bolehJalan)
        {
            mats[2] = greyMaterial;
            mats[3] = greyMaterial;
            mats[4] = greenMaterial;
            keganti = true;
        }
        else
        {
            mats[2] = redMaterial;
            mats[3] = greyMaterial;
            mats[4] = greyMaterial;
            keganti = false;
        }
        rend.materials = mats;
    }


    public void BolehJalan()
    {
        bolehJalan = true;

    }

    public void GaBolehJalan()
    {
        bolehJalan = false;

    }
}
