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
    private fuckFuckScaling _scaling;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (null == _rigidbody)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        
        _scaling = GetComponent<fuckFuckScaling>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isConnected)
        {
            return;
        }

        if (null != _scaling && _scaling.GetScaleBracket() > fuckFuckScaling.playerScaleBracket)
        {
            return;
        }
        
        var stickableObject = collision.gameObject.GetComponent<StickableObject>();
        var stickyBall = collision.gameObject.GetComponent<StickyBall>();

        if ((null != stickableObject && stickableObject.isConnected) || 
            null != stickyBall)
        {
            if (stickableObject != null)
                transform.SetParent(stickableObject.transform.parent);

            if (stickyBall != null)
            {
                transform.SetParent(stickyBall.transform);
                stickyBall.AddObjectToAverageDistance(transform.position);
            }

#if UNITY_EDITOR
            var mov = transform.parent.GetComponent<SimpleMovement>();
            if (null != mov)
            {
                mov.movementModifier += 25.0f;
            }
#endif
            isConnected = true;
            _rigidbody.isKinematic = true;
            Component.Destroy(_rigidbody);
            
        }
    }

}
