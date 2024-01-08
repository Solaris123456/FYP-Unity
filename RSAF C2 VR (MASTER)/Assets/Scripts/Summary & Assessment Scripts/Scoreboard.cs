using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Valve.VR;

public class Scoreboard : MonoBehaviour
{
    //public List<User> users;
   // public GameObject contentObject; // Reference to the Content object
    public GameObject scoreEntry;
    public GameObject scoreEntryParent;

    public void UpdateScoreboard()
    {
        //List<User> users = GameManager.Instance.users;
        // Remove old score entries
        foreach (Transform child in scoreEntryParent.transform)
        {
            Destroy(child.gameObject);
        }

        // Get top 10 users
        List<User> top10Users = GetTop10Users();
        Debug.Log("#Top10"+top10Users.Count);
        // Create a new Text object for each score entry
        foreach (User user in top10Users)
        {
            GameObject _scoreEntry = Instantiate(scoreEntry, scoreEntryParent.transform);
            TextMeshProUGUI[] picked_textMesh = _scoreEntry.GetComponentsInChildren<TextMeshProUGUI>();
            picked_textMesh[0].text = $"{user.Username}"; // user
            picked_textMesh[1].text = $"{user.Attempts.Min()}"; // score
            Debug.Log("#Is success");
            /*
            GameObject textObject = new GameObject("Score Entry");
            textObject.transform.SetParent(contentObject.transform, false);

            Text scoreField = textObject.AddComponent<Text>();
            scoreField.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            userField.text
            scoreField.text = $"{user.Username}    {user.Attempts.Min()}";
            */
            // Adjust the size and position of the Text object as needed
            // ...
        }
    }

    public List<User> GetTop10Users()
    {
        int _amt = 10;
        if (GameManager.Instance.users.Count < _amt)
            _amt = GameManager.Instance.users.Count;
        List<User> top10Users = GameManager.Instance.users.Where(u => u.Attempts.Any(a => !a.Equals("Failed") && !string.IsNullOrEmpty(a)))
                                    .OrderBy(u => ConvertTimeSpanToSeconds(u.Attempts.Min()))
                                    .Take(_amt)
                                    .ToList();
        Debug.Log($"#{_amt}");
        return top10Users;
        
    }
    /*private TimeSpan ParseTimeSpan(string timeSpanString)
    {
        // Parse the time span string into a TimeSpan object
        return TimeSpan.ParseExact(timeSpanString, @"mm\:ss\.fff", null);
    }*/
    /*private TimeSpan ParseTimeSpan(string timeSpanString)
    {
        // Remove the parentheses and split the string into hours, minutes, seconds, and milliseconds
        string[] parts = timeSpanString.Split(":" + "."); //Trim('(', ')')

        // Parse the hours, minutes, seconds, and milliseconds into integers
        //int hours = int.Parse(parts[0]);
        int minutes = int.Parse(parts[0]);
        int seconds = int.Parse(parts[1]);
        int milliseconds = int.Parse(parts[2]);

        // Return a TimeSpan object representing the parsed time span
        return new TimeSpan(minutes, seconds, milliseconds); //.Add(TimeSpan.FromMilliseconds(milliseconds));
        
    }*/
    private float ConvertTimeSpanToSeconds(string timeSpanString)
    {
        TimeSpan timeSpan;
        if (TimeSpan.TryParseExact(timeSpanString, @"mm\:ss\.fff", null, out timeSpan))
        {
            return (float)timeSpan.TotalSeconds;

        }
        /*else if(timeSpan == null)
        {
            string[] parts = timeSpanString.Split(":"); //Trim('(', ')')
            int minutes = int.Parse(parts[0]);
            int seconds = int.Parse(parts[1]);
            int milliseconds = int.Parse(parts[2]);
            //return new timeSpan(minutes, seconds, milliseconds); //.Add(TimeSpan.FromMilliseconds(milliseconds));
            return (float)timeSpan.TotalSeconds;

        }*/
        else
        {
            Debug.LogError($"Invalid time span format: {timeSpanString}");
            float bruh = 0;
            return bruh;
            //return; // Or any other default value
        }
    }
}