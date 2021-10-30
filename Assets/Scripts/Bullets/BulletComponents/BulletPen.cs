using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPen : BulletComponent
{
    private int penetrationValue;
    private int penetrated;
    public override void Start()
    {
        base.Start();
        penetrationValue = bullet.Penetration();
        penetrated = 0;
    }

    // Update is called once per frame
    public override void Collided(Collider2D col)
    {
        if (col.gameObject.layer == 10)
        {
            if (penetrated >= penetrationValue)
            {
                parent.SelfDestructTimer(0f);
            }
            camShake.SetPowerAndTime(shakeValuesGetter.ShakeMagnitude() * 1.05f, shakeValuesGetter.ShakeDuration() * 1.05f);
            col.GetComponent<HitResponse>().Hit(bulletObj.transform.right, bullet.KnockBackDur(), bullet.MinHitParticles(), bullet.MaxHitParticles());
            col.GetComponent<EnemyHealth>().DecrementHealth(bullet.Damage(), bullet.Id());
            penetrated += 1;
            ScoreScript.AddScore(4 * penetrated);
            return;
        }
        parent.SelfDestructTimer(0f);
    }
}
