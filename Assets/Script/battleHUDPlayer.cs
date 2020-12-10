using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleHUDPlayer : MonoBehaviour
{
    public Text nameText;
    public Slider hpSlider;
    public Slider manaSlider;

    public void setHUD(Player player){
        nameText.text = player.unitName;
        hpSlider.maxValue = player.maxHp;
        hpSlider.value = player.currentHp;
        manaSlider.maxValue = player.maxMana;
        manaSlider.value = player.currentMana;
    }

    public void SetHp(int hp){
        hpSlider.value = hp;
    }

    public void SetMana(int mana){
        manaSlider.value = mana;
    }
}
