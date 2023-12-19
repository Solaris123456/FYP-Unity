using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Build.Content;
using static System.Net.WebRequestMethods;
using static UnityEditor.PlayerSettings;
using UnityEngine.EventSystems;

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
        using (StreamWriter writer = new StreamWriter("Accounts.csv"))
        {
            foreach (User user in users)
            {
                string encryptedUsername = user.Username;/*Encrypt(user.Username)*/
                writer.WriteLine(encryptedUsername + "," + user.Password + "," + user.category);
            }
        }
        Debug.Log("User data saved");
    }
    public void LoadUserData()
    {
        users.Clear();
        using (StreamReader reader = new StreamReader("Accounts.csv"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string decryptedUsername = parts[0];//Decrypt(parts[0])
                string encryptedPassword = parts[1];
                string Category = parts[2];
                User user = new User() { Username = decryptedUsername, Password = encryptedPassword, category = Category };
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
}

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string category { get; set; }
}
