using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailAnimation : MonoBehaviour
{
    public GameObject Title;
    public GameObject Subtext;
    public GameObject Button1;
    public GameObject Button2;
    // Start is called before the first frame update
    void Start()
    {
        Animation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Animation()
    {
        Title.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Subtext.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Button1.SetActive(true);
        Button2.SetActive(true);
    }
}
