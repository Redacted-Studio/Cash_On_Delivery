using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public enum VehicleState
{
    Controlled,
    Offline,
    AI
}

public class CarController : MonoBehaviour
{
    [Header("State")]

    public VehicleState vehicleState;

    [Space]

    [Header("Settings")]

    [SerializeField] private float springLength;
    [SerializeField] private float springStrength;
    [SerializeField] private float springDamper;

    [Space]

    [SerializeField] private float steeringMaxAngle;
    [SerializeField] private AnimationCurve steeringCurve;
    [SerializeField] private float wheelMass;
    [SerializeField] private AnimationCurve wheelGrip;
    [SerializeField] private float gripAmplifier;
    [SerializeField] private bool rotateAllWheels;

    private float steeringTimer;
    private bool noSteeringInput;

    [Space]

    [SerializeField] private float carTopSpeed;
    [SerializeField] private float inputMultiplier;
    [SerializeField] private AnimationCurve powerCurve;
    [Range(0f, 1f)]
    [SerializeField] private float brakePower;
    [SerializeField] private AnimationCurve noInputBreakPower;
    [SerializeField] private float noInputBrakePowerAmplifier;
    [SerializeField] private float carTopBackSpeed;
    [SerializeField] private AnimationCurve powerCurveBrake;
    private float accelerationInput;

    [Space]
    [Header("Wheels")]

    [SerializeField] private GameObject frontRight;
    [SerializeField] private GameObject frontLeft;
    [SerializeField] private GameObject backRight;
    [SerializeField] private GameObject backLeft;

    [SerializeField] private GameObject frontRightV;
    [SerializeField] private GameObject frontLeftV;
    [SerializeField] private GameObject backRightV;
    [SerializeField] private GameObject backLeftV;

    [Space]
    [Header("Car")]

    [SerializeField] private GameObject car;
    [SerializeField] private Rigidbody carRb;

    [Space]
    [Header("Visual Feedback")]

    [SerializeField] private GameObject cam;
    [SerializeField] private AnimationCurve camPower;
    [SerializeField] private float camPowerMutliplier;
    private float startingPosition;

    [Space]
    [Header("UI")]

    [SerializeField] private TextMeshProUGUI speedDisplay;

    // ai

    [SerializeField] private Transform targetWaypoint;
    [SerializeField] private float reachedWaypointDistance;
    [SerializeField] private float brakeWaypointDistance;
    [SerializeField] private LayerMask whatIsCar;

    private void Start()
    {
        startingPosition = cam.transform.localPosition.z;

        InvokeRepeating("CustomCarPhysics", 0f, 0.005f);
    }

    private void CustomCarPhysics()
    {
        bool[] enablePhysics = new bool[4];
        bool oneIsTrue = false;

        enablePhysics[0] = WheelSpring(frontRight, frontRightV);
        enablePhysics[1] = WheelSpring(frontLeft, frontLeftV);
        enablePhysics[2] = WheelSpring(backRight, backRightV);
        enablePhysics[3] = WheelSpring(backLeft, backLeftV);

        foreach (var state in enablePhysics)
        {
            if (state)
                oneIsTrue = true;
        }

        carRb.useGravity = oneIsTrue;

        Vector3 dirToMovePosition = (targetWaypoint.position - transform.position).normalized;

        float angleToDir = 0f;

        float steeringInput = 0f;

        if (vehicleState == VehicleState.Controlled)
            steeringInput = Input.GetAxisRaw("Horizontal") * steeringMaxAngle;

        if (vehicleState == VehicleState.AI)
        {
            angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

            steeringInput = Mathf.Clamp(angleToDir, -steeringMaxAngle, steeringMaxAngle);
        }

        float carSpeed = Vector3.Dot(car.transform.forward, carRb.velocity);

        float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

        float steeringMultiplier = steeringCurve.Evaluate(normalizedSpeed);

        steeringTimer += Time.deltaTime;
        steeringTimer = Mathf.Clamp01(steeringTimer);

        if (steeringInput == 0f)
        {
            if (!noSteeringInput)
            {
                steeringTimer = 0f;
                noSteeringInput = true;
            }

            frontRight.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(Mathf.Lerp(frontRight.transform.localRotation.y, 0f, steeringTimer), -steeringMaxAngle, steeringMaxAngle), 0);
            frontLeft.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(Mathf.Lerp(frontLeft.transform.localRotation.y, 0f, steeringTimer), -steeringMaxAngle, steeringMaxAngle), 0);

            if (rotateAllWheels)
            {
                backRight.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(Mathf.Lerp(frontRight.transform.localRotation.y, 0f, steeringTimer), -steeringMaxAngle, steeringMaxAngle), 0);
                backLeft.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(Mathf.Lerp(frontLeft.transform.localRotation.y, 0f, steeringTimer), -steeringMaxAngle, steeringMaxAngle), 0);
            }
        }
        else
        {
            if (noSteeringInput)
            {
                steeringTimer = 0f;
                noSteeringInput = false;
            }

            frontRight.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(Mathf.Lerp(frontRight.transform.localRotation.y, steeringInput * steeringMultiplier, steeringTimer), -steeringMaxAngle, steeringMaxAngle), 0);
            frontLeft.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(Mathf.Lerp(frontLeft.transform.localRotation.y, steeringInput * steeringMultiplier, steeringTimer), -steeringMaxAngle, steeringMaxAngle), 0);

            if (rotateAllWheels)
            {
                backRight.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(Mathf.Lerp(frontRight.transform.localRotation.y, -steeringInput * steeringMultiplier, steeringTimer), -steeringMaxAngle, steeringMaxAngle), 0);
                backLeft.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(Mathf.Lerp(frontLeft.transform.localRotation.y, -steeringInput * steeringMultiplier, steeringTimer), -steeringMaxAngle, steeringMaxAngle), 0);
            }
        }

        WheelSteering(frontRight, true);
        WheelSteering(frontLeft, true);
        WheelSteering(backRight, false);
        WheelSteering(backLeft, false);

        if (vehicleState == VehicleState.Controlled)
            accelerationInput = Input.GetAxisRaw("Vertical") * inputMultiplier;

        if (vehicleState == VehicleState.AI)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetWaypoint.position);
            accelerationInput = 1f * inputMultiplier;

            if (distanceToTarget < brakeWaypointDistance)
            {
                accelerationInput = -0.1f * inputMultiplier;
            }

            if (distanceToTarget < reachedWaypointDistance)
            {
                accelerationInput = 0f;
                targetWaypoint = targetWaypoint.gameObject.GetComponent<TrafficWayPioint>().GetRandomNextWaypoint();
            }
        }

        if (Physics.Raycast(transform.position, transform.forward, 3.5f, whatIsCar) && vehicleState == VehicleState.AI)
        {
            accelerationInput = -0.25f * inputMultiplier;
        }

        WheelAcceleration(frontRight);
        WheelAcceleration(frontLeft);
        WheelAcceleration(backRight);
        WheelAcceleration(backLeft);

        if (vehicleState == VehicleState.Controlled)
            UpdateVisualFeedback();

        if (vehicleState == VehicleState.Controlled)
            UpdateUI();
    }

    private bool WheelSpring(GameObject wheel, GameObject wheelV)
    {
        RaycastHit ray;

        if (Physics.Raycast(wheel.transform.position, -wheel.transform.up, out ray, springLength + 0.25f))
        {
            Vector3 springDirection = wheel.transform.up;

            Vector3 wheelWorldVelocitiy = carRb.GetPointVelocity(wheel.transform.position);

            float offset = springLength - ray.distance;

            float velocity = Vector3.Dot(springDirection, wheelWorldVelocitiy);

            float force = (offset * springStrength) - (velocity * springDamper);

            carRb.AddForceAtPosition(springDirection * force, wheel.transform.position);

            wheelV.transform.localPosition = new Vector3(wheelV.transform.localPosition.x, -ray.distance - 0.1f, wheelV.transform.localPosition.z);

            return false;
        }
        else
        {
            return true;
        }
    }

    private void WheelSteering(GameObject wheel, bool front)
    {
        RaycastHit ray;

        if (Physics.Raycast(wheel.transform.position, -wheel.transform.up, out ray, springLength + 0.25f))
        {
            Vector3 steeringDirection;

            steeringDirection = wheel.transform.right;

            Vector3 wheelWorldVelocity = carRb.GetPointVelocity(wheel.transform.position);

            float steeringVelocity = Vector3.Dot(steeringDirection, wheelWorldVelocity);

            float carSpeed = Vector3.Dot(car.transform.forward, carRb.velocity);

            float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

            float steeringMultiplier = wheelGrip.Evaluate(normalizedSpeed);

            float desiredVelocityChange = -steeringVelocity * steeringMultiplier;

            float desiredAcceleration = desiredVelocityChange / 0.005f;

            Debug.DrawLine(wheel.transform.position, wheel.transform.position + (steeringDirection * wheelMass * desiredAcceleration * gripAmplifier / 1000f), Color.yellow);

            carRb.AddForceAtPosition(steeringDirection * wheelMass * desiredAcceleration * gripAmplifier, wheel.transform.position);
        }
    }

    private void WheelAcceleration(GameObject wheel)
    {
        RaycastHit ray;

        if (Physics.Raycast(wheel.transform.position, -wheel.transform.up, out ray, springLength + 0.25f))
        {
            Vector3 accelerationDirection = wheel.transform.forward;

            if (accelerationInput > 0.0f)
            {
                float carSpeed = Vector3.Dot(car.transform.forward, carRb.velocity);

                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

                float availablePower = powerCurve.Evaluate(normalizedSpeed) * accelerationInput;

                if (carSpeed < carTopSpeed)
                    carRb.AddForceAtPosition(accelerationDirection * availablePower, wheel.transform.position);
            }

            if (accelerationInput < 0.0f)
            {
                Vector3 wheelWorldVelocity = carRb.GetPointVelocity(wheel.transform.position);

                float drivingVelocity = Vector3.Dot(accelerationDirection, wheelWorldVelocity);

                if (drivingVelocity > 0.9f)
                {
                    float desiredVelocityChange = -drivingVelocity * brakePower;

                    float desiredAcceleration = desiredVelocityChange / 0.005f;

                    carRb.AddForceAtPosition(accelerationDirection * wheelMass * desiredAcceleration, wheel.transform.position);
                }    
                else
                {
                    float carSpeed = Vector3.Dot(car.transform.forward, carRb.velocity);

                    float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopBackSpeed);

                    float availablePower = powerCurveBrake.Evaluate(normalizedSpeed) * accelerationInput;

                    if (carSpeed > -carTopBackSpeed)
                        carRb.AddForceAtPosition(accelerationDirection * availablePower, wheel.transform.position);
                }
            }

            if (accelerationInput == 0.0f)
            {
                Vector3 wheelWorldVelocity = carRb.GetPointVelocity(wheel.transform.position);

                float drivingVelocity = Vector3.Dot(accelerationDirection, wheelWorldVelocity);
                
                float carSpeed = Vector3.Dot(car.transform.forward, carRb.velocity);

                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

                float desiredVelocityChange = -drivingVelocity * noInputBreakPower.Evaluate(normalizedSpeed) * noInputBrakePowerAmplifier; ////////////////////////////////////////////////////////////////////

                float desiredAcceleration = desiredVelocityChange / 0.005f;

                carRb.AddForceAtPosition(accelerationDirection * wheelMass * desiredAcceleration, wheel.transform.position);
            }
        }
    }

    private void UpdateVisualFeedback()
    {
        float carSpeed = Vector3.Dot(car.transform.forward, carRb.velocity);

        float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

        float camOffset = camPower.Evaluate(normalizedSpeed) * camPowerMutliplier;

        cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, startingPosition - camOffset);
    }

    private void UpdateUI()
    {
        float carSpeed = Vector3.Dot(car.transform.forward, carRb.velocity);
        speedDisplay.text = string.Concat(carSpeed, " speed");
    }

    private void OnDrawGizmos()
    {
        if (vehicleState == VehicleState.AI && Application.isPlaying)
        {
            Gizmos.color = Color.yellow;

            if (TrafficEditorWindow.showCarDestination)
                Gizmos.DrawSphere(targetWaypoint.position, 1f);

            Gizmos.DrawWireSphere(transform.position, reachedWaypointDistance);
            Gizmos.DrawWireSphere(transform.position, brakeWaypointDistance);

            if (TrafficEditorWindow.showCarDestination)
                GizmoDrawLine(transform.position, targetWaypoint.position, Color.red, "Destination of " + gameObject.name +": " + targetWaypoint.name, Color.black);
        }
    }

    // Following VOID made by CodeMonkey on YouTube
    private void GizmoDrawLine(Vector3 from, Vector3 to, Color lineColor, string text, Color textColor)
    {
        Handles.color = lineColor;
        Handles.DrawAAPolyLine(5f, from, to);

        Vector3 dir = (to - from).normalized;
        float distance = Vector3.Distance(from, to);

        for (float i = 0; i < distance; i += 1f)
        {
            Handles.DrawAAPolyLine(
                5f,
                from + dir * i,
                from + (dir * (i - .5f)) + Quaternion.AngleAxis(Time.realtimeSinceStartup * 360f, dir.normalized * 300f) * Vector3.up * .25f
            );
            Handles.DrawAAPolyLine(
                5f,
                from + dir * i,
                from + (dir * (i - .5f)) + Quaternion.AngleAxis(Time.realtimeSinceStartup * 360f + 180, dir.normalized * 300f) * Vector3.up * .25f
            );
        }

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = textColor;
        Handles.Label(from + (dir * distance * .5f), text, style);
    }
}
