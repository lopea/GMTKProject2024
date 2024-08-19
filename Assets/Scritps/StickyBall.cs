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
    private float _totalDistance = 0.0f;
    private int _numberOfObjects = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddObjectToAverageDistance(Vector3 objectPositionWorldCoord)
    {
        float distance = Vector3.Distance(transform.position, objectPositionWorldCoord);
      
        _totalDistance += distance;
        _numberOfObjects += 1;
        
        averageDistance = _totalDistance / _numberOfObjects;
    }
}
