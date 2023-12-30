using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    public TMP_Text originalTimeTakenText;
    public TMP_Text finalTimeTakenText;
    public TMP_Text lightErrorFoundText;
    public TMP_Text fm200CheckFailText;
    public TMP_Text ceilingErrorFoundText;

    // Define your penalty times
    public float lightErrorPenalty = 120; // Replace with your actual penalty time
    public float ceilingErrorPenalty = 120; // Replace with your actual penalty time
    public float fm200CheckFailPenalty = 120; // Replace with your actual penalty time

    void Start()
    {
        Dictionary<string, string> results = new Dictionary<string, string>();
        string[] pairs = GameManager.Instance.SimulationResults.Split(',');
        foreach (string pair in pairs)
        {
            string[] parts = pair.Split('=');
            results.Add(parts[0], parts[1]);
        }

        float originalTimeTaken = float.Parse(results["TimeTaken"]);
        bool lightErrorFound = bool.Parse(results["LightErrorFound"]);
        bool ceilingErrorFound = bool.Parse(results["CeilingErrorFound"]);
        bool Fm200CheckFail = bool.Parse(results["Fm200CheckFail"]);

        // Add penalty times based on the errors found
        float finalTimeTaken = originalTimeTaken;
        if (!lightErrorFound) finalTimeTaken += lightErrorPenalty;
        if (!ceilingErrorFound) finalTimeTaken += ceilingErrorPenalty;
        if (!Fm200CheckFail) finalTimeTaken += fm200CheckFailPenalty;

        //float lightErrorPenaltyString = lightErrorPenalty;
        //lightErrorPenaltyString

        //TimeSpan timeSpan = new TimeSpan((long)(originalTimeTaken * TimeSpan.TicksPerSecond));
        string ogTimeTaken = originalTimeTaken.ToString(); //previously: string ogTimeTaken = originalTimeTaken.ToString(@"mm\:ss\.fff"); 
        string fiTimeTaken = finalTimeTaken.ToString(); //previously: string fiTimeTaken = finalTimeTaken.ToString(@"mm\:ss\.fff");

        originalTimeTakenText.text = "Original Time Taken: " + ogTimeTaken;
        finalTimeTakenText.text = "Final Time Taken: " + fiTimeTaken;
        lightErrorFoundText.text = "Light Error Found: " + (lightErrorFound ? "Yes" : $"Error was not found: Time Penalty of {lightErrorPenalty} Incurred");
        ceilingErrorFoundText.text = "Ceiling Error Found: " + (ceilingErrorFound ? "Yes" : $"Error was not found: Time Penalty of {ceilingErrorPenalty} Incurred");
        fm200CheckFailText.text = "Fm200 Check Fail: " + (Fm200CheckFail ? "Yes" : $"Error was not found: Time Penalty of {fm200CheckFailPenalty} Incurred");

        // Get a reference to the Register script
        Register registerScript = FindObjectOfType<Register>();
        Debug.Log($"#Saving final time taken: {fiTimeTaken}");
        // Call the SaveFinalTimeTaken method with the finalTimeTaken as argument
        registerScript.SaveFinalTimeTaken(fiTimeTaken);
    }
}

