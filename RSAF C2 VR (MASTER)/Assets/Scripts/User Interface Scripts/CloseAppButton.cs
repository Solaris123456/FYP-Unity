using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseAppButton : MonoBehaviour
{
    public Button closeButton;

    void Start()
    {
        closeButton.onClick.AddListener(CloseApplication);
    }

    void CloseApplication()
    {
        Application.Quit();
    }
}
