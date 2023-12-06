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

    protected Vector3 maxBarr;
    protected GameObject[] wayPoints;
    protected int currentWayPoint = 0;
    protected Vector2 target;
    protected int Health;


    private void Start()
    {

        maxBarr = healthBarr.rectTransform.localScale; 
        Health = maxHealth;
    }

    private void Update()
    {
        Move();
        if (Health <= 0)
        {
            Die();
        }
            
    }

    public void GetDamage(int damage)
    {
        Health -= damage;

        Vector3 barr = healthBarr.rectTransform.localScale;

        barr.x = Health * 100 / maxHealth * maxBarr.x / 100;

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
        Coins coinManager = Coins.GetInstance();
        if (coinManager != null)
        {
            coinManager.AddCoins(coin);
        }
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
