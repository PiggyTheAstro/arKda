using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DisplayStats : MonoBehaviour
{
    [SerializeField] private List<string> keys;
    [SerializeField] private List<TextMeshProUGUI> fields;
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 4)
        {
            DisplayScores();
        }
        PlayerPrefs.SetInt("LastScore", 0);
    }

    private void DisplayScores()
    {
        for (int i = 0; i < 2; i++)
        {
            if (keys[i].ToString() != "Null" && keys[i].ToString().Length < 18)
            {
                fields[i].text = PlayerPrefs.GetInt(keys[i]).ToString();
               
            }
            else if(keys[i].ToString().Length >= 18)
            {
                fields[i].text = "99999999999999999+";
            }
        }
    }
}
