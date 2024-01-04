using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    public Button returnButton;
    private string adminMode = "2";
    //private List<User> users = new List<User>();
    public BNG.SceneLoader sceneLoader;
    public Register registerScript;
    
    

    void Start()
    {
        //registerScript = FindObjectOfType<Register>();
        //registerScript.LoadUserData();
        returnButton.onClick.AddListener(ReturnToMainMenu);
    }

    void ReturnToMainMenu()
    {
        //GetCurrentUser();
        if (GameManager.Instance.CurrentUser.category == adminMode)
        {
            // Load admin menu
            sceneLoader.LoadScene("FYP ADMIN UI 1");
            Debug.Log("Back to menu admin. User count:" + GameManager.Instance.users.Count + "User category:" + GameManager.Instance.CurrentUser.category);
        }
        else
        {
            // Load trainee menu
            sceneLoader.LoadScene("FYP NORMAL UI 1");
            Debug.Log("Back to menu trainee. User count:" + GameManager.Instance.users.Count + "User category:" + GameManager.Instance.CurrentUser.category);
        }
        /*if (currentUser != null)
        {
            if (currentUser.category == adminMode)
            {
                // Load admin menu
            }
            else
            {
                // Load trainee menu
            }
        }
        else
        {
            // Load main menu
        }*/
    }
    /*
    User GetCurrentUser()
    {
        string username = GameManager.Instance.CurrentUser.Username;

        foreach (User user in GameManager.Instance.users)
        {
            if (user.Username == username)
            {
                return user;
            }
        }

        return null;
    }*/
    /*
    public void LoadUserData()
    {
        GameManager.Instance.users.Clear();
        using (StreamReader reader = new StreamReader(Application.dataPath + "/Accounts.csv"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string decryptedUsername = parts[0];
                string encryptedPassword = parts[1];
                string Category = parts[2];

                List<string> attempts = parts.Skip(3).ToList();

                Debug.Log($"#Loading user data: {decryptedUsername}, {encryptedPassword}, {Category}, {string.Join(",", attempts)}");
                User user = new User() { Username = decryptedUsername, Password = encryptedPassword, category = Category, Attempts = attempts };
                GameManager.Instance.users.Add(user);
            }
        }
        Debug.Log("#" + GameManager.Instance.users.Count);
    }*/
    /*void LoadUserData()
    {
        GameManager.Instance.users.Clear();
        using (StreamReader reader = new StreamReader(Application.dataPath + "/Accounts.csv"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string username = parts[0];
                string password = parts[1];
                string category = parts[2];

                User user = new User() { Username = username, Password = password, category = category };
                GameManager.Instance.users.Add(user);
                Debug.Log(GameManager.Instance.users.Count);
            }
        }
    }*/
}
