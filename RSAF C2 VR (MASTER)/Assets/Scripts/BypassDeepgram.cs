using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.ShaderData;
using System.Collections;
//using static UnityEditor.Timeline.Actions.MenuPriority;
using BNG;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit.UI.BodyUI;

public class BypassDeepgram : MonoBehaviour
{
    private InputBridge input;
    //public Transform head;
    public float spawnDist = 2;
    public DeepgramInstance deepgramInstance;
    public bool buttonTrigger = false; // The button that activates the bypass
    public bool hold = false;
    public bool pressed = false;
    public float timer = 0;
    public float addtime = 0;
    public float checktime = 5;
    public OpenHandUI OpenHandUI;
    public GameObject menu; // bypass pop up
    //public GameObject bypassimgmenu; // bypass loading

    private void Start()
    {
        // Get a reference to the InputBridge component
        input = FindObjectOfType<InputBridge>();
    }

    void Update()
    {
        if (deepgramInstance.currentIndex2 < deepgramInstance.wordMatches.Length)
        {
            if (deepgramInstance.wordMatches[deepgramInstance.currentIndex2].detectFlag.activeSelf && !deepgramInstance.wordMatches[deepgramInstance.currentIndex2].audiocheck.isPlaying) // check if required flag is active
            {
                if (hold == false)
                {
                    timer += Time.deltaTime;
                }
                if (!hold && timer > deepgramInstance.wordMatches[deepgramInstance.currentIndex2].waiting)
                {
                    Debug.Log("bypass armed");
                    hold = true;
                    menu.SetActive(true);
                    //menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDist;
                    timer = 0;
                }

                if (hold)
                {
                    //menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
                    //menu.transform.forward *= -1;

                    if (!input.XButton && !pressed)
                    {
                        pressed = true;
                    }

                    if (input.XButton && pressed)
                    {
                        addtime += Time.deltaTime;

                        if (addtime > 2)
                        {
                            if (OpenHandUI.objectToActivate.activeSelf)
                            {
                                OpenHandUI.objectToActivate.SetActive(false);
                                OpenHandUI.buttoninput = true;
                                OpenHandUI.isButtonPressed = true;
                            }

                            //if (!bypassimgmenu.activeSelf)
                            {
                                //bypassimgmenu.SetActive(true);
                                //bypassimgmenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDist;
                            }
                            //bypassimgmenu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
                            //bypassimgmenu.transform.forward *= -1;
                        }
                        if (addtime > checktime)
                        {
                            pressed = false;
                            buttonTrigger = true;
                            addtime = 0;
                            //bypassimgmenu.SetActive(false); // deactive bypass loading
                            menu.SetActive(false); // deactive bypass pop up
                        }
                    }
                    else
                    {
                        addtime = 0;
                        //if (bypassimgmenu.activeSelf)
                        {
                            //bypassimgmenu.SetActive(false);
                        }

                    }

                }

                if (buttonTrigger == true)
                {
                    buttonTrigger = false;
                    ActivateBypass();
                    hold = false;
                }
            }

        }
    }
    public void ActivateBypass()
    {
        // Check if all previous elements have been activated
        deepgramInstance.wordMatches[deepgramInstance.currentIndex2].activated = true;
        if (AllPreviousElementsActivated(deepgramInstance.wordMatches, deepgramInstance.wordMatches[deepgramInstance.currentIndex2]))
        {
            // Invoke the UnityEvent associated with the current WordMatch object
            deepgramInstance.wordMatches[deepgramInstance.currentIndex2].method.Invoke();

            if (deepgramInstance.wordMatches[deepgramInstance.currentIndex2].singleUseOnly == false)
            {
                deepgramInstance.wordMatches[deepgramInstance.currentIndex2].activated = false;
                deepgramInstance.currentIndex2--;
                if (deepgramInstance.allDone == true)
                {
                    deepgramInstance.currentIndex2++;
                    deepgramInstance.wordMatches[deepgramInstance.currentIndex2].activated = true;
                    deepgramInstance.allDone = false;

                }
            }
            deepgramInstance.currentIndex2++; // Move to the next index
            Debug.Log(deepgramInstance.currentIndex2);
        }
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
}