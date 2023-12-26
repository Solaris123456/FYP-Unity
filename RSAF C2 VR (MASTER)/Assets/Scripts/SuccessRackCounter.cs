using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SuccessRackCounter : MonoBehaviour
{
    //public List<GameObject> selectedRacks = new List<GameObject>();
    public BeginScenarioFireScript beginScenarioFireScript;
    public int RacksLeft = 0;
    public bool AllRacksDone = false;
    public UnityEvent racksfinished;
    
    public void SuccessRackCheck()
    {
        RacksLeft = 0;
        for (int racknum = 0; racknum < beginScenarioFireScript.selectedOptions.Count; racknum++)
        {
            int indexvalue = beginScenarioFireScript.selectedOptions[racknum];
            Transform childObject = beginScenarioFireScript.FlammableTargets[indexvalue].transform.Find("BurnFlag");
            if (childObject.gameObject.activeSelf)
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

    /*
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
    //rejected code due to efficiency issues
    */ 
    
}
// text me if you still dont understand (Lang Wenbo / SP 2023 sem 2 / DARE/FT/3B/02)
