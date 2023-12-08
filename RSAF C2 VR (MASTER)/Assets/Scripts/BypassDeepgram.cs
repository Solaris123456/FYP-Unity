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
    private InputBridge input; // button input bridge (database of all buttons)
    public DeepgramInstance deepgramInstance; // voice recognitions script
    public bool buttonTrigger = false; // trigger that activates the bypass
    public bool hold = false; // prevents bypassing before timer 1 is up
    public bool pressed = false; // detects when the button was released (so that we know when the player pressed the button)
    public float timer = 0; // timer 1 for pause time before showing bypass UI
    public float addtime = 0; // timer 2 for press and hold time (how long the bypass button was held)
    public float checktime = 3; // press and hold time b4 bypass
    public OpenHandUI OpenHandUI; // wrist menu script
    //public GameObject bypassimgmenu; // bypass loading

    private void Start()
    {
        // Get a reference to the InputBridge component
        input = FindObjectOfType<InputBridge>();
    }

    void Update()
    {
        if (deepgramInstance.currentIndex2 < deepgramInstance.wordMatches.Length) // make sure current index doesn't exceed the max number of element that can be invoked
        {
            if (deepgramInstance.wordMatches[deepgramInstance.currentIndex2].detectFlag.activeSelf && !deepgramInstance.wordMatches[deepgramInstance.currentIndex2].audiocheck.isPlaying) // check if required flag is active and audio has stopped playing
            {
                // timer 1
                if (hold == false)
                {
                    timer += Time.deltaTime;
                }
                if (!hold && timer > deepgramInstance.wordMatches[deepgramInstance.currentIndex2].waiting) 
                {
                    Debug.Log("bypass armed");
                    hold = true;
                    deepgramInstance.wordMatches[deepgramInstance.currentIndex2].bypassUiPopup.SetActive(true);
                    timer = 0;
                }

                if (hold)
                {
                    // button press detection (prevents user from bypassing everything by just holding the button all the way to the end)
                    if (!input.XButton && !pressed)
                    {
                        pressed = true; // button was released
                    }

                    if (input.XButton && pressed) // button was pressed/held, timer 2 starts
                    {
                        addtime += Time.deltaTime;

                        if (addtime > 2) // if button held more than 2 seconds
                        {
                            if (OpenHandUI.objectToActivate.activeSelf) // if wrist menu is active, close it
                            {
                                OpenHandUI.objectToActivate.SetActive(false);
                                OpenHandUI.buttoninput = true;
                                OpenHandUI.isButtonPressed = true;
                            }
                        }
                        if (addtime > checktime) // if timer 2 is up
                        {
                            pressed = false; // button will no longer work until released again
                            buttonTrigger = true; // bypassed
                            addtime = 0; // timer 2 reset
                            //bypassimgmenu.SetActive(false); // deactive bypass loading
                            deepgramInstance.wordMatches[deepgramInstance.currentIndex2].bypassUiPopup.SetActive(false); // deactive bypass pop up
                        }
                    }
                    else
                    {
                        addtime = 0;

                        /* if (bypassimgmenu.activeSelf)
                        {
                            bypassimgmenu.SetActive(false); // deactvate bypass loading menu
                        } */

                    }

                }

                if (buttonTrigger == true)
                {
                    buttonTrigger = false; // deactive bypass trigger to prevent continuous bypass
                    ActivateBypass(); // bypass script
                    hold = false; // timer 1 may start again
                }
            }
            else // for instruction audio skip
            {
                if (!input.XButton && !pressed) // same idea
                {
                    pressed = true;
                }
                if (input.XButton && pressed) // same idea except no holding time
                {
                    // stops audio if it is playing when button was pressed
                    if (deepgramInstance.wordMatches[deepgramInstance.currentIndex2].audiocheck.isPlaying)
                    {
                        deepgramInstance.wordMatches[deepgramInstance.currentIndex2].audiocheck.Stop();
                    }
                    pressed = false;
                }
            }

        }
    }
    public void ActivateBypass()
    {
        // Check if all previous elements have been activated
        deepgramInstance.wordMatches[deepgramInstance.currentIndex2].activated = true; //the current index was activated
        if (AllPreviousElementsActivated(deepgramInstance.wordMatches, deepgramInstance.wordMatches[deepgramInstance.currentIndex2]))
        {
            // Invoke (bypass) the UnityEvent associated with the current WordMatch method element
            deepgramInstance.wordMatches[deepgramInstance.currentIndex2].method.Invoke();

            if (deepgramInstance.wordMatches[deepgramInstance.currentIndex2].singleUseOnly == false) // if the method element is multiple use
            {
                deepgramInstance.wordMatches[deepgramInstance.currentIndex2].activated = false; // reset the currentindex active status
                deepgramInstance.currentIndex2--; // go back i want to be monkey
                if (deepgramInstance.allDone == true) // if the current wordmatch method is finally done repeating
                {
                    deepgramInstance.currentIndex2++; // next method
                    deepgramInstance.wordMatches[deepgramInstance.currentIndex2].activated = true; // is finally done fr this time
                    deepgramInstance.allDone = false; // reset all done so that the next element that needs to repeat can repeat

                }
            }
            deepgramInstance.currentIndex2++; // Next
            Debug.Log(deepgramInstance.currentIndex2);
        }
    }

    // honestly Idk what this is but we just have this cause the previous group used lol
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