using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginScenarioFireScript1 : MonoBehaviour
{
    public GameObject[] FlammableTargets;
    private List<GameObject> selectedOptions = new List<GameObject>();
    public GameObject FireTick;
    public GameObject ScenarioChecker;
    public GameObject ManualPickedflag;
    public GameObject ConfirmedInspectionflag;

    // Start is called before the first frame update
    void Start()
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
        } */
    }

    public void FlameSelector()
    {
        selectedOptions.Clear();

        int numTargets = Random.Range(1, 1);

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
    }

    private IEnumerator ActivateTargets()
    {
        FireTick.SetActive(true);
        Debug.Log("Fire Tick Triggered");
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
