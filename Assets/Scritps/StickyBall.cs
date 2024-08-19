using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditorInternal;
using UnityEngine;

public class StickyBall : MonoBehaviour
{
    [ReadOnly] public float averageDistance = 0.0f;
    private float totalDistance = 0.0f;
    private int numberOfObjects = 0;
    
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
        float distance = Vector3.Distance(transform.position, collision.transform.position);
      
        totalDistance += distance;
        numberOfObjects += 1;
        
        averageDistance = totalDistance / numberOfObjects;
    }
}
