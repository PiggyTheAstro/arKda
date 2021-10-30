using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] AudioClip menu;
    [SerializeField] AudioClip game;
    private AudioSource source;
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            source = instance.gameObject.GetComponent<AudioSource>();
            Recalculate();
        }
    }
    public void Recalculate()
    {
        if(source.isPlaying)
        {
            if (SceneManager.GetActiveScene().name.Contains("Game"))
            {
                if (source.clip == menu)
                {
                    source.Stop();
                    source.clip = game;
                    source.Play();
                }
            }
            else
            {
                if (source.clip == game)
                {
                    source.Stop();
                    source.clip = menu;
                    source.Play();
                }
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().name.Contains("Game"))
            {
                source.clip = game;
                source.Play();
            }
            else
            {
                source.clip = menu;
                source.Play();
            }
        }
        source.volume = SettingsManager.settings.volume * 0.75f;
    }
}
