using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StickableObject : MonoBehaviour
{
    [HideInInspector] 
    public bool isConnected = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        var stickableObject = collision.gameObject.GetComponent<StickableObject>();
        if (stickableObject != null)
        {
            transform.SetParent(stickableObject.transform.parent, false);
            isConnected = true;
        }
        
        var stickyBall = collision.gameObject.GetComponent<StickyBall>();
        if (stickyBall != null)
        {
            transform.SetParent(stickyBall.transform, false);
            isConnected = true;
        }
        
    }

}
