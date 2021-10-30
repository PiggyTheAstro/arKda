using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilComponent : BulletComponent
{
    public override void Start()
    {
        base.Start();
        bulletObj.transform.Rotate(new Vector3(0f, 0f, Random.Range(-bullet.Recoil(), bullet.Recoil())));
    }
}
