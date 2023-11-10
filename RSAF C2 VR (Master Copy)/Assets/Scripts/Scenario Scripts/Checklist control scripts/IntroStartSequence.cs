using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStartSequence : MonoBehaviour
{
    public GameObject IntroAudio;
    public GameObject ChatlogFlag;
    public float delay = 5f;

    private void Start()
    {
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        IntroAudio.SetActive(true);

        // Activate the target object
        if (ChatlogFlag != null)
        {
            ChatlogFlag.SetActive(true);
        }
    }
}
