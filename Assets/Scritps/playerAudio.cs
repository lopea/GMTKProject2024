using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio : MonoBehaviour
{
    private AudioClip[] audClips;
    private int prevAudIndex = 0;

    private AudioSource aud;

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
        aud.clip = getRandSoundClip();
        aud.Play();
    }

    // returns an audio clip that wasn't played immediately previously
    private AudioClip getRandSoundClip()
    {
        aud.pitch = Random.Range(.8f, 1.2f);
        prevAudIndex = (prevAudIndex + Random.Range(1, audClips.Length)) % audClips.Length;

        return audClips[prevAudIndex];
    }
}
