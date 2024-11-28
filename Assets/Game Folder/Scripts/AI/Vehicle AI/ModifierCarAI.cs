using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;

public class ModifierCarAI : MonoBehaviour
{
    public List<Transform> checks = new List<Transform> { null };
    public List<Transform> Sidechecks = new List<Transform> { null };
    public PathFollower pFollower;

    public GameObject[] Tires;

    public Light[] lights;

    public bool objectDetectedFront = false;
    public bool objectDetectedSide = false;
    public float speedLimit = 0.02f;
    public int recklessnessThreshold;
    public float frontdistanceChecker;
    float distCheck;

    private float steerAngle = 0f;
    public LayerMask seenLayers = Physics.AllLayers;

    // Start is called before the first frame update
    void Start()
    {
        pFollower = GetComponent<PathFollower>();
        frontdistanceChecker = Random.Range(1f, 2f);
        distCheck = frontdistanceChecker;
    }

    // Update is called once per frame
    void Update()
    {
        ObjectChecks();
        SideObjectChecks();

        if (IsAnythingInFront()) SetSpeed(0);
        if (TurnOnLight()) LightsOnOff(true);
        else LightsOnOff(false);
    }

    void LightsOnOff(bool T)
    {
        foreach(Light l in lights)
        {
            if (T == true)
                l.intensity = 250f;
            else l.intensity = 0f;
        }
    }
    public bool TurnOnLight()
    {
        return TimeManager.Hour < 7 || TimeManager.Hour < 18;
    }

    public bool IsAnythingInFront()
    {
        return (objectDetectedFront == true || objectDetectedSide == true);
    }

    void ObjectChecks()
    {
        Vector3 nextCheckpointRelative = transform.InverseTransformPoint(transform.position);

        float xangle = nextCheckpointRelative.y / nextCheckpointRelative.magnitude;

        float maxDistance = frontdistanceChecker;

        xangle = Mathf.Asin(xangle) * 180f / 3.14f;

        RaycastHit carHit = new RaycastHit();

        int objectInFront = 0;

        for (int i = 0; i < checks.Count; i++)
        {
            bool isObjectInFront = Physics.Raycast(checks[i].position, checks[i].forward, out carHit, maxDistance, seenLayers, QueryTriggerInteraction.Ignore);
            bool isObjectInSide = Physics.Raycast(Sidechecks[i].position, Sidechecks[i].forward, out carHit, maxDistance * 1.5f, seenLayers, QueryTriggerInteraction.Ignore);
#if UNITY_EDITOR
            UnityEngine.Debug.DrawRay(checks[i].position, checks[i].forward * maxDistance, Color.green);
            UnityEngine.Debug.DrawRay(Sidechecks[i].position, Sidechecks[i].forward * maxDistance, Color.blue);
#endif

            if (isObjectInFront == true)
                objectInFront++;
        }

        if (objectInFront > 0)
        {
            objectDetectedFront = true;
        }
        else
        {
            objectDetectedFront = false;
            
        }
    }

    void SideObjectChecks()
    {
        Vector3 nextCheckpointRelative = transform.InverseTransformPoint(transform.position);

        float xangle = nextCheckpointRelative.y / nextCheckpointRelative.magnitude;

        float maxDistance = 1f;

        xangle = Mathf.Asin(xangle) * 180f / 3.14f;

        RaycastHit carHit = new RaycastHit();

        int objectInFront = 0;

        for (int i = 0; i < Sidechecks.Count; i++)
        {
            bool isObjectInSide = Physics.Raycast(Sidechecks[i].position, Sidechecks[i].forward, out carHit, maxDistance, seenLayers, QueryTriggerInteraction.Ignore);
#if UNITY_EDITOR
            UnityEngine.Debug.DrawRay(Sidechecks[i].position, Sidechecks[i].forward * maxDistance, Color.blue);
#endif

            if (isObjectInSide == true)
                objectInFront++;
        }

        if (objectInFront > 0)
        {
            objectDetectedSide = true;
        }
        else
        {
            objectDetectedSide = false;
        }
    }

    protected void SetSpeed(float Speed)
    {
        pFollower.velocity = Speed;
    }
}
