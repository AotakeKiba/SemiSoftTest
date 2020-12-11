using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplySkill : MonoBehaviour
{
    public Player player;
    
    public Text skillName;
    public Text skillDmg;
    public Text skillManaCost;
    public Text skillBonus;
    public Text bonusEffect;

    public List<Skills> playerSkill = new List<Skills>();
    public List<Skills> tempSkill = new List<Skills>();
    public string tempBonus;

    public Text buttonSkill1;
    public Text buttonSkill2;
    public Text buttonSkill3;
    public Text buttonSkill4;

    void Update(){
        playerSkill = player.GetSkill();
    }

    public void OnClickExit(){
        GameObject.Find ("MainMenu").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find ("ApplyBonusSkill").transform.localScale = new Vector3(0, 0, 0);
    }

    // to show player's current skills
    public void ShowSkills(int index){

        skillName.text = "Name: " + playerSkill[index].skillName;
        skillDmg.text = "Damage: " + playerSkill[index].skillDmg.ToString();
        skillManaCost.text = "Mana Cost: " + playerSkill[index].skillManaCost.ToString();
        skillBonus.text = "Bonus Effect: " + playerSkill[index].bonusEffect;

        if(tempSkill.Count == 0){
            tempSkill.Add(playerSkill[index]);
        }else{
            tempSkill[0]=playerSkill[index];
        }
    }

    public void OnClickSkill1(){
        buttonSkill1.text = playerSkill[0].skillName;
        ShowSkills(0);
    }
    public void OnClickSkill2(){
        buttonSkill2.text = playerSkill[1].skillName;
        ShowSkills(1);
    }
    public void OnClickSkill3(){
        buttonSkill3.text = playerSkill[2].skillName;
        ShowSkills(2);
    }
    public void OnClickSkill4(){
        buttonSkill4.text = playerSkill[3].skillName;
        ShowSkills(3);
    }

    // to show bonus skill
    public void ShowEffect(string effect){
        bonusEffect.text = "Name: " + effect; 
        tempBonus = effect;
    }

    public void OnClickBonusEffect1(){
        ShowEffect("Atk+");
    }
    public void OnClickBonusEffect2(){
        ShowEffect("Atk++");
    }
    public void OnClickBonusEffect3(){
        ShowEffect("Atk+++");
    }
    public void OnClickBonusEffect4(){
        ShowEffect("Stun");
    }
}
