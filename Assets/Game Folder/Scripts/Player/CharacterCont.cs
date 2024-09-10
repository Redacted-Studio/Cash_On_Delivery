using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.ProBuilder;

public class CharacterCont : NetworkBehaviour
{
    [Header("Refrences")]
    private CharacterController characterController;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject face;
    [SerializeField] GameObject UI;

    [Header("Player Speed")]
    [SerializeField] private float moveSpeed;

    [Header("Player Attribute")]
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpHeight = 0.2f;

    [Header("Input")]
    [SerializeField] float mouseSens;

    // UN PUBLICY VAR
    private float moveInput, turnInput;
    private float mouseX, mouseY;
    float xRot, verticalVel;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            virtualCamera.gameObject.SetActive(false);
            return;
        }
        face.SetActive(false);
        UI.SetActive(false);
        Inputhandler();
        movementHandler();
        
    }

    void Inputhandler()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
    
    void movementHandler()
    {
        groundMove();
        turn();
    }

    void groundMove()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move = transform.TransformDirection(move);
        move *= moveSpeed;
        move.y = vertForceCalc();
        characterController.Move(move * Time.deltaTime);
    }

    void turn()
    {
        mouseX *= mouseSens * Time.deltaTime;
        mouseY *= mouseSens * Time.deltaTime;

        xRot -= mouseY;

        xRot = math.clamp(xRot, -90, 90);

        virtualCamera.transform.localRotation = Quaternion.Euler(xRot, 0, 0);

        transform.Rotate(Vector3.up * mouseX);
    }

    float vertForceCalc()
    {
        if (characterController.isGrounded)
        {
            verticalVel = 0;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVel = math.sqrt(jumpHeight * gravity);
            }

        } else
        {
            verticalVel -= gravity * Time.deltaTime;
        }

        return verticalVel;
    }
}
