using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierCarAI : MonoBehaviour
{
    public List<Transform> checks = new List<Transform> { null };
    public List<Transform> Sidechecks = new List<Transform> { null };
    public PathFollower pFollower;

    public bool objectDetected = false;
    public float speedLimit = 0.02f;
    public int recklessnessThreshold;

    private float steerAngle = 0f;
    public LayerMask seenLayers = Physics.AllLayers;

    // Start is called before the first frame update
    void Start()
    {
        pFollower = GetComponent<PathFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        ObjectChecks();
        SideObjectChecks();
    }

    void ObjectChecks()
    {
        Vector3 nextCheckpointRelative = transform.InverseTransformPoint(transform.position);

        float xangle = nextCheckpointRelative.y / nextCheckpointRelative.magnitude;

        float maxDistance = .2f;

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
            SetSpeed(0);
            objectDetected = true;
        }
        else
        {
            objectDetected = false;
        }
    }

    void SideObjectChecks()
    {
        Vector3 nextCheckpointRelative = transform.InverseTransformPoint(transform.position);

        float xangle = nextCheckpointRelative.y / nextCheckpointRelative.magnitude;

        float maxDistance = .25f;

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
            SetSpeed(0);
            objectDetected = true;
        }
        else
        {
            objectDetected = false;
        }
    }

    protected void SetSpeed(float Speed)
    {
        pFollower.velocity = Speed;
    }
}
