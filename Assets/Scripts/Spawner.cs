using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] enemyPrefabs;
    public float timer = 10f;
    public float spawnInterval = 10f;
    public int maxSpawnCount = 10;
    public bool spawnEnabled = false;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    // Controls how often NavMes Agent should recalculate destination
    public float agentUpdateInterval = 1f;
    public UnityEvent<Spawner> onEnemySpawned = new UnityEvent<Spawner>();
    public UnityEvent<Spawner> onAllEnemiesSpawned = new UnityEvent<Spawner>();
    public bool spawnBehindPlayer = true;


    void Start()
    {
        if (!gameManager)
            gameManager = GameManager.GetGameManager(); 
        EnableSpawning();
    }


    public void DisableSpawning()
    {
        spawnEnabled = false;
        StopCoroutine(SpawnEnemy());
    }

    public void EnableSpawning()
    {
        spawnEnabled = true;
        StartCoroutine(SpawnEnemy());
    }

    public void DestroyAllEnemies()
    {
        foreach(GameObject go in spawnedEnemies)
        {
            if(go != null)
            {
                Destroy(go);
            }
            spawnedEnemies.Remove(go);

        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnInterval);
        if (spawnEnabled)
        {
            if (spawnedEnemies.Count < maxSpawnCount)
            {
                Vector3 spawnPosition;
                if(spawnBehindPlayer) {
                    Vector3 playerHeight = new Vector3(0, gameManager.player.rig.CameraYOffset, 0);
                    spawnPosition = gameManager.player.playerBehindTransform.position - playerHeight;
                } else
                {
                    spawnPosition = transform.position;
                }
          
                GameObject e = GameObject.Instantiate(getPrefab(), spawnPosition, Quaternion.identity);
                SpawnableEnemy se = e.GetComponentInChildren<SpawnableEnemy>();
                spawnedEnemies.Add(e);

       
                se.playerTransform = gameManager.player.rig.CameraFloorOffsetObject.transform;
                onEnemySpawned.Invoke(this);
                if (spawnedEnemies.Count >= maxSpawnCount)
                {
                    onAllEnemiesSpawned.Invoke(this);
                }
            }
            StartCoroutine(SpawnEnemy());
        }

    }

    private GameObject getPrefab()
    {
        int r = Mathf.FloorToInt(Random.Range(0, enemyPrefabs.Length));
        return enemyPrefabs[r];
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
