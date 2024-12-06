using OpenAI;
using Samples.Whisper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpanishToEnglishCheck : MonoBehaviour
{

    //3 - I'm sorry, I don't understand

    //Interaction 1
    //0 - "Welcome! How are you? It's a pleasure to have you here.�

    //1 - That�s Great
    //2 - "That's great, I am also doing well.�

    //Interaction 2
    //4 - "What would you like to drink?�

    //5 - "Here's your drink."

    //Interaction 3
    //6 - "What do you want to eat?"

    //7 - "Here's your food, enjoy your meal."

    //Interaction 4
    //8 - �Here�s your check. Have a nice day.�

    private OpenAIApi openai = new OpenAIApi("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");

    private readonly string fileName = "output.wav";
    private readonly int duration = 5;
    private AudioClip clip;
    private bool isRecording = false;
    private float time;

    public bool hasBeenDone = false;

    private List<string> promptList = new List<string>();

    private int promptIndex;

    public bool debugMode = false;

    int currentActionID = -1;

    TotalSceneManager tsm;

    // Start is called before the first frame update
    void Start()
    {
        tsm = FindAnyObjectByType<TotalSceneManager>();
        promptList.Add("Respond with only a number as described here: 1 - If the provided text is an appropriate response to �Welcome! How are you? It's a pleasure to have you here.� but does not contain a question which can be summarized as �How are you doing?� 2 - If the provided text is an appropriate response to �Welcome! How are you? It's a pleasure to have you here. � and contain a question which can be summarized as �How are you doing?� 3 - If the provided text is not an appropriate response to contain a question which can be summarized as �how are you doing?� Provided text:");
        promptList.Add("Respond with only a number as described here: 1 � If the provided text contains a request for a coke. 2 - If the provided text contains a request for water. 3 � If the provided text contains a request for juice. 4 � If the provided text does not contain a request for any of the above items.");
        promptList.Add("Respond with only a number as described here: 1 � If the provided text contains a request for enchiladas. 2 � If the provided text contains a request for tacos. 3 � If the provided text contains a request for burritos. 4 � If the provided text contains no request for the above items.");
        promptList.Add("Respond with only a number as described here: 1 � If the provided text contains an appropriate response to �Have a nice day�. 2 � If the provided text does not contain an appropriate response to �Have a nice day�.");
    }

    // Update is called once per frame
    void Update()
    {
        if (isRecording)
        {
            Debug.Log("Recording...");
            time += Time.deltaTime;

            if (time >= duration)
            {
                time = 0;
                isRecording = false;
                EndRecording();
            }
        }
    }

    private async void EndRecording()
    {
        Debug.Log("ended recording");
#if !UNITY_WEBGL
        Microphone.End(null);
#endif

        byte[] data = SaveWav.Save(fileName, clip);

        var req = new CreateAudioTranslationRequest
        {
            FileData = new FileData() { Data = data, Name = "audio.wav" },
            // File = Application.persistentDataPath + "/" + fileName,
            Model = "whisper-1",
        };
        var res = await openai.CreateAudioTranslation(req);

        //Interaction 1
        if (currentActionID == 2)
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = promptList[0] + "\n" + res.Text
            };

            List<ChatMessage> messages = new List<ChatMessage>();
            messages.Add(newMessage);

            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-4o-mini",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                Debug.Log("Message: " + message.Content);
                Debug.Log("Done");

                if (message.Content.Contains("1"))
                {
                    tsm.response(1);
                }
                if (message.Content.Contains("2"))
                {
                    tsm.response(2);
                }
                if (message.Content.Contains("3"))
                {
                    tsm.response(3);
                }
                else
                {
                    tsm.response(0);
                }
            }
            else
            {
                Debug.Log("error");
            }
        }
        //Interaction 2
        if (currentActionID == 4)
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = promptList[1] + "\n" + res.Text
            };

            List<ChatMessage> messages = new List<ChatMessage>();
            messages.Add(newMessage);

            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-4o-mini",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                Debug.Log("Message: " + message.Content);
                Debug.Log("Done");

                if (message.Content.Contains("1"))
                {
                    tsm.response(1);
                }
                if (message.Content.Contains("2"))
                {
                    tsm.response(2);
                }
                if (message.Content.Contains("3"))
                {
                    tsm.response(3);
                }
                if (message.Content.Contains("4"))
                {
                    tsm.response(4);
                }
                else
                {
                    tsm.response(0);
                }
            }
            else
            {
                Debug.Log("error");
            }
        }
        //Interaction 3
        if (currentActionID == 6)
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = promptList[2] + "\n" + res.Text
            };

            List<ChatMessage> messages = new List<ChatMessage>();
            messages.Add(newMessage);

            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-4o-mini",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                Debug.Log("Message: " + message.Content);
                Debug.Log("Done");

                if (message.Content.Contains("1"))
                {
                    tsm.response(1);
                }
                if (message.Content.Contains("2"))
                {
                    tsm.response(2);
                }
                if (message.Content.Contains("3"))
                {
                    tsm.response(3);
                }
                if (message.Content.Contains("4"))
                {
                    tsm.response(4);
                }
                else
                {
                    tsm.response(0);
                }
            }
            else
            {
                Debug.Log("error");
            }
        }
        //Interaction 3
        if (currentActionID == 6)
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = promptList[3] + "\n" + res.Text
            };

            List<ChatMessage> messages = new List<ChatMessage>();
            messages.Add(newMessage);

            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-4o-mini",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                Debug.Log("Message: " + message.Content);
                Debug.Log("Done");

                if (message.Content.Contains("1"))
                {
                    tsm.response(1);
                }
                if (message.Content.Contains("2"))
                {
                    tsm.response(2);
                }
                else
                {
                    tsm.response(0);
                }
            }
            else
            {
                Debug.Log("error");
            }
        }
    }

    public void StartRecording(int actionID)
    {
        currentActionID = actionID;
        Debug.Log("started recording");
        isRecording = true;

        var index = PlayerPrefs.GetInt("user-mic-device-index");

#if !UNITY_WEBGL
        clip = Microphone.Start(Microphone.devices[0], false, duration, 44100);
#endif
    }
}
