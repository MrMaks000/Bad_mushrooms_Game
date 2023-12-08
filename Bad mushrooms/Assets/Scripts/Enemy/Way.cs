using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] protected List<GameObject[]> ways;

    [SerializeField] private GameObject[] way;

    public GameObject[] GetWayPoints()
    {
        FindingTheWay();
        if (way != null) return way;
        else return null;
    }

    private void FindingTheWay()
    {
        if (ways == null) { return; }

        for (int i = 0; i < ways.Count; i++)
        {
            way[i] = ways[UnityEngine.Random.Range(0, ways.Count)][i];
        }
    }

    
}
