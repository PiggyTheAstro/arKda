using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOnCol : StopAfterTime
{
    public override void Collided(Collider2D col)
    {
        if (col.gameObject.layer == 10)
        {
            ScoreScript.AddScore(2);
            parent.FireSubBullets(4);
            base.Collided(col);
            parent.SelfDestructTimer(0f);
        }
    }
}
