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

    private List<User> users = new List<User>();

    private const string EncryptionKey = "QgaTO6Tqj1Jg0VwZgehmXF6uQXKK84WkXnwldAjPNlI=";
    private const string InitializationVector = "Vo7arMerutWiGl+zCjdhSA==";
    private const string PasswordSalt = "UGFzc3dvcmRTYWx0";

    string trainingMode = "1";
    string adminMode = "2";

    public BNG.SceneLoader sceneLoader;

    /*private void GenerateKeyAndIV()
    {
        using (Aes aes = Aes.Create())
        {
            // Generate a new key and IV.
            aes.GenerateKey();
            aes.GenerateIV();

            // Get the key and IV as Base64 strings.
            EncryptionKey = Convert.ToBase64String(aes.Key);
            InitializationVector = Convert.ToBase64String(aes.IV);

            Debug.Log($"Key: {EncryptionKey}");
            Debug.Log($"IV: {InitializationVector}");
        }
    }*/
    void Start()
    {
        LoadUserData();
        //GenerateKeyAndIV();

    }

    public void Login()
    {
        string username = nameInputField.text;
        string password = passwordInputField.text;

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
                string encryptedUsername = user.Username;/*Encrypt(user.Username)*/
                // Only write attempts data if user has made at least one attempt
                if (user.Attempts.Count > 0)
                {
                    string attemptsData = String.Join(",", user.Attempts.Select(a => a == -1 ? "Failed" : a.ToString()));
                    writer.WriteLine(encryptedUsername + "," + user.Password + "," + user.category + "," + attemptsData);
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
                string decryptedUsername = parts[0];//Decrypt(parts[0])
                string encryptedPassword = parts[1];
                string Category = parts[2];

                List<float> attempts = new List<float>();
                for (int i = 3; i < parts.Length; i++)
                {
                    if (!string.IsNullOrEmpty(parts[i]) && float.TryParse(parts[i], out float timeTaken))
                    {
                        attempts.Add(timeTaken);
                    }
                    /*else
                    {
                        attempts.Add(-1);
                    }*/
                }

                User user = new User() { Username = decryptedUsername, Password = encryptedPassword, category = Category, Attempts = attempts };
                users.Add(user);
            }
        }
    }

    /*private string Encrypt(string value)
    {
        byte[] encryptedBytes;
        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(EncryptionKey);
            aes.IV = Convert.FromBase64String(InitializationVector);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(value);
                    }
                }
                encryptedBytes = ms.ToArray();
            }
        }
        return Convert.ToBase64String(encryptedBytes);
    }*/

    /*private string Decrypt(string value)
    {
        byte[] encryptedBytes = Convert.FromBase64String(value);
        string decryptedValue;
        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(EncryptionKey);
            aes.IV = Convert.FromBase64String(InitializationVector);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        decryptedValue = sr.ReadToEnd();
                    }
                }
            }
        }
        return decryptedValue;
    }*/

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
    IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }
    public void CompleteSimulation(float timeTaken)
    {
        GameManager.Instance.CurrentUser.Attempts.Add(timeTaken);
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