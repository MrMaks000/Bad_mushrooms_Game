using UnityEngine;

public class AdjustPosition : MonoBehaviour
{
    [SerializeField] private Vector3 adjustVector;

    
    void Start()
    {
        transform.position += adjustVector;
    }

}
