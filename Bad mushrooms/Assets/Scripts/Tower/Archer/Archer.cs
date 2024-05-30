using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Archer : Tovers
{
    private List<GameObject> arrows = new List<GameObject>();

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
            foreach (var tempArrow in arrows)
            {
                if (tempArrow.activeSelf == false)
                {
                    tempArrow.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
                    tempArrow.SetActive(true);
                    if (tempArrow.TryGetComponent<Arrow>(out var tempArrowScript))
                    {
                        tempArrowScript.SetTarget(enemy, damage);
                    }
                    return;

                }
            }
            GameObject arrow = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.Euler(0f, 0f, 0f));
            if (arrow.TryGetComponent<Arrow>(out var arrowScript))
            {
                arrowScript.SetTarget(enemy, damage);
            }
            arrows.Add(arrow);
        }
    }

    
}
