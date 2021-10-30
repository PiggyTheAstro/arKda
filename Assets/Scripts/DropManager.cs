using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> guns;
    public List<Weapon> dropPool;
    private WaveScript waveManager;
    private void Start()
    {
        dropPool = new List<Weapon>();
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveScript>();
    }

    public void DropWeapon()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            int distance = Mathf.Abs(guns[i].IdealDifficulty() - Mathf.RoundToInt(waveManager.difficulty));
            int rawWeight = (int)(71.5753f * Mathf.Exp(-0.2639f * distance));
            for(int j = 0; j < rawWeight; j++)
            {
                dropPool.Add(guns[i]);
            }
        }
        Weapon drop = dropPool[Random.Range(0, dropPool.Count)];
        Instantiate(drop.WeaponDrop(), transform.position, Quaternion.identity);
        guns.Remove(drop);
        dropPool.Clear();


    }
}
