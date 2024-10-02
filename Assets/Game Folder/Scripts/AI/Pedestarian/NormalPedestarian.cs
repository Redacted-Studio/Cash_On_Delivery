using System.Collections;
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
    [SerializeField] PedestarianDestination destination = new PedestarianDestination();
    bool isHavingDestination, isMovingToDestination, isOwningVehicle, isMovingToVehicle;

    [Header("Component Refrences")]
    NavMeshAgent agent;
    [SerializeField] NormalDriving Car;
    Vector3 myCarDoor;
    private AIManager AImanager;

    void Start()
    {
        pedState = pedestarianState.IDLE;
        agent = GetComponent<NavMeshAgent>();
        if (Car)
        {
            isOwningVehicle = true;
            myCarDoor = Car.GetDriverDoorPosition().position;
        }

        AImanager = FindObjectOfType<AIManager>();
        if (AImanager)
            AImanager.RegisterAI(this);

        //StartCoroutine(Brain());
    }

    void Update()
    {
        Brain();
    }

    protected void Brain()
    {
        if (destination)
        {
            if (distanceChecker() > 100)
                SeekVehicle();
            else
                DestinationController();
        }
        
        //Debug.Log("Processing");
    }

    protected float distanceChecker()
    {
        return Vector3.Distance(destination.transform.position, transform.position);
    }

    protected void SeekVehicle()
    {
        if (!isOwningVehicle) return;
        if (agent.SetDestination(myCarDoor)) isMovingToVehicle = true;
        StartCoroutine(WaitSecond(3));
        float doorToMe = Vector3.Distance(transform.position, myCarDoor);
        Debug.Log(doorToMe);
        if (doorToMe <= 2 && isMovingToVehicle)
        {
            isMovingToVehicle = false;
            Car.SetDriver(this);
        }
    }

    protected void DestinationController()
    {
        if (!destination) return;

        SetDestination(destination);

        if (isHavingDestination)
        {
            moveTo(destination.transform);
            pedState = pedestarianState.GO_TO_DESTINATION;
        }

        if (agent.remainingDistance <= 2 && isHavingDestination)
        {
            RemoveDestination();
            isHavingDestination = false;
        }
    }

    public DestinationBase GetDestination()
    {
        return destination;
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
        destination = destinations;
        isHavingDestination = true;
    }

    /// <summary>
    /// Buat Ngeremove Destination dari List yang ada di destination pada, at = destination ke berapa
    /// </summary>
    protected void RemoveDestination()
    {
        destination = null;
    }
    #endregion

    #region Pedestarian State Controller
    pedestarianState evaluateState()
    {
        if (isHavingDestination && isMovingToDestination) return pedestarianState.GO_TO_DESTINATION;
        return pedestarianState.IDLE;
    }
    #endregion

    #region Destructor

    /// <summary>
    /// Destructor For AI
    /// </summary>
    private void OnDestroy()
    {
        AImanager.UnregisterAI(this);
    }

    #endregion

    #region Controller Time

    IEnumerator WaitSecond(float sec)
    {
        while (true)
        {
            yield return new WaitForSeconds(sec);
        }
    }

    #endregion
}