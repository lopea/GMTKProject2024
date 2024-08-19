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
    
    private void FixedUpdate()
    {
        var deltaSpeed = (Speed * currAcceleration) * Time.deltaTime;
        var mouseDelta = playerInput.Player.MoveMap.ReadValue<Vector2>() * deltaSpeed;
        var cameraDir = Camera.main.transform.forward;
        
        if (Vector2.zero != mouseDelta)
        {
            currHorizontalAngle = Mathf.Clamp(currHorizontalAngle + mouseDelta.x, -MaxAngle, MaxAngle);
            currVerticalAngle = Mathf.Clamp(currVerticalAngle + mouseDelta.y, -MaxAngle, MaxAngle);
            currAcceleration += Mathf.Clamp(AccelerationSpeed * Time.deltaTime, 1, MaxAcceleration);
        }
        else
        {
            if (Mathf.Abs(currHorizontalAngle) > 1f)
            {
                currHorizontalAngle -= Mathf.Sign(currHorizontalAngle) * resetSpeed * Time.deltaTime;
                if (Mathf.Abs(currHorizontalAngle) < .9f)
                    currHorizontalAngle = 0.0f;
            }

            if (Mathf.Abs( currVerticalAngle) > 1f)
            {
                currVerticalAngle -= Mathf.Sign(currVerticalAngle) * resetSpeed * Time.deltaTime;
                if (Mathf.Abs(currVerticalAngle) < .9f)
                    currVerticalAngle = 0.0f;
            }

            currAcceleration = 1.0f;
        }

        //rb.constraints = RigidbodyConstraints.FreezeRotationY;  
        

        //Project from the plane to the camera xz plane
        Vector3 projectedCameraDir = Vector3.ProjectOnPlane(cameraDir, Vector3.up);
        
        //get each axis to rotate the map (horizontal = sideways, vertical = forward/backward)
        Vector3 rotationAxisHorizontal = projectedCameraDir.normalized;
        Vector3 rotationAxisVertical = Vector3.Cross(Vector3.up,rotationAxisHorizontal);
        
        //The camera position to that plane represents the vector that we use as the front
        transform.rotation = Quaternion.AngleAxis(currHorizontalAngle, -rotationAxisHorizontal) 
                             * Quaternion.AngleAxis(currVerticalAngle, rotationAxisVertical);

        
       transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

       
    }
}
