using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private bool automatic;
    [SerializeField] private float fireRate;
    [SerializeField] private List<Vector2> bulletPositions;
    [SerializeField] private List<float> bulletAngles;
    [SerializeField] private int bulletAmount;
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float knockbackAmount;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeDuration;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private int idealDifficulty;
    [SerializeField] private GameObject weaponDrop;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float interval;
    public bool IsAutomatic()
    {
        return automatic;
    }
    public float FireRate()
    {
        return fireRate;
    }
    public List<Vector2> BulletPositions()
    {
        return bulletPositions;
    }
    public List<float> BulletAngles()
    {
        return bulletAngles;
    }
    public int BulletAmount()
    {
        return bulletAmount;
    }
    public Bullet Bullet()
    {
        return bullet;
    }
    public GameObject BulletObj()
    {
        return bulletObj;
    }
    public float SpeedMultiplier()
    {
        return speedMultiplier;
    }
    public float KnockbackAmount()
    {
        return knockbackAmount;
    }
    public float KnockbackTime()
    {
        return knockbackTime;
    }
    public void SetSpeedMultiplier(float value)
    {
        speedMultiplier = value;
    }
    public float ShakeMagnitude()
    {
        return shakeMagnitude;
    }
    public float ShakeDuration()
    {
        return shakeDuration;
    }
    public AudioClip SoundEffect()
    {
        return soundEffect;
    }
    public int IdealDifficulty()
    {
        return idealDifficulty;
    }
    public GameObject WeaponDrop()
    {
        return weaponDrop;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
    public float Interval()
    {
        return interval;
    }
}
