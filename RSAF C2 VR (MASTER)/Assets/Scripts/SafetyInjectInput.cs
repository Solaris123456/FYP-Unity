using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyInjectInput : MonoBehaviour
{
    public bool activated;
    public GameObject CeilingBoards;
    public GameObject Lights;
    public Fm200PressureCheck fm200PressureCheck;
    // Start is called before the first frame update
    public void SafeInjectStart ()
    {
        fm200PressureCheck.PressureCheckBegin ();
    }
}
