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

    [SerializeField] private float MaxAngle = 45;
    [SerializeField] private float Speed = .1f;
    

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

    
    private void Update()
    {
        var mouseDelta = playerInput.Player.MoveMap.ReadValue<Vector2>()*Speed;
        var cameraDir = Camera.main.transform.forward;

        currHorizontalAngle = Mathf.Clamp(currHorizontalAngle + mouseDelta.x, -MaxAngle, MaxAngle);
        currVerticalAngle = Mathf.Clamp(currVerticalAngle + mouseDelta.y, -MaxAngle, MaxAngle);
        
        //rb.constraints = RigidbodyConstraints.FreezeRotationY;  
        

        //Project from the plane to the camera xz plane
        Vector3 projectedCameraDir = Vector3.ProjectOnPlane(cameraDir, Vector3.up);
        
        //get each axis to rotate the map (horizontal = sideways, vertical = forward/backward)
        Vector3 rotationAxisHorizontal = projectedCameraDir.normalized;
        Vector3 rotationAxisVertical = Vector3.Cross(Vector3.up,rotationAxisHorizontal);
        
        //The camera position to that plane represents the vector that we use as the front
        transform.rotation = Quaternion.AngleAxis(currHorizontalAngle, -rotationAxisHorizontal) * Quaternion.AngleAxis(currVerticalAngle, rotationAxisVertical);


       transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
    }
    
    
    
}
