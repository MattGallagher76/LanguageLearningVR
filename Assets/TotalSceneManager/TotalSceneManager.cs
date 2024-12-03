using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalSceneManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI tmpDebug;

    public Animator acController;

    public string[] animationTriggers;

    public float waitForStartTimer;

    //bool hasStarted = false;

    private int currentState = 0;
    //0 Waiting to start
    //1 First audio line
    //2 Waiting for user to speak back

    public float timer = 0f;

    public Animator waiterAnimationController;
    public AudioSource waiterAudioController;

    public List<AudioClip> audioLines;

    public SpanishToEnglishCheck openAIStuff;
    //3 - I'm sorry, I don't understand

    //Interaction 1
    //0 - "Welcome! How are you? It's a pleasure to have you here.”

    //1 - That’s Great
    //2 - "That's great, I am also doing well.”

    //Interaction 2
    //4 - "What would you like to drink?”

    //5 - "Here's your drink."

    //Interaction 3
    //6 - "What do you want to eat?"

    //7 - "Here's your food, enjoy your meal."

    //Interaction 4
    //8 - “Here’s your check. Have a nice day.”


    public List<string> hintTextsOne;
    public List<string> hintTextsTwo;
    public List<string> hintTextsThree;
    public List<string> hintTextsFour;
    public List<string> hintTextsFive;

    public List<List<string>> hintTexts = new List<List<string>>();

    public List<TMPro.TextMeshProUGUI> hintTextObjects;
    public List<GameObject> hintBubbles;

    int currentHintCount = 0;
    int eventNumber = 0;

    public List<GameObject> drinks;
    public List<GameObject> food;

    public Vector3 drinkPosition;
    public Vector3 foodPosition;

    public AudioSource record;

    void Start()
    {
        hideAllHints();
        eventNumber = 0;
        hintTexts.Add(hintTextsOne);
        hintTexts.Add(hintTextsTwo);
        hintTexts.Add(hintTextsThree);
        hintTexts.Add(hintTextsFour);
        hintTexts.Add(hintTextsFive);
        timer = waitForStartTimer;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab pressed");
            startRecording();
        }
        if (timer >= 0)
        {
            //Debug.Log(timer);
            timer -= Time.deltaTime;
        }
        if (timer <= 0 && currentState == 0)
        {
            currentState = 1;
        }
        if (currentState == 1)
        {
            currentState = 2;
            //Maybe Light Up Button
            StartCoroutine(playAndwaitToStartRecording(audioLines[0], 0));
        }
        if (currentState == 3)
        {
            currentState = 4;
            StartCoroutine(playAndwaitToStartRecording(audioLines[4], 0));
        }
        if (currentState == 5)
        {
            currentState = 6;
            StartCoroutine(playAndwaitToStartRecording(audioLines[6], 0));
        }
        if (currentState == 7)
        {
            currentState = 8;
            StartCoroutine(playAndwaitToStartRecording(audioLines[8], 0));
        }
    }

    IEnumerator playAndwaitToStartRecording(AudioClip ac, int actionID)
    {
        Debug.Log("Playing: " + actionID);
        waiterAudioController.PlayOneShot(ac);
        if (actionID == 1)
        {
            hideAllHints();
        }
        if (actionID == 3 || actionID == 4 || actionID == 5)
        {
            hideAllHints();
        }
        if (actionID == 7 || actionID == 8 || actionID == 9)
        {
            hideAllHints();
        }
        yield return new WaitForSeconds(ac.length);
        if (actionID == 3)
        {
            GameObject gb = Instantiate(drinks[0]);
            gb.transform.position = drinkPosition;
        }
        if (actionID == 4)
        {
            GameObject gb = Instantiate(drinks[1]);
            gb.transform.position = drinkPosition;
        }
        if (actionID == 5)
        {
            GameObject gb = Instantiate(drinks[2]);
            gb.transform.position = drinkPosition;
        }
        if (actionID == 7)
        {
            GameObject gb = Instantiate(food[0]);
            gb.transform.position = foodPosition;
        }
        if (actionID == 8)
        {
            GameObject gb = Instantiate(food[1]);
            gb.transform.position = foodPosition;
        }
        if (actionID == 9)
        {
            GameObject gb = Instantiate(food[2]);
            gb.transform.position = foodPosition;
        }
        yield return new WaitForSeconds(2.5f);
        if (actionID == 1)
        {
            currentState = 3;
        }
        if (actionID == 2)
        {
            currentState = 1;
        }
        if (actionID == 3 || actionID == 4 || actionID == 5)
        {
            currentState = 5;
        }
        if (actionID == 7 || actionID == 8 || actionID == 9)
        {
            currentState = 7;
        }
        if (actionID == 6)
        {
            currentState = 3;
        }
        if (actionID == 10)
        {
            currentState = 5;
        }
        if (actionID == 11)
        {
            currentState = 7;
        }
    }

    public void startRecording()
    {
        if (currentState == 2 || currentState == 4 || currentState == 6 || currentState == 8)
        {
            Debug.Log("Requested a recording");
            record.Play();
            openAIStuff.StartRecording(currentState);
        }
        else
        {
            Debug.Log("Wrong state");
        }
    }

    public void response(int actionID)
    {
        Debug.Log("Response with actionID: " + actionID + " on: " + currentState);
        if (currentState == 2)
        {
            if (actionID == 1)
            {
                StartCoroutine(playAndwaitToStartRecording(audioLines[1], 1));
            }
            else if (actionID == 2)
            {
                StartCoroutine(playAndwaitToStartRecording(audioLines[2], 1));
            }
            else if (actionID == 3)
            {
                StartCoroutine(playAndwaitToStartRecording(audioLines[3], 2));
            }
            else
            {
                Debug.LogError("Did not get 1, 2, or 3: " + actionID);
                //tmpDebug.text += "Did not get 1, 2, or 3: " + actionID + "\n";
            }
        }
        if (currentState == 4)
        {
            if (actionID == 1)
            {
                //Coke
                StartCoroutine(playAndwaitToStartRecording(audioLines[5], 3));
            }
            else if (actionID == 2)
            {
                //Water
                StartCoroutine(playAndwaitToStartRecording(audioLines[5], 4));
            }
            else if (actionID == 3)
            {
                //Juice
                StartCoroutine(playAndwaitToStartRecording(audioLines[5], 5));
            }
            else if (actionID == 4)
            {
                StartCoroutine(playAndwaitToStartRecording(audioLines[3], 6));
            }
            else
            {
                Debug.LogError("Did not get 1, 2, 3, or 4: " + actionID);
                //tmpDebug.text += "Did not get 1, 2, or 3: " + actionID + "\n";
            }
        }
        if (currentState == 6)
        {
            if (actionID == 1)
            {
                //Enchilada
                StartCoroutine(playAndwaitToStartRecording(audioLines[7], 7));
            }
            else if (actionID == 2)
            {
                //Taco
                StartCoroutine(playAndwaitToStartRecording(audioLines[7], 8));
            }
            else if (actionID == 3)
            {
                //Burrito
                StartCoroutine(playAndwaitToStartRecording(audioLines[7], 9));
            }
            else if (actionID == 4)
            {
                StartCoroutine(playAndwaitToStartRecording(audioLines[3], 10));
            }
            else
            {
                Debug.LogError("Did not get 1, 2, 3, or 4: " + actionID);
                //tmpDebug.text += "Did not get 1, 2, or 3: " + actionID + "\n";
            }
        }
        if (currentState == 8)
        {
            if (actionID == 1)
            {
                FindAnyObjectByType<FadeToBlack>().fade();
            }
            else if (actionID == 2)
            {
                StartCoroutine(playAndwaitToStartRecording(audioLines[3], 8));
            }
            else
            {
                Debug.LogError("Did not get 1 or 2: " + actionID);
                //tmpDebug.text += "Did not get 1, 2, or 3: " + actionID + "\n";
            }
        }
    }

    public void showHint()
    {
        if (currentHintCount == 0)
        {
            hintBubbles[0].SetActive(true);
            hintTextObjects[currentHintCount].text = "The waiter said: " + hintTexts[eventNumber][0].Replace("\"", "");
            currentHintCount++;
        }
        else if (currentHintCount == 1)
        {
            hintBubbles[1].SetActive(true);
            hintTextObjects[currentHintCount].text = "That translates to: " + hintTexts[eventNumber][1].Replace("\"", "");
            currentHintCount++;
        }
        else if (currentHintCount == 2)
        {
            hintBubbles[2].SetActive(true);
            hintTextObjects[currentHintCount].text = ("You should say: " + hintTexts[eventNumber][2] + "Which translates to: " + hintTexts[eventNumber][3]).Replace("\"", "");
            currentHintCount++;
        }
    }

    public void hideAllHints()
    {
        hintBubbles[0].SetActive(false);
        hintBubbles[0].SetActive(false);
        hintBubbles[0].SetActive(false);
        hintTextObjects[0].text = "";
        hintTextObjects[1].text = "";
        hintTextObjects[2].text = "";
        eventNumber++;
    }

}
