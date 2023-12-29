using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class SuccessRackCounter : MonoBehaviour
{
    //public List<GameObject> selectedRacks = new List<GameObject>();
    public BeginScenarioFireScript[] beginScenarioFireScript;
    public AudioSource RacksNotDoneAudio;
    public int RacksLeft = 0;
    public bool AllRacksDone = false;
    public UnityEvent racksfinished;

    public void SuccessRackCheck()
    {
        RacksLeft = 0;
        for (int i = 0; i < beginScenarioFireScript.Length; i++)
        {
            for (int racknum = 0; racknum < beginScenarioFireScript[i].selectedOptions.Count; racknum++)
            {
                int indexvalue = beginScenarioFireScript[i].selectedOptions[racknum];
                Transform childObject = beginScenarioFireScript[i].FlammableTargets[indexvalue].transform.Find("BurnFlag");
                if (childObject.gameObject.activeSelf)
                {
                    RacksLeft++;
                    Debug.Log("RACK NUMBER " + indexvalue + " Not shutting down");
                }
            }
        }
            if (RacksLeft == 0 && AllRacksDone == false)
            {
                AllRacksDone = true;
                racksfinished.Invoke();
            }
            if (RacksLeft != 0 && AllRacksDone == false)
            {
                RacksNotDoneAudio.Play();
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
