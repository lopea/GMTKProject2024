using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private Transform _playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _playerTransform.position += new Vector3(0, 0, 1.0f) * Time.deltaTime;
        }
    }
}
