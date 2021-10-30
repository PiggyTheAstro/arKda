using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    private WeaponManager weaponManager;
    private bool collided = false;
    // Start is called before the first frame update
    void Awake()
    {
        weaponManager = GameObject.Find("Player").GetComponent<WeaponManager>();
        if(weaponManager.GetWeapon() == null)
        {
            weaponManager.enabled = true;
            weaponManager.inventory.Add(weapon);
            Destroy(gameObject);
        }
        StartCoroutine(Shrink());
    }

    // Update is called once per frame
    void Update()
    {
        if (collided)
        {
            weaponManager.inventory.Add(weapon);
            weaponManager.PickUp();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 9)
        {
            collided = true;
        }
    }
    private IEnumerator Shrink()
    {
        while (transform.localScale.x > 0f)
        {
            yield return new WaitForEndOfFrame();
            transform.localScale -= new Vector3(0.22f * Time.deltaTime, 0.22f * Time.deltaTime, 0f);
        }
        Destroy(gameObject);
    }
}
