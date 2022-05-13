using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    
    DDOL ddol;
    DDOLstats savedStats;

    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    //NEW
    bool hasLoadedStats = false;

    //NEW

    private void Awake()
    {
        ddol = GameObject.FindGameObjectWithTag("DDOL").GetComponent<DDOL>();
        savedStats = GameObject.FindGameObjectWithTag("DDOL").GetComponentInChildren<DDOLstats>();
    }
    private void Start()
    {
        hasLoadedStats = savedStats.hasLoadedStats;
        if (hasLoadedStats && gameObject.CompareTag("Player"))
        {
            LoadStats();
        }

        ddol.FindPlayer();
        savedStats.OnPlayerStart();

    }

    public bool TakeDamage(int DamageTaken)
    {
        currentHP = currentHP - DamageTaken;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void LoadStats()
    {
        maxHP = savedStats.maxHP;
        currentHP = savedStats.currentHP;
        damage = savedStats.damage;
        unitLevel = savedStats.unitLevel;
        unitName = savedStats.unitName;
    }
}
