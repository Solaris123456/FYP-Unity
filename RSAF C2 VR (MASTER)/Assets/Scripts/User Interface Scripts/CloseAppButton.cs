using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CloseAppButton : MonoBehaviour
{
    public Button closeButton;
    public TMP_Text messageText;
    void Start()
    {
        //closeButton.onClick.AddListener(CloseApplication);
    }

    public void CloseApplication()
    {

        messageText.text = "Quitting Simulator";
        StartCoroutine(ClearMessageAfterDelay(3f));


        Application.Quit();
        IEnumerator ClearMessageAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            messageText.text = "";
        }
    }
}
