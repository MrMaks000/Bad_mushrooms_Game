using UnityEngine;

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
