using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessRackCounter : MonoBehaviour
{
    public GameObject RackCounter;
    public int SuccessfulRackCount = 0;
    public int RacksToCount = 3;
    public GameObject AllRacksDone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!RackCounter.activeSelf && SuccessfulRackCount < RacksToCount)
        {
            SuccessfulRackCount++;
            RackCounter.SetActive(true);
        }
        if (SuccessfulRackCount == RacksToCount)
        {
            AllRacksDone.SetActive(true);
        }
    }
}
