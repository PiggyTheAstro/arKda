using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAfterTime : BulletComponent
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        parent.StopTimer(0.5f);
    }
}
