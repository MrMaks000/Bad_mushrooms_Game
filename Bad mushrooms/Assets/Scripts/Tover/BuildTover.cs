using Assets.Scripts.Tover;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuildTover : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab; 
    [SerializeField] private GameObject grassPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Coins coins;

    private RaycastHit2D hit;
    private float Displacement;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            towerPrefab.TryGetComponent<Tovers>(out var tover);
            if (tover.typeDamag == TypeOfDamag.archer) Displacement = 0.85f;
            if (tover.typeDamag == TypeOfDamag.magic) Displacement = 0.45f;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.TryGetComponent<ToverSpawnPlase>(out var spawnPlase))
            {
                if (coins.SpendCoins(tover.price) == true)
                {
                    BuildTower(spawnPlase.transform.position);
                    spawnPlase.gameObject.SetActive(false);
                    enabled = false;
                }

            }
        }
    }

    private void BuildTower(Vector3 position)
    {
        GameObject newTower = Instantiate(towerPrefab, new Vector3(position.x, position.y + Displacement, 0), Quaternion.identity);
        GameObject grass = Instantiate(grassPrefab, new Vector3(position.x, position.y, position.z), Quaternion.identity);
    }
}