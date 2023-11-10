using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderData;
using System.Collections;

public class BypassDeepgram : MonoBehaviour
{
    public DeepgramInstance deepgramInstance;
    //public GameObject flagToCheck;// The button that activates the bypass
    //public int currentIndex = 0; // To keep track of the current index
    public bool buttonTrigger = false; // The button that activates the bypass
    public bool hold = true;
    float timer = 0;

    void Update()
    {
        //if (deepgramInstance.Bypass == true)
        if (!hold)
            timer += Time.deltaTime;
        if (!hold && timer > deepgramInstance.wordMatches[deepgramInstance.currentIndex2].waiting)
        {
            Debug.Log("Done waiting");
            hold = true;
            timer = 0;
        }

        if (buttonTrigger == true)
        {
            buttonTrigger = false;
            if (hold == true)
            {
                if (deepgramInstance.wordMatches[deepgramInstance.currentIndex2].detectFlag.activeSelf) // check if required flag is active
                {
                    //flagToCheck.acitveself.AddListener(ActivateBypass); // Add a listener to the button's click even
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