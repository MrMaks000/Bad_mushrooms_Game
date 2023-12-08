using UnityEngine;
using System.Linq;

public class Archer : Tovers
{ 

    private void Update()
    {
        attackTimer += Time.deltaTime * attackSpeed;

        if (attackTimer >= 10)
        {
            AddEnemy();
            if (enemys != null && enemys.Count > 0)
            {
                if (enemys.First() == null) enemys.Remove(enemys.First());
                Attack(enemys.First());
            }            
            attackTimer = 0f;
        }
    }

    protected override void Attack(Enemy enemy)
    {
        if (projectilePrefab != null)
        {           
            GameObject arrow = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.Euler(0f, 0f, 0f));
            Arrow arrowScript = arrow.GetComponent<Arrow>();

            if (arrowScript != null)
            {
                arrowScript.SetTarget(enemy, damage);
            }
        }
    }

    
}
