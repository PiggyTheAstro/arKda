using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private PlayerInput input;
    [SerializeField]
    private float speed;
    private WeaponManager weaponM;
    private float speedMultiplier = 1f;
    private bool shooting = false;
    [SerializeField]
    private TrailRenderer trail;
    private bool boosted = false;
    private int lives;
    [SerializeField] private ParticleSystem hitVFX;
    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;
    private Camera cam;
    public int Lives()
    {
        return lives;
    }
    public float SpeedMultiplier()
    {
        return speedMultiplier;
    }
    public void SetSpeedMultiplier(float value, bool decay)
    {
        if (decay)
        {
            boosted = true;
            StartCoroutine(SetSpeedBack(0.3f, speedMultiplier));
            StartCoroutine(ResetVelocity(0f, 0f));
        }
        speedMultiplier = value;
    }
    private IEnumerator SetSpeedBack(float time, float speed)
    {
        yield return new WaitForSecondsRealtime(time);
        boosted = false;
        speedMultiplier = speed;
    }
    void Start()
    {
        Time.timeScale = 1;
        // Creates input manager and gets weapon manager, gives weapon manager a reference to the input manager
        input = new PlayerInput();
        weaponM = GetComponent<WeaponManager>();
        weaponM.SetInput(input);
        trail.emitting = true;
        lives = 3;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SurvivalPoints());
        if (ScoreScript.hardCore)
        {
            lives = 1;
        }
        cam = Camera.main;
    }

    void Update()
    {
        // Gets mouse position every frame
        input.GetMouse(transform.position);
        RotateTowardsMouse();
        if (!shooting)
        {
            MoveForward();
        }
    }
    void RotateTowardsMouse()
    {
        float direction = input.Angle();
        float lerped = Mathf.LerpAngle(direction, transform.rotation.eulerAngles.z, 0.5f);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, lerped));
    }
    void MoveForward()
    {
        // Moves forward if mouse is above a certain distance from the player
        float dist = input.GetMouseDistance();
        dist = Mathf.Clamp(dist, 0, 10f);
        if (dist > 0.5f)
        {
            transform.position += transform.right * speed * Time.deltaTime;
            // Logarithmic function used to create an acceleration curve
            speed = 3.91f + 5.33f * Mathf.Log(dist);
            speed *= speedMultiplier;
        }
    }
    public void KnockBack(float duration, float intensity, float fireRate)
    {
        if (duration > 0.089f)
        {
            trail.emitting = false;
        }
        shooting = true;
        GetComponent<Rigidbody2D>().AddForce(-transform.right * intensity, ForceMode2D.Impulse);
        StartCoroutine(ResetVelocity(duration, fireRate + 0.2f));
    }
    private IEnumerator ResetVelocity(float time, float trailTime)
    {
        yield return new WaitForSecondsRealtime(time);
        StartCoroutine(ResetTrail(trailTime));
        shooting = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    private IEnumerator ResetTrail(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (!shooting)
        {
            trail.emitting = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 10)
        {
            Time.timeScale = 1;
            lives -= 1;
            Destroy(col.gameObject);
            hitVFX.Play();
            KnockBack(1f, 0f, 0f);
            audioSource.volume = SettingsManager.settings.volume;
            audioSource.PlayOneShot(hitSound);
        }
        if(col.gameObject.layer == 11)
        {
            Time.timeScale = 1;
            lives -= 1000;
        }
        if (lives <= 0)
        {
            Time.timeScale = 1;
            StartCoroutine(Transition());
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 12)
        {
            Time.timeScale = 1;
            lives -= 1;
            Destroy(col.gameObject);
            hitVFX.Play();
            KnockBack(1f, 0f, 0f);
            audioSource.volume = SettingsManager.settings.volume;
            audioSource.PlayOneShot(hitSound);
        }
        if (lives <= 0)
        {
            Time.timeScale = 1;
            StartCoroutine(Transition());
        }
    }
    IEnumerator Transition()
    {
        while (cam.orthographicSize > 1.5)
        {
            yield return new WaitForEndOfFrame();
            cam.orthographicSize -= 15 * Time.deltaTime;
            MusicManager.instance.GetComponent<AudioSource>().volume -= 0.1f * Time.deltaTime;
        }
        SceneManager.LoadScene(1);
    }
    public bool Boosted()
    {
        return boosted;
    }
    public bool Shooting()
    {
        return shooting;
    }
    private IEnumerator SurvivalPoints()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(5f);
            ScoreScript.AddScore(1 * lives);
        }
    }
}
