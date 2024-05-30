using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] private List<GameObject> way;

    private List<List<GameObject>> ways;

    public List<GameObject> GetWayPoints()
    {
        if (ways != null) return ways[UnityEngine.Random.Range(0, ways.Count)];
        if (way != null) return way;
        else return null;
    }

    private void Bias(float x, float y)
    {
        foreach (var element in way)
        {
            element.transform.position = element.transform.position + new Vector3(x, y, 0);
        }
    }

    public void SetWeyPoints(List<GameObject> way)
    {
        this.way = way;
    }

    public void SetWeyPoints(List<GameObject> way, float biasX, float biasY)
    {
        this.way = way;
        Bias(biasX, biasY);
    }

    public void SetWeyPoints(List<List<GameObject>> ways)
    {
        this.ways = ways;
    }
}
