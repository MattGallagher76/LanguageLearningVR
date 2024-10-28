using OpenAI;
using Samples.Whisper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpanishToEnglishCheck : MonoBehaviour
{
    private OpenAIApi openai = new OpenAIApi("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");

    private readonly string fileName = "output.wav";
    private readonly int duration = 5;
    private AudioClip clip;
    private bool isRecording;
    private float time;

    public bool hasBeenDone = false;

    private List<string> promptList = new List<string>();

    private int promptIndex;

    public bool debugMode = false;

    // Start is called before the first frame update
    void Start()
    {
        promptList.Add("Does this text contain a typical greeting in spanish or in english. Repond with only yes or no.");
        promptList.Add("TODO1");
        promptList.Add("TODO2");
        promptList.Add("TODO3");
        promptList.Add("TODO4");
    }

    // Update is called once per frame
    void Update()
    {
        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !hasBeenDone)
            {
                hasBeenDone = true;
                StartRecording(0);
            }
            if (isRecording)
            {
                time += Time.deltaTime;

                if (time >= duration)
                {
                    time = 0;
                    isRecording = false;
                    EndRecording();
                }
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

        Debug.Log("res: " + res.Text);

        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = promptList[promptIndex] + "\n" + res.Text
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
        }
    }

    public void StartRecording(int actionID)
    {
        Debug.Log("started recording");
        isRecording = true;
        
        var index = PlayerPrefs.GetInt("user-mic-device-index");

        #if !UNITY_WEBGL
        clip = Microphone.Start(Microphone.devices[0], false, duration, 44100);
        #endif
    }

    public int sceneManagerCheck()
    {

        return 0;
    }
}
