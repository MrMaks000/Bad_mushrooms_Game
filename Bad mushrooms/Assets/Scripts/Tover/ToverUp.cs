using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToverUp : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Coins coins;

    private RaycastHit2D hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            towerPrefab.TryGetComponent<Tovers>(out var tover);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);


            if (hit.collider != null && hit.collider.TryGetComponent<Tovers>(out var upTover) && upTover.ImprovementStage == tover.ImprovementStage - 1 && upTover.typeDamag == tover.typeDamag)
            {
                if (coins.SpendCoins(tover.price) == true)
                {
                    BuildTower(upTover.transform.position);
                    upTover.gameObject.SetActive(false);
                    enabled = false;
                }
                    
            }


        }
    }

    private void BuildTower(Vector3 position)
    {
        GameObject newTower = Instantiate(towerPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }
}
