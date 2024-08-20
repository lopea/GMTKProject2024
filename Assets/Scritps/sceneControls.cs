using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadMainlevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("fuckLevel");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
