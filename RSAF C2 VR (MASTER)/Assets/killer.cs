using UnityEngine;
using UnityEngine.Events;

public class killer : MonoBehaviour
{
    public UnityEvent kill;
    public bool killed = false;
   
    void Start()
    {
        if (killed == false)
        {
            killed = true;
            kill.Invoke();
        }
    }
}
//DO NOT TOUCH THIS or the UI for the safety injects will not work during the game
