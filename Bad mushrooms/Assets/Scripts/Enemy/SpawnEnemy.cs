using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public static GameObject Spawn(GameObject enemyPrefabs, Way way, Vector3 SpawnPosition)
    {
        GameObject newEnemy = Instantiate(enemyPrefabs, SpawnPosition, Quaternion.identity);
        Enemy enemy = newEnemy.GetComponent<Enemy>();        
        enemy.GetWayPoints(way.GetWayPoints());
        return newEnemy;
    }
}
