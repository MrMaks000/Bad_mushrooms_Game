using Unity.VisualScripting;
using UnityEngine;


public class BuildMapElement : MonoBehaviour
{
    [SerializeField] private GameObject builderPrefab;
    [SerializeField] private Sprite builderSprite;
    [SerializeField] private Camera mainCamera;

    private RaycastHit2D hit;
    private GameObject availablePlace;
    private GameObject nonAvailablePlace;

    private void Start()
    {
        availablePlace = Instantiate(builderPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        availablePlace.TryGetComponent<SpriteRenderer>(out var availablePlaceSpriteRenderer);
        availablePlaceSpriteRenderer.color = new Color(0f, 1f, 0f, 0.5f);
        availablePlace.SetActive(false);

        nonAvailablePlace = Instantiate(builderPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        nonAvailablePlace.TryGetComponent<SpriteRenderer>(out var nonAvailablePlaceSpriteRenderer);
        nonAvailablePlaceSpriteRenderer.color = new Color(1f, 0f, 0f, 0.5f);
        nonAvailablePlace.SetActive(false);
    }

    private void Update()
    {        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null && hit.collider.TryGetComponent<AvailableForBuild>(out var availableBuilderPlase))
        {
            availablePlace.transform.position = availableBuilderPlase.transform.position + new Vector3(0f, -0.4f, -0.1f);
            availablePlace.SetActive(true);
            nonAvailablePlace.SetActive(false);

            if (Input.GetMouseButtonDown(0))
            {
                var spriteRenderer = availableBuilderPlase.GetComponentInParent<SpriteRenderer>();
                spriteRenderer.sprite = builderSprite;
                //availablePlace.SetActive(false);
                //this.enabled = false;
            }
        }
        else if(hit.collider != null && hit.collider.TryGetComponent<NonAvailableForBuild>(out var nonAvailableBuilderPlase))
        {
            nonAvailablePlace.transform.position = nonAvailableBuilderPlase.transform.position + new Vector3(0f, -0.4f, -0.1f);
            nonAvailablePlace.SetActive(true);
            availablePlace.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // Встановлення обидві змінні на false перед вимкненням скрипту
        if (nonAvailablePlace != null)
            nonAvailablePlace.SetActive(false);
        if (availablePlace != null)
            availablePlace.SetActive(false);
    }
}




