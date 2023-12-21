using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    public Button returnButton;
    private string adminMode = "2";
    private List<User> users = new List<User>();
    public BNG.SceneLoader sceneLoader;
    void Start()
    {
        LoadUserData();
        returnButton.onClick.AddListener(ReturnToMainMenu);
    }

    void ReturnToMainMenu()
    {
        User currentUser = GetCurrentUser();
        if (currentUser.category == adminMode)
        {
            // Load admin menu
            sceneLoader.LoadScene("FYP ADMIN UI 1");
        }
        else
        {
            // Load trainee menu
            sceneLoader.LoadScene("FYP NORMAL UI 1");
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

    User GetCurrentUser()
    {
        string username = GameManager.Instance.CurrentUser.Username;

        foreach (User user in users)
        {
            if (user.Username == username)
            {
                return user;
            }
        }

        return null;
    }

    void LoadUserData()
    {
        users.Clear();
        using (StreamReader reader = new StreamReader("Accounts.csv"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string username = parts[0];
                string password = parts[1];
                string category = parts[2];

                User user = new User() { Username = username, Password = password, category = category };
                users.Add(user);
            }
        }
    }
}
