using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CHEAT : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
            SceneManager.LoadScene("Summary Scene");
    }
}
