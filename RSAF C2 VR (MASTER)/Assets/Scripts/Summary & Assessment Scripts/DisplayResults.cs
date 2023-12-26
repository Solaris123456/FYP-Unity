using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    public Text originalTimeTakenText;
    public Text finalTimeTakenText;
    public Text lightErrorFoundText;
    public Text fm200CheckFailText;
    public Text ceilingErrorFoundText;

    // Define your penalty times
    float lightErrorPenalty = 10f; // Replace with your actual penalty time
    float ceilingErrorPenalty = 20f; // Replace with your actual penalty time
    float fm200CheckFailPenalty = 30f; // Replace with your actual penalty time

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
        if (lightErrorFound) finalTimeTaken += lightErrorPenalty;
        if (ceilingErrorFound) finalTimeTaken += ceilingErrorPenalty;
        if (Fm200CheckFail) finalTimeTaken += fm200CheckFailPenalty;

        originalTimeTakenText.text = "Original Time Taken: " + originalTimeTaken;
        finalTimeTakenText.text = "Final Time Taken: " + finalTimeTaken;
        lightErrorFoundText.text = "Light Error Found: " + (lightErrorFound ? "Yes" : "Error was not found: Time Penalty Incurred");
        ceilingErrorFoundText.text = "Ceiling Error Found: " + (ceilingErrorFound ? "Yes" : "Error was not found: Time Penalty Incurred");
        fm200CheckFailText.text = "Fm200 Check Fail: " + (Fm200CheckFail ? "Yes" : "Error was not found: Time Penalty Incurred");

        // Get a reference to the Register script
        Register registerScript = FindObjectOfType<Register>();

        // Call the SaveFinalTimeTaken method with the finalTimeTaken as argument
        registerScript.SaveFinalTimeTaken(finalTimeTaken);
    }
}

