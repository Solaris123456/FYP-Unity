using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceRecogTestBlink : MonoBehaviour
{
    public GameObject BlinkObject;
    public int BlinkTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Blink()
    {
        StartCoroutine("BlinkLight");
    }

    public IEnumerator BlinkLight()
    {
        BlinkObject.SetActive(true);
        yield return new WaitForSeconds(BlinkTime);
        BlinkObject.SetActive(false);
    }

}
