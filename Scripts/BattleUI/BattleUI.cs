using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public Slider hpSlider;

    public void SetHUD(Stats stats)
    {
        nameText.text = stats.unitName;
        levelText.text = "Level: " + stats.unitLevel;
        hpSlider.maxValue = stats.maxHP;
        hpSlider.value = stats.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
 
}
