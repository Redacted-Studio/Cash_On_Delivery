using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
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
    [Header("Backend Refrences")]
    private int NPC_UID;

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
        Brain();
    }

    protected void Brain()
    {
        if (destination.Count > 0)
            SetDestination(destination[0]);

        if (isHavingDestination)
        {
            moveTo(destination[0].transform);
            pedState = pedestarianState.GO_TO_DESTINATION;
        }

        if (agent.remainingDistance <= 2 && isHavingDestination)
        {
            RemoveDestination(0);
            isHavingDestination = false;
        }

        if (destination.Count == 0)
            pedState = pedestarianState.IDLE;
            

        Debug.Log("Processing");
    }

    #region Brain Processing

    protected void moveTo(Transform moveTo)
    {
        agent.SetDestination(moveTo.position);
    }

    #endregion

    #region Destination Controller
    /// <summary>
    /// Buat ngeset Destination dari Pedestarian
    /// </summary>
    public void SetDestination(PedestarianDestination destinations)
    {
        if (!destination.Contains(destinations))
            destination.Add(destinations);
        
        isHavingDestination = true;
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