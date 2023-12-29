using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BeginScenarioFireScript : MonoBehaviour
{
    public GameObject[] FlammableTargets;
    public List<int> selectedOptions = new List<int>();
    public GameObject FireTick;
    public SuccessRackCounter SuccessRackCounter;
    //public GameObject ScenarioChecker;
    //public GameObject ManualPickedflag;
    //public GameObject ConfirmedInspectionflag;
    //public SuccessRackCounter RackCounter;
    public GameObject Rackcounter;
    public GameObject TimerStartFlag;
    public SafetyInjectInput safetyInjectInput;
    public int MinNoRacks = 1;
    public int MaxNoRacks = 4;
    public UnityEvent ExtraItemsToTrigger;
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
        //from here
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
        //to here
        //for stupid people you know

        for (int i = 0; i < numTargets; i++)
        {
            int selectedIndex;
            do { selectedIndex = Random.Range(0, FlammableTargets.Length); } while (selectedOptions.Contains(selectedIndex));
            selectedOptions.Add(selectedIndex);
        }
        //RNG

        for (int x = 0; x < selectedOptions.Count; x++)
        {
            int indexvalue = selectedOptions[x];
            Transform childObject = FlammableTargets[indexvalue].transform.Find("BurnFlag");
            if (childObject != null)
            {
                childObject.gameObject.SetActive(true);
                Debug.Log("Burn Flag activated");
            }
        }

        if (!FireTick.activeSelf)
        {
            FireTick.SetActive(true);
            Debug.Log("Fire Tick Triggered");
        }
        Debug.Log("Begin Burn Flag process");
        //Fire Flag set active

        Debug.Log("Process Started");

        if (!Rackcounter.activeSelf)
        {
            Rackcounter.SetActive(true);
        }
        if (!TimerStartFlag.activeSelf)
        {
            TimerStartFlag.SetActive(true);
        }
        //start timer

        if (!safetyInjectInput.activated)
        {
            safetyInjectInput.SafeInjectStart();
        }

        ExtraItemsToTrigger.Invoke();
    }

    
    /*
    public void FlameSelector()
    {
        selectedOptions.Clear();
        //from here
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
        //to here
        //for stupid people you know

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

        if (!Rackcounter.activeSelf)
        {
            Rackcounter.SetActive(true);
        }
        if (!TimerStartFlag.activeSelf)
        {
            TimerStartFlag.SetActive(true);
        }
        if (!safetyInjectInput.activated)
        {
            safetyInjectInput.SafeInjectStart();
        }
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
            SuccessRackCounter.selectedRacks.Add(childObject.gameObject);

            if (childObject != null)
            {
                childObject.gameObject.SetActive(true);
                Debug.Log("Burn Flag activated");
            }
        }
    }
    //unused code because it crashes unity every single time (not efficient)
    */
}
// text me if you still dont understand (Lang Wenbo / SP 2023 sem 2 / DARE/FT/3B/02)