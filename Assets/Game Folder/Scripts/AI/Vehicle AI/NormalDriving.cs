using UnityEngine;
using UnityEngine.AI;

public class NormalDriving : AIBase
{
    // PROPERTIES
    [SerializeField] int safeDistance, spacing;
    float tempSpeed;
    [SerializeField] Transform DriverDoor;

    // WAYPOINT REFRENCES
    [SerializeField] Waypoint waypoints;

    [Header("Component Refrences")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] NormalPedestarian Driver;
    [SerializeField] NormalPedestarian Passanger;

    DestinationBase Destination;

    bool isMovingToDestination;

    // ON INITIALIZTION
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tempSpeed = agent.speed;
        safeDistance *= 3;
    }

    override public void onTick()
    {
        if (Driver == null) return;
        SafeDistanceCheck();
        WaypointProcecssing();
    }

    protected void WaypointProcecssing()
    {
        if (waypoints.isEndPoint)
        {
            if (agent.remainingDistance < 5) GetOutDriver();
            agent.autoBraking = true;
            return;
        }

        float distance = Vector3.Distance(transform.position, waypoints.transform.position);
        if (distance <= 3)
        {
            if (Destination)
                waypoints = waypoints.GetClosestWaypoint(Destination.transform.position);
            else
                waypoints = waypoints.GetRandomNextWaypoint();
        }
            
        agent.SetDestination(waypoints.transform.position);
    }

    protected void SafeDistanceCheck()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * safeDistance);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), safeDistance))
        {
            agent.speed = 0;
        } else
        {
            agent.speed = tempSpeed;
        }
    }

    protected void DriverManagement()
    {
        Driver.gameObject.SetActive(false);
    }

    public void SetDriver(NormalPedestarian Drivers)
    {
        Driver = Drivers;
        Destination = Drivers.GetDestination();
        agent.speed = tempSpeed;
        Driver.gameObject.SetActive(false);
    }

    public void GetOutDriver()
    {
        Driver.gameObject.transform.position = DriverDoor.transform.position;
        Driver.gameObject.SetActive(true);
        agent.speed = 0;
        Driver = null;
    }

    public Transform GetDriverDoorPosition()
    {
        return DriverDoor;
    }
    
}