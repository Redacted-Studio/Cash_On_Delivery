using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public enum pedestarianState
{
    IDLE,
    WANDERING,
    GO_TO_DESTINATION,
}


public class NormalPedestarian : AIBase
{
    [Header("Pedestarian State")]
    [SerializeField] pedestarianState pedState;

    [Header("Pedestarian Destination Controller")]
    [SerializeField] List<PedestarianDestination> destination = new List<PedestarianDestination>();
    bool isHavingDestination, isMovingToDestination;

    [Header("Component Refrences")]
    [SerializeField] NavMeshAgent agent;

    void Start()
    {
        pedState = pedestarianState.IDLE;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
    }
    
    #region Destination Controller
    /// <summary>
    /// Buat ngeset Destination dari Pedestarian
    /// </summary>
    public void SetDestination(PedestarianDestination destinations)
    {
        destination.Add(destinations);
    }

    /// <summary>
    /// Buat Ngeremove Destination dari List yang ada di destination pada, at = destination ke berapa
    /// </summary>
    protected void RemoveDestination(int at)
    {
        destination.RemoveAt(at);
    }

    /// <summary>
    /// Ngebersihin semua Destination yang udah ada di AI
    /// </summary>
    public void ClearDestination()
    {
        destination.Clear();
    }
    #endregion

    #region Pedestarian State Controller
    pedestarianState evaluateState()
    {
        if (isHavingDestination && isMovingToDestination) return pedestarianState.GO_TO_DESTINATION;
        return pedestarianState.IDLE;
    }
    #endregion
}