using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostOnHit : BulletPen
{
    GameObject player;
    PlayerController controller;
    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        controller = player.GetComponent<PlayerController>();
    }
    public override void Collided(Collider2D col)
    {
        base.Collided(col);
        if (col.gameObject.layer == 10)
        {
            controller.SetSpeedMultiplier(bullet.SpeedOnHit(), true);
            ScoreScript.AddScore(5);
        }
    }
}
