using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{   

    private Text levelText;
    private Image experienceBarImage;
    private LevelSystem levelSystem;

    public void Awake() 
    {
        levelText = transform.Find("LevelText").GetComponent<Text>();
        experienceBarImage = transform.Find("experienceBar").Find("bar").GetComponent<Image>();

    }
    
    //sets the experience bar size
    private void SetExperienceBarSize(float experienceNormalized) 
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    //sets the level number
    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "Level " + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem levelSystem) 
    {
        this.levelSystem = levelSystem;

        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }
}
