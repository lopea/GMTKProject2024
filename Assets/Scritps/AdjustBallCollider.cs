using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustBallCollider : MonoBehaviour
{
    SphereCollider _sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    public void Expand(float distance)
    {
        if (distance > _sphereCollider.radius * transform.localScale.x)
            _sphereCollider.radius = distance / transform.localScale.x * 0.75f;
    }
}
