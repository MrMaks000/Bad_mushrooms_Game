using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private ChangeComplexity ComplexityLevel;
    [SerializeField] private List<GameObject> Wawes;

    private void Start()
    {
        Wawes[ComplexityLevel.GetComplexity()].SetActive(true);
    }
}
