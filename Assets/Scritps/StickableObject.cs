using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StickableObject : MonoBehaviour
{
    [HideInInspector] 
    public bool isConnected = false;
    Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (null == _rigidbody)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isConnected)
            return;
        
        var stickableObject = collision.gameObject.GetComponent<StickableObject>();
        var stickyBall = collision.gameObject.GetComponent<StickyBall>();

        if (null != stickableObject || null != stickyBall)
        {
            _rigidbody.isKinematic = true;
            isConnected = true;

            if (stickableObject != null)
                transform.SetParent(stickableObject.transform.parent);
        
            if (stickyBall != null)
                transform.SetParent(stickyBall.transform);

            transform.parent.GetComponent<SimpleMovement>().movementModifier += 25.0f;
        }
    }

}
