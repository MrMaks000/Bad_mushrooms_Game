using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float[] spawnIntervals;
    [SerializeField] private float waveInterval = 5;
    [SerializeField] private int[] enemyNumbers;
    [SerializeField] private int[] indexOfEnemyPrefabs;
    [SerializeField] protected Way way;

    private float defoultSpawnInterval = 1;
    private float spawnTimer = 0;
    private float spawnInterval = 0;
    private int indexOfSpawnIntervals = 0;
    private int indexOfEnemyNumbers = 0;
    private int numberOfSpawnEnemy = 0;
    private int indexOfindexOfEnemyPrefabs = 0;

    private void Start()
    {
        spawnInterval = waveInterval;
    }

    private void Update()
    {
        if (numberOfSpawnEnemy >= enemyNumbers[indexOfEnemyNumbers])
        {
            if (indexOfEnemyNumbers >= enemyNumbers.Length - 1) enabled = false;
            indexOfEnemyNumbers++;
            numberOfSpawnEnemy = 0;
            indexOfSpawnIntervals++;
            indexOfindexOfEnemyPrefabs++;
            spawnInterval = waveInterval;
        }
        

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnInterval = indexOfSpawnIntervals < spawnIntervals.Length ? spawnIntervals[indexOfSpawnIntervals] : defoultSpawnInterval;

            spawnTimer = 0;
                       
            SpawnEnemy.Spawn(enemyPrefabs[indexOfEnemyPrefabs[indexOfindexOfEnemyPrefabs]], way, transform.position);         
            if (indexOfindexOfEnemyPrefabs == indexOfEnemyPrefabs.Length - 1) enabled = false;

            numberOfSpawnEnemy++;
        }
    }

    
}
