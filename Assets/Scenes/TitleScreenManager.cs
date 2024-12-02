using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class TitleScreenManager : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("temp"); 
    }

   
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the editor
#else
        Application.Quit(); // Quits the application
#endif
    }
}

