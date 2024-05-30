using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildMap : MonoBehaviour
{
    [SerializeField] private List<GameObject> mapObjects;
    [SerializeField] private List<Sprite> elementSprites;
    [SerializeField] private List<GameObject> prefabsOfObjects;

    [SerializeField] private GameObject buildCanvas;
    [SerializeField] private GameObject playCanvas;
    [SerializeField] private FindWeyPoints findWeyPoints;

    private List<GameObject> newMap = new List<GameObject>();
    private List<GameObject> roads = new List<GameObject>();

    private void Start()
    {
        newMap.Capacity = mapObjects.Count;
    }

    private void Update()
    {
        InstantiateMap();

        playCanvas.SetActive(true);
        buildCanvas.SetActive(false);
    }

    private void InstantiateMap()
    {
        for (int i = 0; i < mapObjects.Count; i++)
        {
            for (int j = 0; j < elementSprites.Count; j++)
            {
                mapObjects[i].TryGetComponent<SpriteRenderer>(out var spriteRenderer);
                if (spriteRenderer.sprite == elementSprites[j])
                {
                    Vector3 vector3 = new Vector3(mapObjects[i].transform.position.x,
                        mapObjects[i].transform.position.y, mapObjects[i].transform.position.z);
                    newMap.Add(Instantiate(prefabsOfObjects[j], vector3, Quaternion.identity));
                    mapObjects[i].SetActive(false);

                    if(j > 1)
                    {
                        roads.Add(newMap[i]);
                    }    
                }
            }
        }

        findWeyPoints.enabled = true;
        findWeyPoints.SetRoads(roads);
    }
}
