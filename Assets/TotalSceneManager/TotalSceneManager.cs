using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalSceneManager : MonoBehaviour
{
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

    private userStates currentState = userStates.sit;

    private float timer = 0f;

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
                //Set waiter animation controller values to trigger first animation
                //waiterAnimationController.
            }
            //Determine how this state is entered
            else if(currentState == userStates.waiterGreet)
            {
                waiterAudioController.PlayOneShot(audioLines[0]);
            }
        }
    }

    IEnumerator playAndwaitToStartRecording(AudioClip ac, int actionID)
    {
        waiterAudioController.PlayOneShot(audioLines[0]);
        yield return new WaitForSeconds(ac.length);
        if(actionID == 0)
        {
            openAIStuff.StartRecording(actionID);
        }
    }
}
