using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindWeyPoints : MonoBehaviour
{
    [SerializeField] private Way way;
    [SerializeField] private Wave wave;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private bool[] startDiraction;

    private List<bool[]> previousDiraction = new List<bool[]>();
    private List<bool[]> unavailableDirections = new List<bool[]>();
    private List<Vector3> unavailableDirectionsPosition = new List<Vector3>();
    private List<GameObject> roads = new List<GameObject>();
    private List<GameObject> sortedRoad = new List<GameObject>();
    private List<List<GameObject>> sortedRoads = new List<List<GameObject>>();
    private Vector3 nextPosition = new Vector3();   
    private int i = -1;    

    private void Start()
    {
        nextPosition = startPosition.position;
        previousDiraction.Add(startDiraction);
    }

    private void Update()
    {
        i++;
        for (int j = 0; j < roads.Count; j++)
        {
            if (roads[j].transform.position == nextPosition)
            {
                if (sortedRoad.Contains(roads[j]))
                {
                    CloseDiractionsOfRoad(sortedRoad);
                    nextPosition = startPosition.position;
                    previousDiraction.Clear();
                    previousDiraction.Add(startDiraction);
                    sortedRoad.Clear();
                    i = -1;
                    return;
                }
                sortedRoad.Add(roads[j]);
            }
        }
        
        
        if (nextPosition == endPosition.position)
        {
            List<GameObject> list = new List<GameObject>();
            for (int k = 0; k < sortedRoad.Count; k++)
            {
                GameObject tempObject = new GameObject();
                tempObject.transform.position = sortedRoad[k].transform.position;
                list.Add(tempObject);
            }
            sortedRoads.Add(list);

            if (CloseDiractionsOfRoad(sortedRoad) == true)
            {
                List<List<GameObject>> listOfLists = new List<List<GameObject>>();
                for (int j = 0; j < sortedRoads.Count; j++)
                {
                    List<GameObject> tempList = new List<GameObject>();
                    for (int k = 0; k < sortedRoads[j].Count; k++)
                    {
                        GameObject tempObject = new GameObject();
                        tempObject.transform.position = sortedRoads[j][k].transform.position + new Vector3(0f, 0.5f, -1f);
                        tempList.Add(tempObject);
                    }
                    listOfLists.Add(tempList);
                }
                way.SetWeyPoints(listOfLists);
                wave.enabled = true;
                enabled = false;
            }

            nextPosition = startPosition.position;
            previousDiraction.Clear();
            previousDiraction.Add(startDiraction);
            sortedRoad.Clear();
            i = -1;
            return;
        }

        sortedRoad[i].TryGetComponent<SpriteRenderer>(out var spriteRenderer);
        Sprite sprite = spriteRenderer.sprite;

        bool[] diractionsOfAvailableRoad = new bool[4];
        
        diractionsOfAvailableRoad = sprite.name.Split('-').Skip(1).First().Select(c => c == '1').ToArray();

        for (int j = 0; j < diractionsOfAvailableRoad.Length; j++)
        {
            diractionsOfAvailableRoad[j] = previousDiraction[i][j] & diractionsOfAvailableRoad[j];
        }

        for (int j = 0; j < unavailableDirectionsPosition.Count; j++)
        {
            if (sortedRoad[i].transform.position == unavailableDirectionsPosition[j]) 
            {
                for (int k = 0; k < diractionsOfAvailableRoad.Length; k++)
                {
                    diractionsOfAvailableRoad[k] = unavailableDirections[j][k] & diractionsOfAvailableRoad[k];
                }
            }
        }

        if (diractionsOfAvailableRoad[0] == true)
        {

            previousDiraction.Add(new bool[] { true, true, false, true });
            nextPosition = (new Vector3(
                nextPosition.x - 1.25f,
                nextPosition.y - 0.75f,
                nextPosition.z - 1f
                ));
        }
        else if (diractionsOfAvailableRoad[1] == true)
        {
            previousDiraction.Add(new bool[] { true, true, true, false });
            nextPosition = (new Vector3(
                nextPosition.x - 1.25f,
                nextPosition.y + 0.75f,
                nextPosition.z + 1f
                ));
        }
        else if (diractionsOfAvailableRoad[2] == true)
        {
            previousDiraction.Add(new bool[] { false, true, true, true });
            nextPosition = (new Vector3(
                nextPosition.x + 1.25f,
                nextPosition.y + 0.75f,
                nextPosition.z + 1f
                ));
        }
        else if (diractionsOfAvailableRoad[3] == true)
        {
            previousDiraction.Add(new bool[] { true, false, true, true });
            nextPosition = (new Vector3(
                nextPosition.x + 1.25f,
                nextPosition.y - 0.75f,
                nextPosition.z - 1f
                ));
        }
    }

    private bool CloseDiractionsOfRoad(List<GameObject> road)
    {
        for (int i = road.Count - 1; i >= 0; i--)
        {           
            road[i].TryGetComponent<SpriteRenderer>(out var spriteRenderer);
            Sprite sprite = spriteRenderer.sprite;

            List<bool> diractionsOfAvailableRoad = new List<bool>();

            diractionsOfAvailableRoad = sprite.name.Split('-').Skip(1).First().Select(c => c == '1').ToList();

            for (int j = 0; j < diractionsOfAvailableRoad.Count; j++)
            {                
                diractionsOfAvailableRoad[j] = previousDiraction[i][j] & diractionsOfAvailableRoad[j];               
            }

            for (int j = 0; j < unavailableDirectionsPosition.Count; j++)
            {
                if (road[i].transform.position == unavailableDirectionsPosition[j])
                {
                    for (int k = 0; k < diractionsOfAvailableRoad.Count; k++)
                    {
                        diractionsOfAvailableRoad[k] = unavailableDirections[j][k] & diractionsOfAvailableRoad[k];
                    }
                }
            }

            if (diractionsOfAvailableRoad.Count(n => n == true) > 1)
            {
                unavailableDirectionsPosition.Add(road[i].transform.position);
                    if (diractionsOfAvailableRoad[0] == true)
                {
                    unavailableDirections.Add(new bool[] { false, true, true, true });
                }
                else if (diractionsOfAvailableRoad[1] == true)
                {
                    unavailableDirections.Add(new bool[] { true, false, true, true });
                }
                else if (diractionsOfAvailableRoad[2] == true)
                {
                    unavailableDirections.Add(new bool[] { true, true, false, true });
                }
                else if (diractionsOfAvailableRoad[3] == true)
                {
                    unavailableDirections.Add(new bool[] { true, true, true, false });
                }
                return false;
            }
        }
        return true;
    }

    public void SetRoads(List<GameObject> roads)
    {
        this.roads = roads;
    }
}
