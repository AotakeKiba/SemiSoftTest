using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkillDisplay : MonoBehaviour
{
    public List<Skills> allSkills = new List<Skills>();

    public Text skillName;
    public Text skillDmg;
    public Text skillManaCost;
    public Text skillCoinCost;
    public Text bonusEffect;

    public Skills[] tempSkill = new Skills[1];

    string buySkillName;
    int buySkillDmg;
    int buySkillManaCost;
    int buySkillCoinCost;
    string buyBonusEffect;

    public void ShowSkills(int index){
        skillName.text = "Name: " + allSkills[index].skillName;
        skillDmg.text = "Damage: " + allSkills[index].skillDmg.ToString();
        skillManaCost.text = "Mana Cost: " + allSkills[index].skillManaCost.ToString();
        skillCoinCost.text = "Coin Cost: " + allSkills[index].skillCoinCost.ToString();

        tempSkill[0] = allSkills[index];
    }

    public void OnClickFireball(){
        ShowSkills(0);
    }
    public void OnClickWaterball(){
        ShowSkills(1);
    }
    public void OnClickEarthquake(){
        ShowSkills(2);
    }
    public void OnClickWindblade(){
        ShowSkills(3);
    }
    public void OnClickHolyLight(){
        ShowSkills(4);
    }
    public void OnClickDarkMatter(){
        ShowSkills(5);
    }

    public void OnClickCloseButton(){
        GameObject.Find ("MainMenu").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find ("ShopMenuPanel").transform.localScale = new Vector3(0, 0, 0);
    }

}
