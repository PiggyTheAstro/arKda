using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New bullet", menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    private List<BulletComponent> components;
    [SerializeField] private List<string> serializedComps;
    [SerializeField] private float speed;
    [SerializeField] private float size;
    [SerializeField] private float recoil;
    [SerializeField] private float speedOnHit;
    [SerializeField] private float knockbackDur;
    [SerializeField] private short minHitparticles;
    [SerializeField] private short maxHitParticles;
    [SerializeField] private float lifetime;
    [SerializeField] private float damage;
    [SerializeField] private int penetration;
    [SerializeField] private int ID;
    
    // Getters for variables
    public List<BulletComponent> Components()
    {
        return components;
    }
    public float Speed()
    {
        return speed;
    }
    public float Size()
    {
        return size;
    }
    public float Recoil()
    {
        return recoil;
    }
    public float SpeedOnHit()
    {
        return speedOnHit;
    }
    public float KnockBackDur()
    {
        return knockbackDur;
    }
    public short MinHitParticles()
    {
        return minHitparticles;
    }
    public short MaxHitParticles()
    {
        return maxHitParticles;
    }
    public float Lifetime()
    {
        return lifetime;
    }
    public float Damage()
    {
        return damage;
    }
    public int Penetration()
    {
        return penetration;
    }
    public int Id()
    {
        return ID;
    }
    // Setters
    public void SetClass()
    {
        // Turns the serialized strings assigned in the editor into component classes
        for (int i = 0; i < serializedComps.Count; i++)
        {
            var type = System.Type.GetType(serializedComps[i]);
            var obj = System.Activator.CreateInstance(type);
            components = new List<BulletComponent>();
            if (obj is BulletComponent)
            {
                components.Add(obj as BulletComponent);
            }

        }
    }
}
