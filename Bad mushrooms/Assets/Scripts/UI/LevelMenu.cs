using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private List<Sprite> spritesOfMap;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;

    private int indexOfSprite = 0;

    private void Start()
    {
        RenderLevel();
    }

    private void RenderLevel()
    {
        if (spritesOfMap.Count == 0) return;
        if (indexOfSprite >= spritesOfMap.Count) indexOfSprite = 0;
        if (indexOfSprite < 0) indexOfSprite = spritesOfMap.Count - 1;
        image.sprite = spritesOfMap[indexOfSprite];
        text.text = "Level " + (indexOfSprite + 1);
    }

    public void ChangeLevelLeft()
    {
        indexOfSprite--;
        RenderLevel();
    }

    public void ChangeLevelRight()
    {
        indexOfSprite++;
        RenderLevel();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level_" + (indexOfSprite + 1));
    }



}
