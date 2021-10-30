using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
public class SettingsManager : MonoBehaviour
{
    public static Settings settings;
    private bool inputting;
    private string inputButton;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;
    [SerializeField] private Slider bloom;
    private Bloom bloomVal;
    void Awake()
    {
        settings = new Settings();
        settings = Load();
        inputting = false;
        inputButton = "";
        SetToggles();
        if (MusicManager.instance != null)
        {
            MusicManager.instance.Recalculate();
        }
    }
    void SetToggles()
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.TryGetSettings<Bloom>(out bloomVal);
        bloomVal.intensity.value = settings.bloom;
        if (toggle != null)
        {
            toggle.isOn = settings.scrollSwitch;
            if (settings.scrollSwitch)
            {
                toggle.GetComponentInChildren<Image>().color = Color.green;
            }
            else
            {
                toggle.GetComponentInChildren<Image>().color = Color.red;
            }
        }
        if(slider != null)
        {
            slider.value = settings.volume;
        }
        if(bloom != null)
        {
            bloom.value = settings.bloom;
        }
        if(GameObject.Find("Shoot") != null)
        {
            GameObject.Find("Shoot").GetComponentInChildren<TextMeshProUGUI>().text = settings.shootKey.ToString();
            if (!settings.scrollSwitch)
            {
                GameObject.Find("Prev").GetComponentInChildren<TextMeshProUGUI>().text = settings.previousGun.ToString();
                GameObject.Find("Next").GetComponentInChildren<TextMeshProUGUI>().text = settings.nextGun.ToString();
            }
            else
            {
                GameObject.Find("Prev").GetComponentInChildren<TextMeshProUGUI>().text = "None";
                GameObject.Find("Next").GetComponentInChildren<TextMeshProUGUI>().text = "None";
            }
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
        }
        if (inputting && Input.anyKeyDown)
        {
            foreach (KeyCode kCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kCode))
                {
                    inputting = false;
                    switch (inputButton)
                    {
                        case "Shoot":
                            settings.shootKey = kCode;
                            break;
                        case "Prev":
                            settings.previousGun = kCode;
                            settings.scrollSwitch = false;
                            SetToggles();
                            break;
                        case "Next":
                            settings.nextGun = kCode;
                            settings.scrollSwitch = false;
                            SetToggles();
                            break;
                    }
                    GameObject.Find(inputButton).GetComponentInChildren<TextMeshProUGUI>().text = kCode.ToString();
                    inputButton = "";
                    Save();
                }
            }
        }
    }
    static void Save()
    {
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(Application.persistentDataPath + "/Controls.json", json);
    }
    static Settings Load()
    {
        string json;
        try
        {
            json = File.ReadAllText(Application.persistentDataPath + "/Controls.json");
            return JsonUtility.FromJson<Settings>(json);
        }
        catch
        {
            settings.scrollSwitch = true;
            settings.shootKey = KeyCode.Mouse0;
            settings.nextGun = KeyCode.E;
            settings.previousGun = KeyCode.Q;
            settings.volume = 1;
            settings.bloom = 2.5f;
            return settings;
        }
    }

    public void TickBox()
    {
        settings.scrollSwitch = !settings.scrollSwitch;
        if (settings.scrollSwitch)
        {
            toggle.GetComponentInChildren<Image>().color = Color.green;
            GameObject.Find("Prev").GetComponentInChildren<TextMeshProUGUI>().text = "None";
            GameObject.Find("Next").GetComponentInChildren<TextMeshProUGUI>().text = "None";
        }
        else
        {
            toggle.GetComponentInChildren<Image>().color = Color.red;
            GameObject.Find("Prev").GetComponentInChildren<TextMeshProUGUI>().text = settings.previousGun.ToString();
            GameObject.Find("Next").GetComponentInChildren<TextMeshProUGUI>().text = settings.nextGun.ToString();
        }
        Save();
        
    }
    public void VolumeChanged()
    {
        settings.volume = slider.value;
        Save();
        MusicManager.instance.Recalculate();
    }
    public void BloomChanged()
    {
        settings.bloom = bloom.value;
        Camera.main.GetComponent<PostProcessVolume>().profile.TryGetSettings<Bloom>(out bloomVal);
        bloomVal.intensity.value = settings.bloom;
        Save();
    }
    public void SetterClicked(GameObject obj)
    {
        inputButton = obj.name;
        inputting = true;

    }
}
