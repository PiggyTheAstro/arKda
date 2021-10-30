using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletComponent
{
    protected GameObject bulletObj;
    private float bulletSpeed;
    private float bulletSize;
    protected BulletManager parent;
    protected Bullet bullet;
    private TrailRenderer trail;
    protected CameraControl camShake;
    protected Weapon shakeValuesGetter;
    public void SetInfo(BulletManager parent)
    {
        // Gets the bullet base class and the bullet object from the bullet's manager
        this.parent = parent;
        bulletObj = parent.Bullet;
        bullet = parent.BulletType;
        trail = parent.GetComponent<TrailRenderer>();
        camShake = Camera.main.GetComponent<CameraControl>();
        shakeValuesGetter = parent.WeaponsM.GetWeapon();
        GetBulletVars();
    }
    private void GetBulletVars()
    {
        bulletSpeed = bullet.Speed();
        bulletSize = bullet.Size();
    }
    public virtual void Start()
    {
        // Sets trail size
        trail.startWidth = bulletSize;
        trail.time = bulletSpeed * 0.003f;
        // Sets correct size and starts self destruct timer
        parent.SelfDestructTimer(bullet.Lifetime());
        bulletObj.transform.localScale = new Vector2(bulletSize, bulletSize);
    }
    public virtual void Update()
    {
        // Moves bullet forward
        bulletObj.transform.position += bulletObj.transform.right * bulletSpeed * Time.deltaTime;
        shakeValuesGetter = parent.WeaponsM.GetWeapon();
    }
    public virtual void Collided(Collider2D col)
    {
        // Destroys bullet object on collision
        if (col.gameObject.layer == 10)
        {
            camShake.SetPowerAndTime(shakeValuesGetter.ShakeMagnitude() * 1.05f, shakeValuesGetter.ShakeDuration() * 1.05f);
            col.GetComponent<HitResponse>().Hit(bulletObj.transform.right, bullet.KnockBackDur(), bullet.MinHitParticles(), bullet.MaxHitParticles());
            col.GetComponent<EnemyHealth>().DecrementHealth(bullet.Damage(), bullet.Id());
        }
        parent.SelfDestructTimer(0f);
    }
}
