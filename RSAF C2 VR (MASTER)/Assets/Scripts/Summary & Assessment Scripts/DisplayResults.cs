using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    public Text timeTakenText;
    public Text lightErrorFoundText;
    public Text wrongButtonPressedText;

    void Start()
    {
        Dictionary<string, string> results = new Dictionary<string, string>();
        string[] pairs = GameManager.Instance.SimulationResults.Split(',');
        foreach (string pair in pairs)
        {
            string[] parts = pair.Split('=');
            results.Add(parts[0], parts[1]);
        }
        
        float timeTaken = float.Parse(results["TimeTaken"]);
        bool lightErrorFound = bool.Parse(results["LightErrorFound"]);
        bool Fm200CheckFail = bool.Parse(results["Fm200CheckFail"]);

        timeTakenText.text = "Time Taken: " + timeTaken;
        lightErrorFoundText.text = "Light Error Found: " + (lightErrorFound ? "Yes" : "No");
        wrongButtonPressedText.text = "Fm200 Check Fail: " + (Fm200CheckFail ? "Yes" : "No");
    }
}
