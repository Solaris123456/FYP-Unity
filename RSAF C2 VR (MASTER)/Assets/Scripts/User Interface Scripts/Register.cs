
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
//using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField passwordInputField;
    public Button loginButton;
    public Button registerButton;
    public TMP_Text messageText;

    public bool lightErrorFound; // public bool for light error found
    public bool ceilingErrorFound;
    public bool Fm200CheckFail; // public bool for wrong button pressed
    //public float timeTaken;

    private List<User> users = new List<User>();

    //(ignore this) private const string EncryptionKey = "QgaTO6Tqj1Jg0VwZgehmXF6uQXKK84WkXnwldAjPNlI=";
    //(ignore this) private const string InitializationVector = "Vo7arMerutWiGl+zCjdhSA==";
    private const string PasswordSalt = "UGFzc3dvcmRTYWx0";

    string trainingMode = "1";
    string adminMode = "2";

    public BNG.SceneLoader sceneLoader;

    
    void Start()
    {
        LoadUserData();
        //(ignore this) GenerateKeyAndIV();

    }

    public void Login()
    {
        string username = nameInputField.text;
        string password = passwordInputField.text;

        // Logout the current user before logging in a new user
        if (GameManager.Instance.CurrentUser != null)
        {
            Logout();
        }

        foreach (User user in users)
        {
            if (user.Username == username && VerifyPassword(password, user.Password))
            {
                GameManager.Instance.CurrentUser = user;
                Debug.Log("Login successful");
                if (user.category == adminMode)
                {
                    messageText.text = "Welcome Admin";
                    StartCoroutine(ClearMessageAfterDelay(3f));
                    Debug.Log("Welcome Admin");
                    sceneLoader.LoadScene("FYP ADMIN UI 1");

                }
                else
                {
                    messageText.text = "Welcome Trainee";
                    StartCoroutine(ClearMessageAfterDelay(3f));
                    Debug.Log("Welcome Trainee");
                    sceneLoader.LoadScene("FYP NORMAL UI 1");
                }
                return;
            }
        }
        messageText.text = "Login failed";
        StartCoroutine(ClearMessageAfterDelay(3f));
        Debug.Log("Login failed");
    }
    public void Registering()
    {
        string username = nameInputField.text;
        string password = passwordInputField.text;
        foreach (User user in users)
        {
            if (user.Username == username)
            {
                messageText.text = "Username already exists";
                StartCoroutine(ClearMessageAfterDelay(3f));
                Debug.Log("Username already exists");
                return;
            }
        }


        string encryptedPassword = EncryptPassword(password);
        User newUser = new User { Username = username, Password = encryptedPassword, category = "1" };
        users.Add(newUser);

        //LoadUserData();
        SaveUserData();
        messageText.text = "Registration Successful";
        StartCoroutine(ClearMessageAfterDelay(3f));
        Debug.Log("Registration Successful");
    }
    public void SaveUserData()
    {
        System.IO.File.WriteAllText(Application.dataPath + "/Accounts.csv", "");
        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Accounts.csv", append: true))
        {
            foreach (User user in users)
            {
                string encryptedUsername = user.Username;
                // Only write attempts data if user has made at least one attempt
                if (user.Attempts.Count > 0 && user.Attempts[0] != -1)
                {
                    float finalTimeTaken = user.Attempts[0];
                    writer.WriteLine(encryptedUsername + "," + user.Password + "," + user.category + "," + finalTimeTaken);


                    /*string attemptsData = String.Join(",", user.Attempts.Select(a => a == -1 ? "Failed" : a.ToString()));
                    writer.WriteLine(encryptedUsername + "," + user.Password + "," + user.category + "," + attemptsData);*/
                }
                else
                {
                    // If user has not made any attempts, don't write "Failed"
                    writer.WriteLine(encryptedUsername + "," + user.Password + "," + user.category);
                }
            }
        }
        Debug.Log("User data saved");
    }
    public void LoadUserData()
    {
        users.Clear();
        using (StreamReader reader = new StreamReader(Application.dataPath + "/Accounts.csv"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string decryptedUsername = parts[0];
                string encryptedPassword = parts[1];
                string Category = parts[2];

                float finalTimeTaken = -1;
                if (parts.Length > 3)
                {
                    float.TryParse(parts[3], out finalTimeTaken);
                }
                /*List<float> attempts = new List<float>();
                for (int i = 3; i < parts.Length; i++)
                {
                    if (!string.IsNullOrEmpty(parts[i]) && float.TryParse(parts[i], out float timeTaken))
                    {
                        attempts.Add(timeTaken);
                    }
                    else
                    {
                        attempts.Add(-1);
                    }
                }*/

                User user = new User() { Username = decryptedUsername, Password = encryptedPassword, category = Category, Attempts = new List<float>() { finalTimeTaken } };
                users.Add(user);
            }
        }
    }

    

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
                    return false; // Return false if the padding is invalid
                }
            }
        }
    }
    public void SaveFinalTimeTaken(string fiTimeTaken)
    {
        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Accounts.csv", append: true))
        {
            string encryptedUsername = GameManager.Instance.CurrentUser.Username;
            writer.WriteLine(encryptedUsername + "," + GameManager.Instance.CurrentUser.Password + "," + GameManager.Instance.CurrentUser.category + "," + fiTimeTaken);
        }
        Debug.Log("Final time taken saved");
    }
    IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }
    public void CompleteSimulation(float timeTaken)
    {
        string timeTakenString = timeTaken.ToString();
        //GameManager.Instance.CurrentUser.Attempts.Add(timeTaken);
        GameManager.Instance.SimulationResults = $"TimeTaken={timeTakenString},LightErrorFound={lightErrorFound},CeilingErrorFound={ceilingErrorFound},Fm200CheckFail={Fm200CheckFail}";
        // Save user data after updating
        SaveUserData();
    }

    public void FailSimulation()
    {
        GameManager.Instance.CurrentUser.Attempts.Add(-1);
        // Save user data after updating
        SaveUserData();
    }
    public void Logout()
    {
        // Save the current user's data before logging out
        SaveUserData();

        // Clear the current user's data in memory
        GameManager.Instance.CurrentUser = null;

        // Load the login scene
        SceneManager.LoadScene("FYP UI 2");
        SceneManager.sceneLoaded += (scene, mode) => 
        {
            Canvas myCanvas = FindObjectOfType<Canvas>();
            myCanvas.enabled = true;
        };
    }
}

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string category { get; set; }
    public List<float> Attempts { get; set; } = new List<float>();
}
/*public class Attempt
{
    public float TimeTaken { get; set; }
    public bool WasSuccessful { get; set; }
}*/
