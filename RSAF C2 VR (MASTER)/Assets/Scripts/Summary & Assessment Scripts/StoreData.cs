using UnityEngine;
using UnityEngine.UI;

public class StoreData : MonoBehaviour
{
    public Button storeButton;
    public bool lightErrorFound; // public bool for light error found
    public bool Fm200CheckFail; // public bool for wrong button pressed

    void Start()
    {
        storeButton.onClick.AddListener(StoreSimulationResults);
    }

    void StoreSimulationResults()
    {
        float timeTaken = 1; // CalculateTimeTaken(); // The time taken for the simulation

        // Convert the float to a string
        string timeTakenString = timeTaken.ToString();

        // Store the results in the GameManager
        GameManager.Instance.SimulationResults = $"TimeTaken={timeTakenString},LightErrorFound={lightErrorFound},Fm200CheckFail={Fm200CheckFail}";
    }
}