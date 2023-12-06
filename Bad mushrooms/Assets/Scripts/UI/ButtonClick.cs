using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject nextMenu;
    

    public void ClickButton()
    {
        nextMenu.SetActive(true);
        startMenu.SetActive(false);
    }
}
