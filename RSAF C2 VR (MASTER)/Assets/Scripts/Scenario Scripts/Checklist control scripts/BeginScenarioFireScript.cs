using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Valve.VR;

public class BeginScenarioFireScript : MonoBehaviour
{
    public GameObject[] FlammableTargets;
    public List<GameObject> selectedOptions = new List<GameObject>();
    public GameObject FireTick;
    //public GameObject ScenarioChecker;
    //public GameObject ManualPickedflag;
    //public GameObject ConfirmedInspectionflag;
    //public SuccessRackCounter RackCounter;
    public GameObject Rackcounter;
    public GameObject TimerStartFlag;
    public SafetyInjectInput safetyInjectInput;
    public int MinNoRacks = 1;
    public int MaxNoRacks = 4;

    // Start is called before the first frame update
    /*void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /* if (ManualPickedflag.activeSelf && ConfirmedInspectionflag.activeSelf && ScenarioChecker.activeSelf)
        {
            FlameSelector();
            Debug.Log("Senario Sequence has begun");
            ScenarioChecker.SetActive(false);
        } 
    }
    */

    public void FlameSelector()
    {
        selectedOptions.Clear();
        int numTargets;
        if (MinNoRacks >= MaxNoRacks)
        {
            numTargets = MinNoRacks;
        }
        else
        {
            numTargets = Random.Range(MinNoRacks, MaxNoRacks);
        }
        if (numTargets < 1)
        {
            numTargets = 1;
        }

        if (!TimerStartFlag.activeSelf)
        {
            TimerStartFlag.SetActive(true);
        }
        if (!safetyInjectInput.activated)
        {
            safetyInjectInput.SafeInjectStart();
        }

        List<GameObject> options = new List<GameObject>(FlammableTargets);

        for (int i = 0; i < numTargets; i++)
        {
            int selectedIndex = Random.Range(0, options.Count);
            GameObject selectedOption = options[selectedIndex];
            selectedOptions.Add(selectedOption);
            options.RemoveAt(selectedIndex);
        } 
        StartCoroutine(ActivateTargets());
        Debug.Log("Process Started");
        Rackcounter.SetActive(true);
    }

    private IEnumerator ActivateTargets()
    {
        if (!FireTick.activeSelf)
        {
            FireTick.SetActive(true);
            Debug.Log("Fire Tick Triggered");
        }
            yield return new WaitForSeconds(0);
            Debug.Log("Begin Burn Flag process");

        foreach (GameObject selectedOption in selectedOptions)
        {
            selectedOption.SetActive(true);

            // Check if the selectedOption has a child object named "ChildObject"
            Transform childObject = selectedOption.transform.Find("BurnFlag");
            if (childObject != null)
            {
                childObject.gameObject.SetActive(true);
                Debug.Log("Burn Flag activated");
            }
        }
    }
}
