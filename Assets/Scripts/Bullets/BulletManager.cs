using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletManager : MonoBehaviour
{
    private Bullet bulletType;
    private WeaponManager weaponsM;
    private GameObject player;
    private GameObject bullet;
    private bool stopped;
    public Bullet BulletType => bulletType;
    public GameObject Bullet => bullet;

    public GameObject subBullet;
    public WeaponManager WeaponsM => weaponsM;
    void Start()
    {
        player = GameObject.Find("Player");
        weaponsM = player.GetComponent<WeaponManager>();
        CreateBullet();
        stopped = false;
    }
    private void CreateBullet()
    {
        // Instantiate copy of the class bullet
        Bullet classBullet = weaponsM.GetWeapon().Bullet();
        bulletType = ScriptableObject.Instantiate(classBullet);
        bullet = this.gameObject;
        // Set the bullet's component variables
        bulletType.SetClass();
        // Set all component variables and call start method in all components
        for (int i = 0; i < bulletType.Components().Count; i++)
        {
            bulletType.Components()[i].SetInfo(this);
            bulletType.Components()[i].Start();
        }
    }
    // Update is called once per frame
    private void Update()
    {
        // Update all bullet components
        if (!stopped)
        {
            for (int i = 0; i < bulletType.Components().Count; i++)
            {
                bulletType.Components()[i].Update();
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        // Call collided event in all bullet components
        if (col.gameObject.layer == 10 || col.gameObject.layer == 11)
        {
            for (int i = 0; i < bulletType.Components().Count; i++)
            {
                bulletType.Components()[i].Collided(col);
            }
        }
    }
    // Destroy the bullet gameObject in x time
    public void SelfDestructTimer(float time)
    {
        StartCoroutine(SelfDestruct(time));
    }
    private IEnumerator SelfDestruct(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(bullet);
    }
    public void StopTimer(float time)
    {
        StartCoroutine(StopSelf(time));
    }
    private IEnumerator StopSelf(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        stopped = true;
    }
    public void FireSubBullets(int count)
    {
        for(int i = 0; i < count; i++)
        {
            var spawnPos = transform.position;
            GameObject spawned = Instantiate(subBullet, spawnPos, Quaternion.Euler(new Vector3(0f, 0f, i * 90f)));
        }
    }
}
