using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float baseHealth;
    private GameObject vfxChild;
    private GameObject deathChild;
    private VFXScript vfx;
    private VFXScript dfx;
    private float currentHealth;
    [SerializeField] private HitPause hitpause;
    [SerializeField] private GameObject deathAnim;
    private AudioSource source;
    [SerializeField] private AudioClip die;
    void Start()
    {
        currentHealth = baseHealth;
        vfxChild = transform.GetChild(0).gameObject;
        deathChild = transform.GetChild(1).gameObject;
        vfx = vfxChild.GetComponent<VFXScript>();
        dfx = deathChild.GetComponent<VFXScript>();
        hitpause = GameObject.Find("HitPauseManager").GetComponent<HitPause>();
        source = GetComponentInChildren<AudioSource>();

    }

    public void DecrementHealth(float damage, int id)
    {
        currentHealth -= damage;
        ScoreScript.AddScore((int)damage);
        if (currentHealth <= 0)
        {
            source.pitch = Random.Range(0.8f, 1.2f);
            source.volume = SettingsManager.settings.volume;
            source.PlayOneShot(die);
            hitpause.Pause(0.05f);
            gameObject.transform.DetachChildren();
            deathAnim.SetActive(true);
            deathChild.GetComponent<ParticleSystem>().Play();
            vfx.StartCoroutine(vfx.Kill());
            dfx.StartCoroutine(dfx.Kill());
            ScoreScript.TrackKill(id);
            Kill();
        }
    }
    private void Kill()
    {
        ScoreScript.AddScore((int) baseHealth);
        Destroy(gameObject);
    }
}
