using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystemAnimated : MonoBehaviour
{
    private LevelSystem levelSystem;
    //boolean for whether the animation is active or not
    private bool isAnimating;
    //take variables from LevelSystem class
    private int level;
    private int experience;
    private int experienceToNextLevel;

    public LevelSystemAnimated(LevelSystem levelSystem)
    {
        SetLevelSystem(levelSystem);

    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem; 

        level = levelSystem.GetLevelNumber();
        experience = levelSystem.GetExperience();
        experienceToNextLevel = levelSystem.GetExperienceToNextLevel();


        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    private void Update()
    {
        if (isAnimating) 
        {
            //local level under target level
            if (level < levelSystem.GetLevelNumber()) 
            {
                AddExperience();
            } 
            //local level equals the target level
            else
            {
                if (experience < levelSystem.GetExperience())
                {
                    AddExperience();
                }
                //stops animation when we've exact same level and experience
                else
                {
                    isAnimating = false;
                }
            }
            
        }
        Debug.Log(level + " " + experience);

    }

    private void AddExperience()
    {
        experience++;
        if (experience >= experienceToNextLevel)
        {
            level++;
            experience = 0;
        }
    }

}
