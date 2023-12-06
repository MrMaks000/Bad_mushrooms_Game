using Assets.Scripts.Tover;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 10;    
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Image healthBarr;
    [SerializeField] protected int coin = 1;
    [SerializeField] protected int damage = 1;

    protected Vector3 maxBarr;
    protected GameObject[] wayPoints;
    protected int currentWayPoint = 0;
    protected Vector2 target;
    protected int CurrentHealth;


    private void Start()
    {

        maxBarr = healthBarr.rectTransform.localScale; 
        CurrentHealth = maxHealth;
    }

    private void Update()
    {
        Move();
        if (CurrentHealth <= 0)
        {
            Coins coinManager = Coins.GetInstance();
            if (coinManager != null)
            {
                coinManager.AddCoins(coin);
            }
            Die();
        }
            
    }

    public void GetDamage(int damage)
    {
        CurrentHealth -= damage;

        Vector3 barr = healthBarr.rectTransform.localScale;

        barr.x = CurrentHealth * 100 / maxHealth * maxBarr.x / 100;

        healthBarr.rectTransform.localScale = barr;
    }

    public void GetWayPoints(GameObject[] points)
    {
        wayPoints = points;
        target = wayPoints[currentWayPoint].transform.position;
        currentWayPoint++;
    }

    protected virtual void Die()
    {
        
        Destroy(gameObject);        
    }

    protected virtual void Move()
    {
        if (wayPoints == null) return;

        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed / 4 * Time.deltaTime);
        if (transform.position == new Vector3(target.x, target.y, 0))
        {
            if (currentWayPoint > wayPoints.Length - 1)
            {
                Health healthManager = Health.GetInstance();
                if (healthManager != null)
                {
                    healthManager.SpendHealth(damage);
                }
                Die();           
            }
            else
            {                
                target = wayPoints[currentWayPoint].transform.position;
                currentWayPoint++;
            }
        }      
    }
}
