using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ButtonScript : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private AudioSource source;
    private TextMeshProUGUI field;
    private Color ogColor;
    [SerializeField] private string weaponName;
    [SerializeField] private string desc;
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private int ID;
    void Start()
    {
        source = GetComponent<AudioSource>();
        field = GetComponentInChildren<TextMeshProUGUI>();
        ogColor = field.color;
       
    }
    public void Hover()
    {
        source.volume = SettingsManager.settings.volume;
        source.Play();
        field.color = Color.white;
        transform.localScale += new Vector3(0.05f, 0.05f, 0f);
        if(sceneName == "" && SceneManager.GetActiveScene().name != "Settings")
        {
            GameObject.Find("Name").GetComponent<TextMeshProUGUI>().text = weaponName;
            GameObject.Find("Desc").GetComponent<TextMeshProUGUI>().text = desc;
            if (ID != 8)
            {
                try
                {
                    GameObject.Find("KillField").GetComponent<TextMeshProUGUI>().text = ScoreScript.Load().gunKills[ID].ToString();
                }
                catch
                {
                    GameObject.Find("KillField").GetComponent<TextMeshProUGUI>().text = "0";
                }
                GameObject.Find("BigBorder").transform.GetChild(0).GetComponent<Image>().sprite = weaponSprite;
                GameObject.Find("BigBorder").transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
            else
            {
                GameObject.Find("KillField").GetComponent<TextMeshProUGUI>().text = "0";
                GameObject.Find("BigBorder").transform.GetChild(0).GetComponent<Image>().color = Color.black;
            }
        }
    }
    public void UnHover()
    {
        field.color = ogColor;
        transform.localScale -= new Vector3(0.05f, 0.05f, 0f);
    }
    public void Click()
    {
        if(sceneName == "Quit")
        {
            Application.Quit();
            return;
        }
        if(sceneName == "")
        {
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
