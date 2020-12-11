using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public string unitName;

    public int atk;
    public int maxHp;
    public int currentHp;
    public int maxMana;
    public int currentMana;

    public int coin;
    
    SkillDisplay skill;
    MainMenu deleteSkill;
    ApplySkill bonusSkill;
    List<Skills> boughtSkills = new List<Skills>();

    public InputField inputPlayerGo;

    public Text thankYou;

    // to calculate the damage it toke from the attack, return if the player is dead or not
    public bool TakeDamage(int damage){
        currentHp -= damage;
        if (currentHp <= 0){
            return true;
        }else{
            return false;
        }
    }
    // to calculate the mana before using skill
    public bool ManaLost(int manaCost){
        if (currentMana - manaCost >= 0){
            currentMana -= manaCost;
            return true;
        } else{
            return false;
        }
    }

    public List<Skills> GetSkill(){
        return boughtSkills;
    }

    // to buy skill
    public void OnClickPurchase(){
        // get the selected skill on SkillDisplay script
        skill = GameObject.Find("ShopMenuPanel").GetComponent<SkillDisplay>();
        if (coin - skill.tempSkill[0].skillCoinCost >= 0){
            if (boughtSkills.Count < 4){
                boughtSkills.Add(skill.tempSkill[0]);
                coin -= skill.tempSkill[0].skillCoinCost;
                thankYou.text = "Thank You For Your Purchase";
            }
            else{
                thankYou.text = "Not Enough Space";
            }
        }
        else{
            thankYou.text = "not enough money";
        }
    }

    // to delete a skill
    public void OnClickDeleteSkill(){
        // get the selected skill on MainMenu script
        deleteSkill = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        for (int i=0; i<boughtSkills.Count; i++)
        {
            if (boughtSkills[i].skillName.Contains(deleteSkill.tempSkill[0].skillName)){
                boughtSkills.Remove(boughtSkills[i]);
                deleteSkill.DeleteSkill();
                break;
            }
            else if (boughtSkills[i] == null){
                continue;
            }
        }
    }

    // to increase GO
    public void OnClickIncreaseGo(){
        coin += int.Parse(inputPlayerGo.text);
    }

    // to apply the bonus effect
    public void OnClickApply(){
        bonusSkill = GameObject.Find("ApplyBonusSkill").GetComponent<ApplySkill>();
        for (int i=0; i<boughtSkills.Count; i++)
        {
            if (boughtSkills[i].skillName.Contains(bonusSkill.tempSkill[0].skillName)){
                boughtSkills[i].bonusEffect = bonusSkill.tempBonus;
                break;
            }
            else if (boughtSkills[i] == null){
                continue;
            }
        }
    }

    // to play the game
    public void OnClickPlay(){
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
        DontDestroyOnLoad(this.gameObject);
    }
}
