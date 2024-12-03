using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class TitleScreenManager : MonoBehaviour
{
    // Method to start the game by loading a scene
    public void StartGame()
    {
        SceneManager.LoadScene("temp"); 
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

