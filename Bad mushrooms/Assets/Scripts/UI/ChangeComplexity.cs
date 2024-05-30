using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeComplexity : MonoBehaviour
{
    private static int Complexity = 0;

    public void ChangeComplexityRight()
    {
        Complexity++;
        FixComplexity();
    }

    public void ChangeComplexityLeft()
    {
        Complexity--;
        FixComplexity();
    }

    private void FixComplexity()
    {
        if(Complexity > 2)
            Complexity = 0;
        else if(Complexity < 0)
            Complexity = 2;
    }

    public int GetComplexity()
    {
        return Complexity;
    }
}
