using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatAudioScript : MonoBehaviour
{
    public GameObject ChecklistAudio;
    public GameObject ChecklistFlag;
    public bool complete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ChecklistFlag.activeSelf && complete == false)
        {
            ChecklistAudio.SetActive(true);
            complete = true;
        }
    }
}
