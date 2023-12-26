using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

using NativeWebSocket;
using UnityEngine.Events;
//using System.Linq;
//using static UnityEditor.Timeline.Actions.MenuPriority;
//using UnityEditor.Presets;

[System.Serializable]
public class DeepgramResponse
{
    public int[] channel_index;
    public bool is_final;
    public Channel channel;
}

[System.Serializable]
public class Channel
{
    public Alternative[] alternatives;
}

[System.Serializable]
public class Alternative
{
    public string transcript;
}

[System.Serializable]
public class WordMatch
{
    public AudioSource audiocheck; // check if audio source is playing
    public float waiting = 2; // waiting time before bypass menu pop up
    public GameObject detectFlag; // currentflag active status detection
    public string[] words;
    // public string bypassUIWords; // bypass menu words 
    public int matchedCount;
    public bool activated;
    public bool singleUseOnly; // Added field to indicate if the element is single use only
    public UnityEvent method; // Added field to store the corresponding UnityEvent
    public GameObject bypassUiPopup; // bypass pop up
}

public class DeepgramInstance : MonoBehaviour
{
    public bool allDone = false; // for repeating elements
    public int currentIndex2 = 0; // To keep track of the current index
    public bool Bypass; // will only be true if only bypass is available (deepgram connection lost/fail)
    public BypassDeepgram bypassScript; // BypassDeepgram script
    WebSocket websocket; // Deepgram
    public WordMatch[] wordMatches; // Changed the type to WordMatch array

    public float tolerance;

    [SerializeField]
    private bool enabledWords = false;

    public bool EnabledWords
    {
        get { return enabledWords; }
        set { enabledWords = value; }
    }

    async void Start()
    {
        foreach (var wordMatch in wordMatches)
        {
            wordMatch.matchedCount = 0;
            wordMatch.activated = false;
        }

        var headers = new Dictionary<string, string>
        {
            { "Authorization", "Token 91cb8a5abb4ca5acc6bea15a3e561ba310facdd5" }
        };
        websocket = new WebSocket("wss://api.deepgram.com/v1/listen?encoding=linear16&sample_rate=" + AudioSettings.outputSampleRate.ToString(), headers);

        websocket.OnOpen += () =>
        {
            Debug.Log("Connected to Deepgram!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error: " + e);
            Bypass = true;
            //bypassScript.ActivateBypass();
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
            Bypass = true;
            //bypassScript.ActivateBypass();
        };

        websocket.OnMessage += (bytes) =>
        {
            
                var message = System.Text.Encoding.UTF8.GetString(bytes);
                Debug.Log("OnMessage: " + message);
                DeepgramResponse deepgramResponse = JsonUtility.FromJson<DeepgramResponse>(message);
                if (deepgramResponse.is_final)
                {
                    var transcript = deepgramResponse.channel.alternatives[0].transcript;
                    Debug.Log(transcript);

                    string[] words = transcript.Split(' ');

                    int wordsToMatch = Mathf.CeilToInt(words.Length * tolerance);

                    // Reset activated flag for all word matches
                    foreach (var wordMatch in wordMatches)
                    {
                        wordMatch.matchedCount = 0;
                        //wordMatch.activated = false;
                    }

                    for (int i = 0; i < words.Length; i++)
                    {
                        string word = words[i];

                        if (currentIndex2 < wordMatches.Length)
                        {
                            if (EnabledWords && !wordMatches[currentIndex2].audiocheck.isPlaying)
                            {
                                /*
                                foreach (var wordMatch in wordMatches)
                                {
                                    if (!wordMatch.activated && Array.Exists(wordMatch.words, w => w == word))
                                    {
                                        wordMatch.matchedCount++;

                                        if (wordMatch.matchedCount >= wordsToMatch)
                                        {
                                            wordMatch.activated = true;

                                            // Invoke the method only if all previous elements have been activated
                                            if (AllPreviousElementsActivated(wordMatches, wordMatch))
                                            {
                                                wordMatch.method.Invoke();
                                                //wordMatches[currentIndex2].method.Invoke();
                                                currentIndex2++;
                                                Debug.Log("works" + (Array.IndexOf(wordMatches, wordMatch) + 1));

                                                if (wordMatch.singleUseOnly)
                                                {
                                                    // Remove the word match from the array to prevent further invocation
                                                    wordMatches = wordMatches.Where(match => match != wordMatch).ToArray();
                                                }
                                            }
                                        }

                                        break;
                                    }
                                }
                                */

                                if (!wordMatches[currentIndex2].activated && Array.Exists(wordMatches[currentIndex2].words, w => w == word))
                                {
                                    wordMatches[currentIndex2].matchedCount++;

                                    if (wordMatches[currentIndex2].matchedCount >= wordsToMatch)
                                    {
                                        wordMatches[currentIndex2].activated = true;

                                        // Invoke the method only if all previous elements have been activated
                                        if (AllPreviousElementsActivated(wordMatches, wordMatches[currentIndex2]))
                                        {
                                            wordMatches[currentIndex2].method.Invoke();
                                            //wordMatches[currentIndex2].method.Invoke();
                                            Debug.Log("works" + (Array.IndexOf(wordMatches, wordMatches[currentIndex2]) + 1));

                                            if (!wordMatches[currentIndex2].singleUseOnly)
                                            {
                                                wordMatches[currentIndex2].activated = false;
                                                currentIndex2--;
                                            }
                                            currentIndex2++;
                                            bypassScript.pressed = false;
                                            bypassScript.hold = false;
                                            bypassScript.addtime = 0;
                                            bypassScript.timer = 0;
                                            //bypassimgmenu.SetActive(false); // deactive bypass loading
                                            wordMatches[currentIndex2].bypassUiPopup.SetActive(false); // deactive bypass pop up
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }
            
        
        };

        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    public async void ProcessAudio(byte[] audio)
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.Send(audio);
        }
    }

    public void EnableWords()
    {
        EnabledWords = true;
    }

    public void DisableWords()
    {
        EnabledWords = false;
    }

    private bool AllPreviousElementsActivated(WordMatch[] wordMatches, WordMatch currentMatch)
    {
        int currentIndex = Array.IndexOf(wordMatches, currentMatch);

        for (int i = 0; i < currentIndex; i++)
        {
            if (!wordMatches[i].activated)
            {
                return false;
            }
        }

        return true;
    }

    public void DoAllDone()
    {
        allDone = true;
        if (allDone == true)
        {
            wordMatches[currentIndex2].activated = true;
            allDone = false;
            currentIndex2++;
        }
    }
}