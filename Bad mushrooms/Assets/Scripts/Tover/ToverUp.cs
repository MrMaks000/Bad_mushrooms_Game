using UnityEngine;

public class ToverUp : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab1;
    [SerializeField] private GameObject towerPrefab2;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Coins coins;

    private RaycastHit2D hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            towerPrefab1.TryGetComponent<Tovers>(out var tover1);
            towerPrefab2.TryGetComponent<Tovers>(out var tover2);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);


            if (hit.collider != null && hit.collider.TryGetComponent<Tovers>(out var upTover))
            {
                if (upTover.ImprovementStage == tover1.ImprovementStage - 1 && upTover.typeDamag == tover1.typeDamag)
                {
                    if (coins.SpendCoins(tover1.price) == true)
                    {
                        BuildTower(upTover.transform.position, towerPrefab1);
                        upTover.gameObject.SetActive(false);
                        enabled = false;
                    }
                }
                else if (upTover.ImprovementStage == tover2.ImprovementStage - 1 && upTover.typeDamag == tover2.typeDamag)
                {
                    if (coins.SpendCoins(tover2.price) == true)
                    {
                        BuildTower(upTover.transform.position, towerPrefab2);
                        upTover.gameObject.SetActive(false);
                        enabled = false;
                    }
                }


            }


        }
    }

    private void BuildTower(Vector3 position, GameObject towerPrefab)
    {
        GameObject newTower = Instantiate(towerPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }
}
