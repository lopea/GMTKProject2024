using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinSpin : MonoBehaviour
{
    [SerializeField]
    private float minSpinSpeed = 8;
    [SerializeField]
    private float maxSpinSpeed = 16;

    [SerializeField]
    private float accelerateSpeed = 16;

    [SerializeField]
    private float minSpinTimer = 1.2f;

    [SerializeField]
    private float maxSpinTimer = 6f;

    private float setSpinSpeed = 1;
    private float currSpinSpeed = 1;
    private float spinTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // spin
        transform.Rotate(0, 0, currSpinSpeed * Time.deltaTime);

        // accelerate to set speed
        if (currSpinSpeed < setSpinSpeed)
            currSpinSpeed += Time.deltaTime * accelerateSpeed;
        else
            currSpinSpeed -= Time.deltaTime * accelerateSpeed;

        // dec timer
        spinTimer -= Time.deltaTime;

        // get new spinTime and magnitude etc etc
        if (spinTimer <= 0)
        {
            spinTimer = Random.Range(minSpinTimer, maxSpinTimer);

            setSpinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
            if (Random.Range(0, 1f) < .5f)
                setSpinSpeed *= -1;
        }
    }
}
