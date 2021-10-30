using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    private Vector2 mPos;
    private Vector2 playerPos;
    public void GetMouse(Vector3 pPos)
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPos = pPos;
        
    }
    public float Angle()
    {
        return Mathf.Atan2((mPos.y - playerPos.y), (mPos.x - playerPos.x)) * Mathf.Rad2Deg;
    }
    public float GetMouseDistance()
    {
        return (mPos - playerPos).magnitude;
    }
    public bool SemiAuto()
    {
        return Input.GetKeyDown(SettingsManager.settings.shootKey);
        
    }
    public bool Auto()
    {
        return Input.GetKey(SettingsManager.settings.shootKey);
    }
}
