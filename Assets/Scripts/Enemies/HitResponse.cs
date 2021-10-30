using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitResponse : MonoBehaviour
{
    private Material mat;
    private Rigidbody2D rb;
    private bool hit;
    [SerializeField] private ParticleSystem vfx;
    [SerializeField] private ParticleSystem deathFX;
    [SerializeField] private Material flashMaterial;
    private AudioSource source;
    [SerializeField] private AudioClip hitsfx;
    public void Hit(Vector2 hitDir, float knockbackDuration, short minimum, short maximum)
    {
        hit = true;
        GetComponent<SpriteRenderer>().sharedMaterial = flashMaterial;
        rb.AddForce(hitDir * 3f);
        vfx.gameObject.transform.right = hitDir;
        deathFX.gameObject.transform.right = hitDir;
        ParticleSystem.Burst burst = vfx.emission.GetBurst(0);
        burst.minCount = minimum;
        burst.maxCount = maximum;
        vfx.emission.SetBurst(0, burst);
        vfx.Play();
        StartCoroutine(ResetColor(0.15f));
        StartCoroutine(ResetVelocity(knockbackDuration));
        source.pitch = Random.Range(0.8f, 1.2f);
        source.volume = SettingsManager.settings.volume;
        source.PlayOneShot(hitsfx);
    }
    private IEnumerator ResetColor(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        GetComponent<SpriteRenderer>().sharedMaterial = mat;
    }
    private IEnumerator ResetVelocity(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        rb.velocity = Vector3.zero;
        hit = false;
    }
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().sharedMaterial;
        rb = GetComponent<Rigidbody2D>();
        source = GetComponentInChildren<AudioSource>();
    }
    public bool Hit()
    {
        return hit;
    }
}
