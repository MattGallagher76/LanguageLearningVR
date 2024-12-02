using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel; // Reference to the tutorial panel

    // Show the tutorial panel
    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    // Hide the tutorial panel
    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}
