using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    [SerializeField] public float distanceScale = 15f;

    [SerializeField] private float distLerpSpeed = .72f;

    private static float statDistanceScale;

    public MapMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        statDistanceScale = distanceScale;
    }

    // Update is called once per frame
    void Update()
    {
        //update the anchor to the player (avoids rotation)
        transform.position = Player.transform.position;

        //Check if the player is moving
        if (movement.IsMoving() && Vector3.Dot(movement.GetInputDir(), movement.GetProjectedViewDir()) < .5f)
        {
            
        }

        // lerp distanceScale to desired Dist if it's too far apart
        if (Mathf.Abs(distanceScale - statDistanceScale) > .06f)
        {
            if (distanceScale < statDistanceScale)
                distanceScale += Time.deltaTime * distLerpSpeed;
            else
                distanceScale -= Time.deltaTime * distLerpSpeed;
        }
    }

    private void FixedUpdate()
    {
        Vector3 dir = (Camera.main.transform.localPosition).normalized;

        Camera.main.transform.localPosition = dir * distanceScale;
    }

    public static void setCamDistance(float setScale)
    {
        statDistanceScale = setScale;
    }
}
