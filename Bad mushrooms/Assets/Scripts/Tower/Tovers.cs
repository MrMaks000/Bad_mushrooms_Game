using Assets.Scripts.Tover;
using System.Collections.Generic;
using UnityEngine;

public class Tovers : MonoBehaviour
{
    [SerializeField] public TypeOfDamag typeDamag;
    [SerializeField] protected float attackRange;
    [SerializeField] protected int attackSpeed;    
    [SerializeField] protected List<Enemy> visibleEnemies;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected int damage;
    

    protected List<Enemy> enemys = new List<Enemy>();
    protected float attackTimer = 0;

    public int ImprovementStage = 0;
    public int price = 10;

    private void Start()
    {
        
    }

    private void Update()
    {
    }

    protected virtual void Attack(Enemy enemy) 
    { 
        
    }
    protected virtual void Destroy() { }
    protected virtual void LevelUp() { }

    protected virtual void AddEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        bool deleteEnemy;


        foreach (Collider2D collider in hitColliders)
        {
            if (collider.TryGetComponent<Enemy>(out var enemy) == true)
            {
                if (enemys.Contains(enemy) == false)
                {
                    enemys.Add(enemy);
                }
            }
        }

        if (enemys.Count > 0)
        {
            for (int i = 0; i < enemys.Count; i++)
            {
                deleteEnemy = true;
                foreach (Collider2D collider in hitColliders)
                {

                    if (collider.TryGetComponent<Enemy>(out var colliderEnemy) == true && colliderEnemy == enemys[i]) deleteEnemy = false;
                }

                if (deleteEnemy == true)
                {
                    enemys.Remove(enemys[i]);
                    i--;
                }
            }
        }
    }


}
