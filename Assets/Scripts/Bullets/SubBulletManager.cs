using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBulletManager : MonoBehaviour
{
    private TrailRenderer trail;
    [SerializeField] private Bullet bullet;
    private CameraControl camShake;
    [SerializeField] private float shakeStrength;
    [SerializeField] private float shakeDur;
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        transform.localScale = new Vector2(bullet.Size(), bullet.Size());
        trail.startWidth = transform.localScale.y;
        camShake = Camera.main.GetComponent<CameraControl>();
        camShake.SetPowerAndTime(shakeStrength, shakeDur);
        StartCoroutine(SelfDestruct(bullet.Lifetime()));
    }
    private void Update()
    {
        transform.position += transform.right * bullet.Speed() * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 10)
        {
            col.GetComponent<HitResponse>().Hit(transform.right, bullet.KnockBackDur(), bullet.MinHitParticles(), bullet.MaxHitParticles());
            col.GetComponent<EnemyHealth>().DecrementHealth(bullet.Damage(), bullet.Id());
            Destroy(gameObject);
        }
        else if(col.gameObject.layer == 11)
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator SelfDestruct(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }
}
