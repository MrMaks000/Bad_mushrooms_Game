using System;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private GameObject spawn;

    private static Health instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (health.text == "0")
        {
            loseMenu.SetActive(true);
            buildMenu.SetActive(false);
            spawn.SetActive(false);
        }
    }

    public static Health GetInstance()
    {
        return instance;
    }

    public bool SpendHealth(int count)
    {
        if (Convert.ToInt32(health.text) - count < 0)
        {
            health.text = "0";
            return false; 
        }
        health.text = Convert.ToString(Convert.ToInt32(health.text) - count);
        return true;
    }
}
