using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponUI : MonoBehaviour
{
    private WeaponManager weaponManager;
    private Image image;
    private void Start()
    {
        weaponManager = GameObject.Find("Player").GetComponent<WeaponManager>();
        image = GetComponent<Image>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        image.sprite = weaponManager.GetWeapon().GetSprite();
    }
}
