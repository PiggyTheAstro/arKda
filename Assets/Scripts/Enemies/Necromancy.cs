using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancy : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject rareEnemy;
    [SerializeField] GameObject spawnPos;
    private WaveScript waveManager;
    // Start is called before the first frame update
    void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveScript>();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(5f);
            int randint = (int) Random.Range(1f, 3f);
            if (randint != 3)
            {
                waveManager.enemies.Add(Instantiate(enemy, spawnPos.transform.position, Quaternion.identity));
            }
            else
            {
                waveManager.enemies.Add(Instantiate(rareEnemy, spawnPos.transform.position, Quaternion.identity));
            }
        }
    }
}
