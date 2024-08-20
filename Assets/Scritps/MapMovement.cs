using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MapMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    
    private Rigidbody rb;

    private float currHorizontalAngle = 0;
    private float currVerticalAngle = 0;
    private float currAcceleration = 1;

    private Vector3 dir;
    [SerializeField] private float MaxAngle = 45;
    [SerializeField] private float Speed = .1f;

    [SerializeField] private float resetSpeed = 10;
    [SerializeField] private float AccelerationSpeed = 2;
    [SerializeField] private float MaxAcceleration = 10;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool IsMoving()
    {
        return dir != Vector3.zero;
    }
    public Vector3 GetInputDir()
    {
        return dir;
    }
    private void Update()
    {
        var deltaSpeed = (Speed * currAcceleration) * Time.deltaTime;
        var mouseDelta = playerInput.Player.MoveMap.ReadValue<Vector2>() * deltaSpeed;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            currAcceleration = 0;
        }

        if (mouseDelta.x != 0)
        {
            currHorizontalAngle = Mathf.Clamp(currHorizontalAngle + mouseDelta.x, -MaxAngle, MaxAngle);
            currAcceleration += Mathf.Clamp(AccelerationSpeed * Time.deltaTime, 1, MaxAcceleration);
            dir = new Vector3(mouseDelta.x, 0, mouseDelta.y);
            dir.Normalize();
        }
        else
        {
            dir = Vector3.zero;

            if (Mathf.Abs(currHorizontalAngle) > 1f)
            {
                currHorizontalAngle -= Mathf.Sign(currHorizontalAngle) * resetSpeed * Time.deltaTime;
                if (Mathf.Abs(currHorizontalAngle) < .9f)
                    currHorizontalAngle = 0.0f;
            }

        }

        if (mouseDelta.y != 0)
        {
            currVerticalAngle = Mathf.Clamp(currVerticalAngle + mouseDelta.y, -MaxAngle, MaxAngle);
            currAcceleration += Mathf.Clamp(AccelerationSpeed * Time.deltaTime, 1, MaxAcceleration);
            dir = new Vector3(mouseDelta.x, 0, mouseDelta.y);
            dir.Normalize();
        }
        else
        {
            dir = Vector3.zero;

            if (Mathf.Abs(currVerticalAngle) > 1f)
            {
                currVerticalAngle -= Mathf.Sign(currVerticalAngle) * resetSpeed * Time.deltaTime;
                if (Mathf.Abs(currVerticalAngle) < .9f)
                    currVerticalAngle = 0.0f;
            }
        }

        //rb.constraints = RigidbodyConstraints.FreezeRotationY;  



        //get each axis to rotate the map (horizontal = sideways, vertical = forward/backward)
        Vector3 rotationAxisHorizontal = GetProjectedViewDir();
        Vector3 rotationAxisVertical = Vector3.Cross(Vector3.up,rotationAxisHorizontal);
        
        //The camera position to that plane represents the vector that we use as the front
        transform.rotation = Quaternion.AngleAxis(currHorizontalAngle, -rotationAxisHorizontal) 
                             * Quaternion.AngleAxis(currVerticalAngle, rotationAxisVertical);

        
       transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

       
    }

    public Vector3 GetProjectedViewDir()
    {
        var cameraDir = Camera.main.transform.forward;
        //Project from the plane to the camera xz plane
        Vector3 projectedCameraDir = Vector3.ProjectOnPlane(cameraDir, Vector3.up);
        return projectedCameraDir.normalized;
    }
}
