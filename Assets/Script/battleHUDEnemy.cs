using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleHUDEnemy : MonoBehaviour
{
    public Text nameText;
    public Slider hpSlider;
    public Slider manaSlider;

    public void setHUD(Enemy enemy){
        nameText.text = enemy.unitName;
        hpSlider.maxValue = enemy.maxHp;
        hpSlider.value = enemy.currentHp;
        manaSlider.maxValue = enemy.maxMana;
        manaSlider.value = enemy.currentMana;
    }

    public void SetHp(int hp){
        hpSlider.value = hp;
    }
    
    public void SetMana(int mana){
        manaSlider.value = mana;
    }
}
