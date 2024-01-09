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
        TimeSpan lightErrortimeSpan = TimeSpan.FromSeconds(lightErrorPenalty);
        string leTimeTaken = lightErrortimeSpan.ToString(@"mm\:ss");
        TimeSpan ceilingErrortimeSpan = TimeSpan.FromSeconds(ceilingErrorPenalty);
        string ceTimeTaken = ceilingErrortimeSpan.ToString(@"mm\:ss");
        TimeSpan fm200timeSpan = TimeSpan.FromSeconds(fm200CheckFailPenalty);
        string fmTimeTaken = fm200timeSpan.ToString(@"mm\:ss");

        TimeSpan ogtimeSpan = TimeSpan.FromSeconds(originalTimeTaken);
        TimeSpan fitimeSpan = TimeSpan.FromSeconds(finalTimeTaken);
        string ogTimeTaken = $"{ogtimeSpan.ToString(@"mm\:ss\.fff")}"; //ogtimeSpan.ToString(@"mm\:ss\.fff"); //previously: string ogTimeTaken = originalTimeTaken.ToString(@"mm\:ss\.fff"); 
        string fiTimeTaken = $"'{fitimeSpan.ToString(@"mm\:ss\.fff")}'"; //fitimeSpan.ToString(@"mm\:ss\.fff"); //previously: string fiTimeTaken = finalTimeTaken.ToString(@"mm\:ss\.fff");

        //string ogDisplayTimeTaken = $"{ogtimeSpan.ToString(@"mm\:ss")}";
        string fiDisplayTimeTaken = $"{fitimeSpan.ToString(@"mm\:ss\.fff")}";
        originalTimeTakenText.text = "Time Taken: " + ogTimeTaken + " minutes";
        finalTimeTakenText.text = "Final Time: " + fiDisplayTimeTaken + " minutes";
        lightErrorFoundText.text = "Light Error: " + (lightErrorFound ? "Error Found" : $"Penalty +{leTimeTaken} minutes");
        ceilingErrorFoundText.text = "Ceiling Error: " + (ceilingErrorFound ? "Error Found" : $"Penalty +{ceTimeTaken} minutes");
        fm200CheckFailText.text = "Fm200 Check: " + (Fm200CheckFail ? "Error Found" : $"Penalty +{fmTimeTaken} minutes");

        Debug.Log("#Number of users b4 savefinaltime " + GameManager.Instance.users.Count);
        // Get a reference to the Register script
        Register registerScript = FindObjectOfType<Register>();
        Debug.Log($"#Saving final time taken: {fiTimeTaken}");
        // Call the SaveFinalTimeTaken method with the finalTimeTaken as argument
        Debug.Log("#"+registerScript.gameObject.name);
        registerScript.SaveFinalTimeTaken(fiTimeTaken);
    }
}

