using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOLstats : MonoBehaviour
{
    //stores the Player stats and allows for persistence between scenes
    Stats playerStats;

    //variables
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;

    public bool hasLoadedStats = false;

    //bool that ensures stats will load onto the Player Object
    public void OnPlayerStart()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        if (hasLoadedStats == false)
        {
            LoadStats();
            hasLoadedStats = true;
        }
    }

    //loads the Player stats variables from the Stats class
    public void LoadStats()
    {
        maxHP = playerStats.maxHP;
        currentHP = playerStats.currentHP;
        damage = playerStats.damage;
        unitLevel = playerStats.unitLevel;
        unitName = playerStats.unitName;
    }
}
