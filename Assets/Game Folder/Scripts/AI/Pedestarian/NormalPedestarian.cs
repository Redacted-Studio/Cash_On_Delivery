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


public class NormalPedestarian : AIBase
{
    [SerializeField] pedestarianState pedState;
    // Start is called before the first frame update
    void Start()
    {
        pedState = pedestarianState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    pedestarianState evaluateState()
    {
        return pedestarianState.IDLE;
    }
}
