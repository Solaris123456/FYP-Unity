using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public FM200LightTest FM200LightTestscript;
    public GameObject something;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SceneLoadOnChange(int SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
