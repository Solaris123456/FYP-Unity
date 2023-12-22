using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BeginScenarioScripts
{
    public BeginScenarioFireScript beginScenarioFireScript;
}
public class SuccessRackCounter : MonoBehaviour
{
    public BeginScenarioScripts[] beginScenarioScripts;
    public List<GameObject> selectedRacks = new List<GameObject>();
    public int RacksLeft = 0;
    public bool AllRacksDone = false;
    public UnityEvent racksfinished;
    // Start is called before the first frame update
    void Start()
    {
        selectedRacks.Clear();
        for (int i = 0; i < beginScenarioScripts.Length; i++)
        {
            for (int x = 0; x < beginScenarioScripts[i].beginScenarioFireScript.selectedOptions.Count; x++)
            {
                selectedRacks.Add(beginScenarioScripts[i].beginScenarioFireScript.selectedOptions[x]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RacksLeft = 0;
        for (int racknum = 0; racknum < selectedRacks.Count; racknum++)
        {
            if (selectedRacks[racknum].activeSelf)
            {
                RacksLeft++;
            }
        }
        if (RacksLeft == 0 && AllRacksDone == false)
        {
            AllRacksDone = true;
            racksfinished.Invoke();
        }
    }
}
