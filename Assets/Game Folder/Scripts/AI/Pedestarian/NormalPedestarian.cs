using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public enum pedestarianState
    {
    IDLE,
    WANDERING,
    GO_TO_DESTINATION,
}


public class NormalPedestarian : NetworkBehaviour
{
    [SerializeField] pedestarianState pedState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
