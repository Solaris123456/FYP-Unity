using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SuccessRackCounter : MonoBehaviour
{
    public List<GameObject> selectedRacks = new List<GameObject>();
    public int RacksLeft = 0;
    public bool AllRacksDone = false;
    public UnityEvent racksfinished;
    
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
