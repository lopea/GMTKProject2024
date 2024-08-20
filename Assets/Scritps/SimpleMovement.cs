using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
#if UNITY_EDITOR
    
    public float movementModifier = 5.0f;
    
    private Rigidbody _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var speed = 20.0f * movementModifier * Time.deltaTime;
        Console.WriteLine(speed);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rb.AddForce(Vector3.forward * speed);
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rb.AddForce(Vector3.back * speed);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.AddForce(Vector3.left * speed);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rb.AddForce(Vector3.right * speed);
        }
    }
#endif
}
