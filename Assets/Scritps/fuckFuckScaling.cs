using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuckFuckScaling : MonoBehaviour
{
    // static fields
    public static int playerScaleBracket;
    private static UnityEngine.Events.UnityEvent callScaleFUckback = new UnityEngine.Events.UnityEvent();

    // 

    [SerializeField]
    private bool isKinetic = false;

    [SerializeField]
    private int scaleBracket;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // when the player changes scales, this callback will tell all the listeners
        callScaleFUckback.AddListener(OnScaleChange);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // callback function to be used when player changes scale
    private void OnScaleChange()
    {
        // if the same scale, check what it should be
        if (playerScaleBracket == scaleBracket)
        {
            rb.isKinematic = isKinetic;
            rb.useGravity = isKinetic;
        }
        // if size bracket is different, make larger obj static and small obj kinetic
        else
        {
            if (playerScaleBracket < scaleBracket)
                rb.isKinematic = false;
            else
                rb.isKinematic = true;
            rb.useGravity = rb.isKinematic;
        }
    }

    // when updating player scale, tell all the objs
    public static void PlayerSetScale(int setScale)
    {
        playerScaleBracket = setScale;
        callScaleFUckback.Invoke();
    }

    public static void restartLevel()
    {
        playerScaleBracket = 0;
        callScaleFUckback.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        // when the object is removed, stop listening for scale changes
        callScaleFUckback.RemoveListener(OnScaleChange);
    }
}
