using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapValidation : MonoBehaviour
{
    [SerializeField] private List<GameObject> mapObjects;
    [SerializeField] private List<Sprite> roadSprites;
    [SerializeField] private GameObject startRoad;
    [SerializeField] private GameObject endRoad;    
    [SerializeField] private BuildMap buildMap;

    [SerializeField] private bool[] leftAvailableRoads;
    [SerializeField] private bool[] topAvailableRoads;
    [SerializeField] private bool[] rightAvailableRoads;   
    [SerializeField] private bool[] bottomAvailableRoads;
    
    public GameObject[][] mapObjectsArray = new GameObject[9][];
    public Sprite[][] mapSpritsArray = new Sprite[9][];

    private void Start()
    {
        for (int i = 0; i < mapObjectsArray.Length; i++)
        {
            if (i == 0 || i % 2 == 0)
            {
                mapObjectsArray[i] = new GameObject[5];
                mapSpritsArray[i] = new Sprite[5];
            }
            else
            {
                mapObjectsArray[i] = new GameObject[4];
                mapSpritsArray[i] = new Sprite[4];
            }
        }
    }

    private void Update()
    {             
        foreach (var mapObject in mapObjects)
        {
            int indexOfElement = Convert.ToInt32(mapObject.name);
            int []rowCol = GetCoordinates(indexOfElement);
            mapObjectsArray[rowCol[0]][rowCol[1]] = mapObject;
            mapObject.TryGetComponent<SpriteRenderer>(out var roadSpriteRenderer);
            mapSpritsArray[rowCol[0]][rowCol[1]] = roadSpriteRenderer.sprite;
        } 
        
        buildMap.enabled = ConstructionValidationCheck();       
        enabled = false;
    }

    private bool ConstructionValidationCheck()
    {
        bool roadIs = false;
        for(int i = 0; i < mapSpritsArray.Length; i++)
        {
            for (int j = 0; j < mapSpritsArray[i].Length; j++)
            {
                if (roadSprites.Contains(mapSpritsArray[i][j]) == true)
                {
                    roadIs = true;
                    if (IsPathContinuous(mapSpritsArray[i][j], i, j) == false)
                    {
                        Debug.Log("Дорога має розриви.");
                        return false;
                    }
                }
            }                
        }

        if (roadIs == false) return false;

        Debug.Log("Дорога побудована правильно.");
        return true;
    }

    // Метод для перевірки, чи об'єкт є дорогою
    private bool IsRoad(Sprite sprite)
    {        
        return roadSprites.Contains(sprite);
    }

    private bool IsRoad(Sprite sprite, bool[] availableRoads)
    {
        for(int i = 0; i < roadSprites.Count; i++)
        {
            if (availableRoads[i] == true && sprite == roadSprites[i])
            {
                return true;
            }
        }
        return false;
    }

    // Метод, який перевіряє, чи дорога нерозривна
    private bool IsPathContinuous(Sprite sprite, int x, int y)
    {
        // Можливі напрямки дороги
        bool[] diractionsOfAvailableRoad = sprite.name.Split('-').Skip(1).First().Select(c => c == '1').ToArray();
        int X;
        int Y;
        // Початкова координата дороги
        int[] startRowCol = GetCoordinates(startRoad.name);
        // Кінцева координата дороги
        int[] endRowCol = GetCoordinates(endRoad.name);

        //Перевірка на те чи являється дорога початковою або кінцевою
        if (x == startRowCol[0] && y == startRowCol[1])
        {
            return true;
        }
        else if (x == endRowCol[0] && y == endRowCol[1])
        {
            return true;
        }

        if (diractionsOfAvailableRoad[0] == true)
        {
            //leftAvailableRoads
            if (x == 0 || x % 2 == 0)
            {
                if (y > 0)
                {
                    X = x + 1;
                    Y = y - 1;                     
                }
                else return false; 
            }
            else
            {
                X = x + 1;
                Y = y;
            }
            if (IsRoad(mapSpritsArray[X][Y], leftAvailableRoads) == false)
            {
                return false;
            }
        }

        if (diractionsOfAvailableRoad[1] == true)
        {
            //topAvailableRoads
            if (x == 0 || x % 2 == 0)
            {
                if (y > 0 && x > 0)
                {
                    X = x - 1;
                    Y = y - 1;
                }
                else return false;
            }
            else
            {
                X = x - 1;
                Y = y;                               
            }
            if (IsRoad(mapSpritsArray[X][Y], topAvailableRoads) == false)
            {
                return false;
            }
        }

        if (diractionsOfAvailableRoad[2] == true)
        {
            //rightAvailableRoads
            if (x == 0 || x % 2 == 0)
            {
                if (y < 4 && x > 0)
                {
                    X = x - 1;
                    Y = y;
                }
                else return false;
            }
            else
            {
                X = x - 1;
                Y = y + 1;                
            }
            if (IsRoad(mapSpritsArray[X][Y], rightAvailableRoads) == false)
            {
                return false;
            }
        }

        if (diractionsOfAvailableRoad[3] == true)
        {
            //bottomAvailableRoads
            if (x == 0 || x % 2 == 0)
            {
                if (y < 4)
                {
                    X = x + 1;
                    Y = y;
                }
                else return false;
            }
            else
            {
                X = x + 1;
                Y = y + 1;
            }
            if (IsRoad(mapSpritsArray[X][Y], bottomAvailableRoads) == false)
            {
                return false;
            }
        }

        return true;
    }

    private int[] GetCoordinates(int value)
    {
        return new int[2]
        {
            value / 10,
            value % 10
        };
    }

    private int[] GetCoordinates(string value)
    {
        return GetCoordinates(Convert.ToInt32(value));
    }
}
