using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;
using System.IO;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class AccountManager : MonoBehaviour
{
    public Button csvButton;
    public TMP_Text messageText;
    void Start()
    {
        csvButton.onClick.AddListener(OpenCSVFile);
    }

    void OpenCSVFile()
    {
        string path = Application.dataPath + "/Accounts.csv";
        print(path);
        if (!File.Exists(path))
        {
            messageText.text = "File not found";
            StartCoroutine(ClearMessageAfterDelay(3f));

            UnityEngine.Debug.Log("File not found: " + path);
            return;
        }
        // Open the CSV file
        try
        {
            Process.Start(path);
        }
        catch (Exception e)
        {
            messageText.text = "Failed to open CSV";
            StartCoroutine(ClearMessageAfterDelay(3f));
            UnityEngine.Debug.Log("Failed to open CSV file: " + e.Message);
        }

        IEnumerator ClearMessageAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            messageText.text = "";
        }
        // Quit the application
        Application.Quit();
        
    }
}