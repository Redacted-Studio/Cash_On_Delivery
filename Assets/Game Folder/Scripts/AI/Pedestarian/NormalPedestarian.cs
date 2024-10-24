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
    [SerializeField] PedestarianDestination destination;
    bool isHavingDestination, isMovingToDestination, isOwningVehicle, isMovingToVehicle;

    [Header("Component Refrences")]
    NavMeshAgent agent;
    [SerializeField] NormalDriving Car;
    Vector3 myCarDoor;
    private AIManager AImanager;
    [SerializeField] private GameObject AIModel;
    private Animator modelAnimator;

    void Start()
    {
        pedState = pedestarianState.IDLE;
        agent = GetComponent<NavMeshAgent>();
        if (Car)
        {
            isOwningVehicle = true;
            myCarDoor = Car.GetDriverDoorPosition().position;
        }

        AImanager = AIManager.GetInstances();

        if(AIModel)
        {
            modelAnimator = AIModel.GetComponent<Animator>();
        }

        //StartCoroutine(Brain());
    }

    override public void onTick()
    {
        Brain();
        
    }

    protected void AnimationHandler()
    {
        if (agent.velocity.magnitude == 0)
        {
            modelAnimator.SetBool("isWalking", false);
        }
        else if (agent.velocity.magnitude > 0)
        {
            modelAnimator.SetBool("isWalking", true);
        } 
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

        if (agent.isOnOffMeshLink)
        {
            OffMeshLinkData data = agent.currentOffMeshLinkData;
            modelAnimator.SetBool("isWalking", true);

            //calculate the final point of the link
            Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;

            //Move the agent to the end point
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            gameObject.transform.LookAt(Vector3.MoveTowards(agent.transform.position, endPos, agent.speed));

            //when the agent reach the end point you should tell it, and the agent will "exit" the link and work normally after that
            if (agent.transform.position == endPos)
            {
                agent.CompleteOffMeshLink();
            }
        } else
        {
            AnimationHandler();
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

        if (agent.remainingDistance == 0 && isHavingDestination)
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