using UnityEngine;
using UnityEngine.UI;

public class StoreData : MonoBehaviour
{
    public SummarizerForC2 summarizerForC2;
    //public Button storeButton; //???
    public bool lightErrorFound; // public bool for light error found
    public bool ceilingErrorFound;
    public bool Fm200CheckFail; // public bool for wrong button pressed
    public float timeTaken;
    void Start()
    {
        if (!summarizerForC2.trainingMode)
        {
            timeTaken = summarizerForC2.FinalTime;
            lightErrorFound = summarizerForC2.LightCheck;
            ceilingErrorFound = summarizerForC2.CeilingCheck;
            Fm200CheckFail = summarizerForC2.Fm200;
            StoreSimulationResults();
        }
    }

    void StoreSimulationResults()
    {
        // CalculateTimeTaken(); // The time taken for the simulation

        // Convert the float to a string
        string timeTakenString = timeTaken.ToString();

        // Store the results in the GameManager
        GameManager.Instance.SimulationResults = $"TimeTaken={timeTakenString},LightErrorFound={lightErrorFound},CeilingErrorFound={ceilingErrorFound},Fm200CheckFail ={Fm200CheckFail}";
    }
}