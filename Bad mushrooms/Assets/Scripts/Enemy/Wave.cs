using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float[] spawnIntervals;
    [SerializeField] private float waveInterval = 5;
    [SerializeField] private int[] enemyNumbers;
    [SerializeField] private int[] indexOfEnemyPrefabs;
    [SerializeField] private Way way;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject buildMenu;

    private List<GameObject> enemyList = new List<GameObject>();
    private bool enabledEnemy = false;
    private bool waveIsOver = false;

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
        if (waveIsOver == false)
        {
            if (numberOfSpawnEnemy >= enemyNumbers[indexOfEnemyNumbers])
            {
                if (indexOfEnemyNumbers >= enemyNumbers.Length)
                {
                    waveIsOver = true;
                    return;
                }
                indexOfEnemyNumbers++;
                numberOfSpawnEnemy = 0;
                indexOfSpawnIntervals++;
                indexOfindexOfEnemyPrefabs++;
                if (indexOfindexOfEnemyPrefabs >= indexOfEnemyPrefabs.Length)
                {
                    waveIsOver = true;
                    return;
                }
                spawnInterval = waveInterval;
            }


            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                spawnInterval = indexOfSpawnIntervals < spawnIntervals.Length ? spawnIntervals[indexOfSpawnIntervals] : defoultSpawnInterval;

                spawnTimer = 0;

                enemyPrefabs[indexOfEnemyPrefabs[indexOfindexOfEnemyPrefabs]].TryGetComponent<SpriteRenderer>(out var prefabSpriteRenderer);
                foreach (GameObject enemy in enemyList)
                {
                    enemy.TryGetComponent<SpriteRenderer>(out var spriteRenderer);
                    
                    if (enemy.activeSelf == false && spriteRenderer.sprite == prefabSpriteRenderer.sprite)
                    {
                        enemy.transform.position = transform.position;
                        enemy.SetActive(true);
                        enemy.TryGetComponent<Enemy>(out var enemyObject);
                        enemyObject.GetWayPoints(way.GetWayPoints());
                        
                        enabledEnemy = true;
                        numberOfSpawnEnemy++;
                        break;
                    }
                }
                if (enabledEnemy == false)
                {
                    enemyList.Add(SpawnEnemy.Spawn(enemyPrefabs[indexOfEnemyPrefabs[indexOfindexOfEnemyPrefabs]], way, transform.position));
                    numberOfSpawnEnemy++;
                }
                enabledEnemy = false;               
            }           
        }
        else if (enemyList.Count >0 && waveIsOver == true)
        {
            foreach (GameObject enemy in enemyList)
            {
                if (enemy.activeSelf == true)
                {
                    return;
                }
            }
            winMenu.SetActive(true);
            buildMenu.SetActive(false);
            enabled = false;
        }
    }

    
}
