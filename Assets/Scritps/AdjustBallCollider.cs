using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustBallCollider : MonoBehaviour
{
    public float percentage = 0.55f;
    SphereCollider _sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    public void Expand(float distance)
    {
        if (distance > _sphereCollider.radius * transform.localScale.x)
            _sphereCollider.radius = distance / transform.localScale.x * percentage;

        // scale ball up
        float setScale = 2f * _sphereCollider.radius * percentage;
        transform.GetChild(0).localScale = setScale * Vector3.one;

        // zoom cam out
        CameraController.setCamDistance(setScale + 1f);
    }
}
