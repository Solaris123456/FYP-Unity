using System.Collections;
using System.Collections.Generic;
using UnityEditor.EventSystems;
using UnityEngine;
using UnityEngine.Events;

public class ProcedureProgress : MonoBehaviour
{
    public GameObject PreviousAudio;
    public GameObject Audio;
    public GameObject Subtitle;
    public GameObject PreviousChecklistTick;
    public GameObject Checklist;
    public GameObject Conditionflag;
    public GameObject PreCheckflag;
    public GameObject Nextflag;
    public GameObject ChatText;

    public UnityEvent Extras;

    public bool Triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Procedure();
    }

    public void Procedure()
    {
        StartCoroutine("ProgressProcedure");
    }

    public IEnumerator ProgressProcedure()
    {
        if (Conditionflag.activeSelf && PreCheckflag.activeSelf && Triggered == false)
        {
            PreviousAudio.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Audio.SetActive(true);
            //Subtitle.SetActive(true);
            PreviousChecklistTick.SetActive(true);
            Checklist.SetActive(true);
            ChatText.SetActive(true);   
            Nextflag.SetActive(true);
            Triggered = true;
        }
        else
        {
            yield return null; 
        }
    }
}
