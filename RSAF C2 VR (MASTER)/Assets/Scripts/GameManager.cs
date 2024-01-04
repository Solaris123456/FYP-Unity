//using System.Collections;
//using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public User CurrentUser { get; set; }
    public string SimulationResults { get; set; }

    public List<User> users = new List<User>();

    public void Start()
    {
        Register registerScript = FindObjectOfType<Register>();
        registerScript.LoadUserData();
        Debug.Log("this btr run once sia #");
        Debug.Log("#" + GameManager.Instance.users.Count);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
}
