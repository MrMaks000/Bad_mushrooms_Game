using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeComplexityText : MonoBehaviour
{
    [SerializeField] private ChangeComplexity ComplexityLevel;
    [SerializeField] private TextMeshProUGUI ComplexityText;

    private void Update()
    {
        int complexity = ComplexityLevel.GetComplexity();
        if (complexity == 0) { ComplexityText.text = "Easy"; }
        else if (complexity == 1){ ComplexityText.text = "Medium"; }
        else if (complexity == 2) { ComplexityText.text = "Hard"; }

    }
}
