using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolsBobs : MonoBehaviour
{
    // Start is called before the first frame update
    public bool toggling;
    public GameObject scoreboard;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setBool()
    {
        if (toggling == true)
        {
            toggling = false;
            scoreboard.SetActive(false);
        }
        else
        {
            toggling = true;
            scoreboard.SetActive(true);
        }
    }
}
