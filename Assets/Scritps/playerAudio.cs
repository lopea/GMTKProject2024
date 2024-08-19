using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio : MonoBehaviour
{
    private static AudioClip[] audClips;
    private static int prevAudIndex = 0;

    private static AudioSource aud;

    [SerializeField]
    private const float minVol = .2f;

    [SerializeField]
    private const float minVolSpeed = 1.2f;

    [SerializeField]
    private const float maxVol = .8f;

    [SerializeField]
    private const float maxVolSpeed = 6f;


    // Start is called before the first frame update
    void Start()
    {
        audClips = Resources.LoadAll<AudioClip>("Audio/playerBall");
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        aud.clip = getRandSoundClip(collision.relativeVelocity.magnitude);
        aud.Play();
    }

    // returns an audio clip that wasn't played immediately previously
    private AudioClip getRandSoundClip(float speed)
    {
        aud.pitch = Random.Range(.8f, 1.2f);
        prevAudIndex = (prevAudIndex + Random.Range(1, audClips.Length)) % audClips.Length;

        // calculate volume
        if (speed < minVolSpeed)
            aud.volume = minVol;
        else if (speed > maxVolSpeed)
            aud.volume = maxVol;
        else
            aud.volume = maxVol * speed / maxVolSpeed;

        return audClips[prevAudIndex];
    }
}
