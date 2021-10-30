using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveScript : MonoBehaviour
{
    [SerializeField] public float difficulty;
    private int currentWave;
    private int subWaves;
    [SerializeField] private GameObject spawner;
    [SerializeField] private List<GameObject> spawners;
    [SerializeField] private List<Vector3> spawnerPositions;
    private int enemiesInWave;
    private int enemiesPerWave;
    private int restInterval;
    public List<GameObject> enemies;
    public bool waveStarted;
    [SerializeField] private TextMeshProUGUI waveCounter;
    private DropManager dropManager;
    private bool lastWaveDropped = true;
    private void Start()
    {
        dropManager = GameObject.Find("DropManager").GetComponent<DropManager>();
        dropManager.DropWeapon();
        spawners = new List<GameObject>();
        waveStarted = false;
        enemies.Capacity = 0;
        currentWave = 1;
        restInterval = 3;
        StartCoroutine(StartWave());
    }
    private void FixedUpdate()
    {
        if (waveStarted)
        {
            waveCounter.text = "";
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                }
            }
            if(enemies.Count == 0)
            {
                waveStarted = false;
                ClearSpawners();
                StartCoroutine(StartWave());
            }
        }
    }
    private void GenerateWave()
    {
        difficulty = Random.Range(0.7f, 1.3f) * currentWave;
        if(ScoreScript.hardCore)
        {
            difficulty *= 1.5f;
        }
        enemiesInWave = (int)(Mathf.Sin(1.5f * difficulty) * 2 + (2f * difficulty));
        subWaves = Mathf.CeilToInt((float) enemiesInWave / spawnerPositions.Count * (int)Random.Range(1f, 3f));
        enemiesPerWave = Mathf.Clamp(Mathf.CeilToInt(enemiesInWave / subWaves), 0, spawnerPositions.Count);
        for(int i = 0; i < enemiesPerWave; i++)
        {
            GameObject spawned = Instantiate(spawner, spawnerPositions[i], Quaternion.identity);
            spawners.Add(spawned);
            SpawnerScript script = spawned.GetComponent<SpawnerScript>();
            script.bursts = subWaves;
            script.spawnInterval = restInterval;
            script.StartCoroutine(script.SpawnRoutine());
        }
        currentWave += 1;
        ScoreScript.AddScore((int)difficulty * 5);
    }
    private IEnumerator StartWave()
    {
        if (!lastWaveDropped)
        {
            int roundDiff = (int)difficulty;
            if (roundDiff == 3 || roundDiff == 7 || roundDiff == 11 || roundDiff == 15)
            {
                dropManager.DropWeapon();
                lastWaveDropped = true;
            }
            else
            {
                if (Random.Range(1, 9) < 5)
                {
                    dropManager.DropWeapon();
                    lastWaveDropped = true;
                }
            }
        }
        else
        {
            lastWaveDropped = false;
        }
        waveCounter.text = "Wave " + currentWave;
        yield return new WaitForSecondsRealtime(restInterval);
        GenerateWave();
        
    }
    private void ClearSpawners()
    {
        spawners.Clear();
    }
    
}
