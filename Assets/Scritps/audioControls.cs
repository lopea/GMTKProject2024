using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControls : MonoBehaviour
{
    private static float volumeMod = .64f;

    public static bool isMuted = false;

    private static AudioSource aud;

    private static AudioSource bgm;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        bgm = transform.GetChild(0).GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            aud.volume = .24f * getVolumeMod();
            aud.Play();

            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.M))
            toggleMute();

        if (Input.GetKeyDown(KeyCode.Minus))
            changeVolume(-.1f);

        if (Input.GetKeyDown(KeyCode.Equals))
            changeVolume(.1f);
    }

    public static float getVolumeMod()
    {
        if (isMuted)
            return 0;
        else
            return volumeMod;
    }

    public static void toggleMute()
    {
        isMuted = !isMuted;

        // update music
        bgm.volume = getVolumeMod();
    }

    public static void changeVolume(float add)
    {
        volumeMod = Mathf.Clamp(volumeMod + add, 0, 1);

        bgm.volume = getVolumeMod();
    }
}
