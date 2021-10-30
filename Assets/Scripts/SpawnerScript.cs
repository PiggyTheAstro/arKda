using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject spawn;
    public GameObject rangedSpawn;
    public GameObject tankSpawn;
    public float spawnInterval;
    public int bursts;
    private WaveScript waveManager;
    [SerializeField] private ParticleSystem vfx;
    // Start is called before the first frame update
    private void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveScript>();
    }
    // Update is called once per frame
    public IEnumerator SpawnRoutine()
    {
        ScoreScript.AddScore(bursts * 2);
        for(int i = 0; i < bursts; i++)
        {
            yield return new WaitForSecondsRealtime(spawnInterval);
            waveManager.enemies.Capacity += 1;
            if (waveManager.difficulty < 5f || bursts < 2)
            {
                waveManager.enemies.Add(Instantiate(spawn, transform.position, Quaternion.identity));
            }
            else
            {
                int randint = Random.Range(1, 6);
                if(randint == 1)
                {
                    waveManager.enemies.Add(Instantiate(rangedSpawn, transform.position, Quaternion.identity));
                    bursts -= 2;
                }
                else if(randint == 2 && waveManager.difficulty > 8f)
                {
                    waveManager.enemies.Add(Instantiate(tankSpawn, transform.position, Quaternion.identity));
                    bursts -= 2;
                }
                else
                {
                    waveManager.enemies.Add(Instantiate(spawn, transform.position, Quaternion.identity));
                }
            }
            waveManager.waveStarted = true;
        }
        transform.DetachChildren();
        vfx.Stop();
        ScoreScript.AddScore(30);
        Destroy(gameObject);
    }
}
