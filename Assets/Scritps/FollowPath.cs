using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;


//Make a function that turns this off
//Make a function to control rotation behaviors
//Make a function that makes him face the splines

public class FollowPath : MonoBehaviour
{
    public SplineContainer splineContainer;
    public float speed = 5f;
    private Spline spline;
    private float t = 0f;
    private bool disableMovement;

    [SerializeField]
    [Range(0f, 1f)]
    private float startPos;

    void Start()
    {
        if (splineContainer != null)
        {
            spline = splineContainer.Spline;
        }

        t = startPos;
    }

    void Update()
    {
        if (spline != null)
        {
            MoveAlongSpline();
        }
    }

    void DisableMovement()
    {
        disableMovement = true;
    }

    void MoveAlongSpline()
    {
        if (!disableMovement)
        {
            t += speed * Time.deltaTime / spline.GetLength();
            if (t > 1f)
            {
                t -= 1f; // Loop back to the start
            }

            Vector3 position = spline.EvaluatePosition(t);
            transform.position = splineContainer.transform.TransformPoint(position);
        }
    }
}
