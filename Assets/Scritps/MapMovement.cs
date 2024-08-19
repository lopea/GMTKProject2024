using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private float speed;
    private float maxBounds;
    private float acceleration;
    private Rigidbody rb;

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var mouseDelta = playerInput.Player.MoveMap.ReadValue<Vector2>();

        //rb.constraints = RigidbodyConstraints.FreezeRotationY;  

        //Apply velocity
        //Add min max values

        //Project from the plane to the camera xz plane
        //The camera position to that plane represents the vector that we use as the front
        //Perpindicular is the angle to give to the quaternion


        
        //get camera
        transform.rotation *= Quaternion.Euler(mouseDelta.x, 0, mouseDelta.y);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
    }
}
