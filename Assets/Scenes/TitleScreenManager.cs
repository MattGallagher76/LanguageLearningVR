using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class TitleScreenManager : MonoBehaviour
{
    public AudioSource ac;
    public GameObject XRInteractionSetup;

    public bool changeScene = false;

    public void Awake()
    {
        DontDestroyOnLoad(XRInteractionSetup);
    }

    // Method to start the game by loading a scene
    public void StartGame()
    {
        ac.Play();
        DontDestroyOnLoad(XRInteractionSetup);
        SceneManager.LoadScene("temp"); 
    }

    private void Update()
    {
        if (changeScene)
            StartGame();
    }

    // Method to quit the application
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); 
#endif
    }
}

