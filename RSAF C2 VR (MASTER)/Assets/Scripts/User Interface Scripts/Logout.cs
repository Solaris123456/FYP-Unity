using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logout : MonoBehaviour
{
    // Start is called before the first frame update
    public Register registerScript;
    void Start()
    {
        
        registerScript = FindObjectOfType<Register>();
            
        
    }
    public void logout()
    {
        
        registerScript.Logout();
    }
    
}
