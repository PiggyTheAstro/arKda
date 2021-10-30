using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Weapon activeWeapon;
    private PlayerInput input;
    private bool canShoot = true;
    [SerializeField] GameObject centerSpawn;
    private GameObject player;
    private PlayerController controller;
    private CameraControl camShake;
    [SerializeField] public List<Weapon> inventory;
    private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound;
    public void SetInput(PlayerInput input)
    {
        this.input = input;
    }
    public Weapon GetWeapon()
    {
        return activeWeapon;
    }
    private void Start()
    {
        player = GameObject.Find("Player");
        controller = player.GetComponent<PlayerController>();
        camShake = Camera.main.GetComponent<CameraControl>();
        audioSource = player.GetComponent<AudioSource>();
        activeWeapon = inventory[0];
        controller.SetSpeedMultiplier(activeWeapon.SpeedMultiplier(), false);
    }
    private void Update()
    {
        if (!controller.Boosted())
        {
            controller.SetSpeedMultiplier(activeWeapon.SpeedMultiplier(), false);
        }
        GetShootInput(activeWeapon.IsAutomatic());
        if (SettingsManager.settings.scrollSwitch)
        {
            Vector2 scroll = Input.mouseScrollDelta;
            if (Mathf.Abs(scroll.y) > 0.9f)
            {
                if (scroll.y < 0f)
                {
                    try
                    {
                        activeWeapon = inventory[WeaponIndex() - 1];
                    }
                    catch
                    {
                        activeWeapon = inventory[inventory.Count - 1];
                    }
                }
                else if (scroll.y > 0f)
                {
                    try
                    {
                        activeWeapon = inventory[WeaponIndex() + 1];
                    }
                    catch
                    {
                        activeWeapon = inventory[0];
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(SettingsManager.settings.previousGun))
            {
                try
                {
                    activeWeapon = inventory[WeaponIndex() - 1];
                }
                catch
                {
                    activeWeapon = inventory[inventory.Count - 1];
                }
            }
            else if (Input.GetKeyDown(SettingsManager.settings.nextGun))
            {
                try
                {
                    activeWeapon = inventory[WeaponIndex() + 1];
                }
                catch
                {
                    activeWeapon = inventory[0];
                }
            }
        }
        
    }
    private void GetShootInput(bool auto)
    {
        // Shoots if the weapon's auto type and input match
        if (auto)
        {
            if (!input.Auto())
            {
                return;
            }
        }
        else
        {
            if (!input.SemiAuto())
            {
                return;
            }
        }
        if (canShoot)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        audioSource.volume = SettingsManager.settings.volume;
        audioSource.pitch = Random.Range(0.5f, 0.9f);
        audioSource.PlayOneShot(activeWeapon.SoundEffect());
        camShake.SetPowerAndTime(activeWeapon.ShakeMagnitude() / 1.2f, activeWeapon.ShakeDuration() / 1.2f);
        if (!controller.Shooting())
        {
            controller.KnockBack(activeWeapon.KnockbackTime(), activeWeapon.KnockbackAmount(), activeWeapon.FireRate());
        }
        // Spawns a bullet and sets a timer for when the next shot may happen
        canShoot = false;
        StartCoroutine(SpawnBullets(activeWeapon.BulletAmount(), activeWeapon.BulletPositions(), activeWeapon.BulletAngles()));
        StartCoroutine(ResetShootPermission(activeWeapon.FireRate()));
    }
    IEnumerator ResetShootPermission(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        canShoot = true;
    }
    IEnumerator SpawnBullets(int amount, List<Vector2> positions, List<float> angles)
    {
        for (int i = 0; i < amount; i++)
        {
            var spawnPos = centerSpawn.transform.rotation * new Vector3(positions[i].x, positions[i].y, 1f) + centerSpawn.transform.position;
            Instantiate(activeWeapon.BulletObj(), spawnPos, Quaternion.Euler(new Vector3(0f, 0f, angles[i] + player.transform.rotation.eulerAngles.z)));
            if (activeWeapon.Interval() > 0)
            {
                yield return new WaitForSecondsRealtime(activeWeapon.Interval());
            }
           
        }
    }
    public int WeaponIndex()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i] == activeWeapon)
            {
                return i;
            }
        }
        return -1;
    }
    public void PickUp()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.volume = SettingsManager.settings.volume;
        audioSource.PlayOneShot(pickupSound);
    }
}
