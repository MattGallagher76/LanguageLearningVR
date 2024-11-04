using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalSceneManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI tmpDebug;

    public Animator acController;

    public string[] animationTriggers;

    enum userStates
    {
        sit, waiterGreet, greetRepond, waiterDrinkOrder, drinkOrderRespond,
        waiterLeaveMenu, menuReading, waiterFoodOrder, foodOrderRespond, 
        waiterBringFood, eat, waiterBringCheck, payCheck, directions, 
        useBathroom, leaves
    };

    public float waitForStartTimer;

    bool hasStarted = false;

    private userStates currentState = userStates.sit;

    public float timer = 0f;

    public Animator waiterAnimationController;
    public AudioSource waiterAudioController;

    public List<AudioClip> audioLines;

    public SpanishToEnglishCheck openAIStuff;
    //0 - "Hello how are you"

    //1 - "Good, can I get you a drink?
    //2 - "I'm well, can I get you a drink?"

    //3 - "I'm sorry, I don't know what you mean?"

    void Start()
    {
        timer = waitForStartTimer;
    }

    void FixedUpdate()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            if(currentState == userStates.sit)
            {
                currentState = userStates.waiterGreet;
                //Set waiter animation controller values to trigger first animation
                //waiterAnimationController.
            }
            //Determine how this state is entered
            else if(currentState == userStates.waiterGreet && !hasStarted)
            {
                hasStarted = true;
                Debug.Log("Starting co");
                tmpDebug.text += "Starting co\n";
                StartCoroutine(playAndwaitToStartRecording(audioLines[0], 0));
            }
        }
    }

    IEnumerator playAndwaitToStartRecording(AudioClip ac, int actionID)
    {
        waiterAudioController.PlayOneShot(audioLines[0]);
        yield return new WaitForSeconds(ac.length);
        if(actionID == 0)
        {
            Debug.Log("Started recording");
            tmpDebug.text += "Started recording\n";
            openAIStuff.StartRecording(actionID);
        }
    }

    public void response(int actionID)
    {
        if(currentState == userStates.waiterGreet)
        {
            if(actionID == 1)
            {
                waiterAudioController.PlayOneShot(audioLines[1]);
            }
            else if (actionID == 2)
            {
                waiterAudioController.PlayOneShot(audioLines[2]);
            }
            else if (actionID == 3)
            {
                waiterAudioController.PlayOneShot(audioLines[3]);
                hasStarted = false;
                timer = waitForStartTimer;
            }
            else
            {
                Debug.Log("Did not get 1, 2, or 3: " + actionID);
                tmpDebug.text += "Did not get 1, 2, or 3: " + actionID + "\n";
            }
        }
    }

    public void addText(string str)
    {
        tmpDebug.text += str + "\n";
    }
}
