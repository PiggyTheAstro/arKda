using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreScript : MonoBehaviour
{
    private static int score;
    private static GunStats statTracker;
    public static bool hardCore;
    public static void AddScore(int amount)
    {
        if (!hardCore)
        {
            score += amount;
        }
        else
        {
            score += amount * 2;
        }
        PlayerPrefs.SetInt("LastScore", score);
        if(PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
    public static void TrackKill(int id)
    {
        if(id == 666)
        {
            return;
        }
        statTracker.gunKills[id] += 1;
        Save();
    }
    private void Awake()
    {
        score = 0;
        AddScore(0);
        statTracker = new GunStats();
        statTracker = Load();
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            hardCore = false;
            return;
        }
        hardCore = true;
    }
    private static void Save()
    {
        string json = JsonUtility.ToJson(statTracker);
        File.WriteAllText(Application.persistentDataPath + "/StatTracking.json", json);
    }
    public static GunStats Load()
    {
        string json;
        try
        {
            json = File.ReadAllText(Application.persistentDataPath + "/StatTracking.json");
            return JsonUtility.FromJson<GunStats>(json);
        }
        catch
        {
            return new GunStats();
        }
        
    }
}
