using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;

public class Scoreboard : MonoBehaviour
{
    public List<User> users = GameManager.Instance.users;
    public GameObject contentObject; // Reference to the Content object

    public void UpdateScoreboard()
    {
        // Remove old score entries
        foreach (Transform child in contentObject.transform)
        {
            Destroy(child.gameObject);
        }

        // Get top 10 users
        List<User> top10Users = GetTop10Users();

        // Create a new Text object for each score entry
        foreach (User user in top10Users)
        {
            GameObject textObject = new GameObject("Score Entry");
            textObject.transform.SetParent(contentObject.transform, false);

            Text scoreField = textObject.AddComponent<Text>();
            scoreField.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            scoreField.text = $"{user.Username}: {user.Attempts.Min()}";

            // Adjust the size and position of the Text object as needed
            // ...
        }
    }

    public List<User> GetTop10Users()
    {
        List<User> top10Users = users.Where(u => u.Attempts.Any(a => !a.Equals("Failed")))
                                    .OrderBy(u => u.Attempts.Min(a => float.Parse(a)))
                                    .Take(10)
                                    .ToList();
        return top10Users;
    }
}