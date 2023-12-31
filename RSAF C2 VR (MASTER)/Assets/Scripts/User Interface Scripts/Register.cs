using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.Build.Content;
//using static System.Net.WebRequestMethods;
//using static UnityEditor.PlayerSettings;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Register : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;
    public Button registerButton;
    public TMP_Text messageText;

    public bool lightErrorFound;
    public bool ceilingErrorFound;
    public bool Fm200CheckFail;

    //public List<User> users = new List<User>();
    
    private const string PasswordSalt = "UGFzc3dvcmRTYWx0";

    string trainingMode = "1";
    string adminMode = "2";
    public UnityEvent displayResult;
    public BNG.SceneLoader sceneLoader;

    void Start()
    {
        
        //LoadUserData();

    }

    public void Login()
    {
        
        string username = nameInputField.text;
        string password = passwordInputField.text;

        if (GameManager.Instance.CurrentUser != null)
        {
            Debug.Log("#Logged out:" + GameManager.Instance.CurrentUser);
            Logout();
            
        }
        Debug.Log("#User count before login:" + GameManager.Instance.users.Count);
        foreach (User user in GameManager.Instance.users)
        {
            if (user.Username == username && VerifyPassword(password, user.Password))
            {
                GameManager.Instance.CurrentUser = user;
                Debug.Log("#Login successful");

                if (user.category == adminMode)
                {
                    messageText.text = "Welcome Admin";
                    StartCoroutine(ClearMessageAfterDelay(3f));
                    Debug.Log("#Welcome Admin");
                    //FailSimulation();//remove after done
                    sceneLoader.LoadScene("FYP ADMIN UI 1");
                    Debug.Log("#User count after login:" + GameManager.Instance.users.Count);

                }
                else
                {
                    messageText.text = "Welcome Trainee";
                    StartCoroutine(ClearMessageAfterDelay(3f));
                    Debug.Log("#Welcome Trainee");
                    //FailSimulation();
                    sceneLoader.LoadScene("FYP NORMAL UI 1");
                    Debug.Log("#User count after login:" + GameManager.Instance.users.Count);
                }
                return;
            }
        }
        messageText.text = "Login failed";
        StartCoroutine(ClearMessageAfterDelay(3f));
        Debug.Log("#Login failed");
    }
    public void Registering()
    {
        string username = nameInputField.text;
        string password = passwordInputField.text;

        foreach (User user in GameManager.Instance.users)
        {
            if (user.Username == username)
            {
                messageText.text = "Username already exists";
                StartCoroutine(ClearMessageAfterDelay(3f));
                Debug.Log("#Username already exists");
                return;
            }
        }

        string encryptedPassword = EncryptPassword(password);
        User newUser = new User { Username = username, Password = encryptedPassword, category = "1"};
        
        GameManager.Instance.users.Add(newUser);
        Debug.Log("#User count after registration:"+GameManager.Instance.users.Count);
        SaveUserData();
        messageText.text = "Registration Successful";
        StartCoroutine(ClearMessageAfterDelay(3f));
        Debug.Log("#Registration Successful");
    }
    public void SaveUserData()
    {
        Debug.Log($"#Saving user data. Users count before saving: {GameManager.Instance.users.Count}");

        
        System.IO.File.WriteAllText(Application.dataPath + "/Accounts.csv", "");
        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Accounts.csv", append: true))
        {
            foreach (User user in GameManager.Instance.users)
            {
                string encryptedUsername = user.Username;
                string encryptedPassword = user.Password;
                string Category = user.category;
                string attempts = "";
                if(user.Attempts != null)
                {
                    attempts = string.Join(",", user.Attempts);
                }
                Debug.Log($"#Users count before writing to csv: {GameManager.Instance.users.Count}");
                Debug.Log($"#Writing to CSV. User: {GameManager.Instance.CurrentUser.Username}, Attempts: {string.Join(",", GameManager.Instance.CurrentUser.Attempts)}");
                writer.WriteLine(encryptedUsername + "," + encryptedPassword + "," + Category + ","+ attempts);
            }
        }

        Debug.Log($"#Saved user data. Users: {string.Join(", ", GameManager.Instance.users.Select(u => u.Username))}");

        Debug.Log("#User data saved");
        Debug.Log($"#Users count after saving: {GameManager.Instance.users.Count}");
        foreach (User user in GameManager.Instance.users)
        {
            Debug.Log($"#User: {user.Username}, Attempts: {string.Join(",", user.Attempts)}");
        }
    }
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
        Debug.Log("#LoadUserData"+ GameManager.Instance.users.Count);

    }
    //usernem = input("usernem: " )
    //paswod = input("paswor: " )
    //with open "Accounts.csv","r": 
    //smu comp sci nerd award winning python code
    private string EncryptPassword(string password)
    {
        byte[] salt = Convert.FromBase64String(PasswordSalt);
        using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000))
        {
            byte[] key = deriveBytes.GetBytes(32);
            byte[] iv = deriveBytes.GetBytes(16);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(password);
                        }
                    }
                    byte[] encryptedPasswordBytes = ms.ToArray();
                    return Convert.ToBase64String(encryptedPasswordBytes);
                }
            }
        }
    }

    private bool VerifyPassword(string password, string encryptedPassword)
    {
        byte[] salt = Convert.FromBase64String(PasswordSalt);
        using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000))
        {
            byte[] key = deriveBytes.GetBytes(32);
            byte[] iv = deriveBytes.GetBytes(16);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] encryptedPasswordBytes = Convert.FromBase64String(encryptedPassword);

                try
                {
                    using (MemoryStream ms = new MemoryStream(encryptedPasswordBytes))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                string decryptedPassword = sr.ReadToEnd();
                                return password == decryptedPassword;
                            }
                        }
                    }
                }
                catch (CryptographicException)
                {
                    return false;
                }
            }
        }
    }
    public void SaveFinalTimeTaken(string fiTimeTaken)
    {
        //User currentUser = GameManager.Instance.CurrentUser;
        //currentUser.Attempts.Add(fiTimeTaken);
        GameManager.Instance.CurrentUser.Attempts.Add(fiTimeTaken);
        /*int index = users.FindIndex(user => user.Username == GameManager.Instance.CurrentUser.Username);
        if (index != -1)
        {
            users[index] = GameManager.Instance.CurrentUser;
        }
        else
        {
            Debug.LogError("#User not found in list");
        }*/
        if (GameManager.Instance.CurrentUser != null && GameManager.Instance.users.Exists(user => user.Username == GameManager.Instance.CurrentUser.Username))
        {
            int index = GameManager.Instance.users.FindIndex(user => user.Username == GameManager.Instance.CurrentUser.Username);
            if (index != -1)
            {
                GameManager.Instance.users[index] = GameManager.Instance.CurrentUser;
            }
        }
        else
        {
            Debug.LogError("#User not found in list");
        }
        Debug.Log($"#Final time taken saved for user {GameManager.Instance.CurrentUser}. Attempts: {string.Join(",", GameManager.Instance.CurrentUser.Attempts)}");
        Debug.Log("#Number of users " + GameManager.Instance.users.Count);
        SaveUserData();

        Debug.Log("#Final time taken saved");
        //Debug.Log($"#Final time taken saved for user {GameManager.Instance.CurrentUser}. Attempts: {string.Join(",", GameManager.Instance.CurrentUser.Attempts)}");
        //SaveUserData();

        //Debug.Log("#Final time taken saved");
    }
    IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }
    public void CompleteSimulation(float timeTaken)
    {
        string timeTakenString = timeTaken.ToString();
        Debug.Log("#Number of users complete sim " + GameManager.Instance.users.Count);
        GameManager.Instance.SimulationResults = $"TimeTaken={timeTakenString},LightErrorFound={lightErrorFound},CeilingErrorFound={ceilingErrorFound},Fm200CheckFail={Fm200CheckFail}";

    }

    public void FailSimulation()
    {

        if (GameManager.Instance.CurrentUser != null)
        {
            GameManager.Instance.CurrentUser.Attempts.Add("Failed");
            Debug.Log($"#Failed attempt added for user {GameManager.Instance.CurrentUser.Username}. Attempts: {string.Join(",", GameManager.Instance.CurrentUser.Attempts)}");

            int index = GameManager.Instance.users.FindIndex(user => user.Username == GameManager.Instance.CurrentUser.Username);
            if (index != -1)
            {
                GameManager.Instance.users[index] = GameManager.Instance.CurrentUser;
            }
            else
            {
                Debug.LogError("#User not found in list");
            }

            SaveUserData();
        }
        else
        {
            Debug.LogError("#FailSimulation called but no current user is set in GameManager.Instance.CurrentUser");
        }
    }
    public void Logout()
    {

        SaveUserData();
        //GameManager.Instance.users.Clear();
        GameManager.Instance.CurrentUser = null;

        SceneManager.LoadScene("FYP UI 2");
        /*
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            Canvas myCanvas = FindObjectOfType<Canvas>();
            myCanvas.enabled = true;
        };*/
    }
}

[System.Serializable]
public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string category { get; set; }
    public List<string> Attempts { get; set; } = new List<string>();
}

