using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuckFuckScaling : MonoBehaviour
{
    // static fields
    public static int playerScaleBracket = 1;
    private static UnityEngine.Events.UnityEvent callScaleFUckback = new UnityEngine.Events.UnityEvent();

    // easy access to the plane to attach to
    private static GameObject fuckPlane;

    // has gravity
    [SerializeField]
    private bool isKinetic = false;

    [SerializeField]
    private int scaleBracket;
    private Rigidbody rb;
    private ParticleSystem ptFx;
    private AudioSource aud;

    [SerializeField]
    private float vol = .25f;

    private void Awake()
    {
        fuckPlane = GameObject.FindGameObjectWithTag("planeController");
    }

    // Start is called before the first frame update
    void Start()
    {
        // when the player changes scales, this callback will tell all the listeners
        callScaleFUckback.AddListener(OnScaleChange);

        rb = GetComponent<Rigidbody>();
        ptFx = GetComponent<ParticleSystem>();
        aud = GetComponent<AudioSource>();

        // fix up the rb automaticall
        OnScaleChange();
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Alpha0))
            PlayerSetScale(0);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayerSetScale(1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayerSetScale(2);
        #endif
    }

    // callback function to be used when player changes scale
    private void OnScaleChange()
    {
        // if the same scale, check what it should be
        if (playerScaleBracket == scaleBracket)
        {
            rb.isKinematic = !isKinetic;
            rb.useGravity = isKinetic;
        }
        // if size bracket is different, make larger obj static and small obj kinetic
        else
        {
            if (playerScaleBracket < scaleBracket)
                rb.isKinematic = true;
            else
                rb.isKinematic = false;
            rb.useGravity = !rb.isKinematic;
        }

        if (rb.isKinematic)
            transform.SetParent(fuckPlane.transform);
        else
            transform.SetParent(null);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (ptFx)
            ptFx.Play();

        if (aud && !aud.isPlaying)
        {
            aud.pitch = Random.Range(.8f, 1.2f);
            aud.volume = vol * audioControls.getVolumeMod();
            aud.Play();
        }

        
    }

    public int GetScaleBracket()
    {
        return scaleBracket;
    }
}
