using System;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coins;

    private static Coins instance;

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

    public static Coins GetInstance()
    {
        return instance;
    }

    public void AddCoins(int count)
    {
        coins.text = Convert.ToString(Convert.ToInt32(coins.text) + count);
    }

    public bool SpendCoins(int count)
    {
        if (Convert.ToInt32(coins.text) - count < 0) return false;
        coins.text = Convert.ToString(Convert.ToInt32(coins.text) - count);
        return true;
    }
}
