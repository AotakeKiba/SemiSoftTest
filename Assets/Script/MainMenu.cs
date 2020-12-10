using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    public Text coinAmount;
    public Player player;

    public Text skillName;
    public Text skillDmg;
    public Text skillManaCost;
    public Text skillCoinCost;
    public Text bonusEffect;

    public Text buttonSkill1;
    public Text buttonSkill2;
    public Text buttonSkill3;
    public Text buttonSkill4;

    public List<Skills> playerSkill = new List<Skills>();
    public List<Skills> tempSkill = new List<Skills>();

    void Start()
    {
        GameObject.Find ("ShopMenuPanel").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find ("ApplyBonusSkill").transform.localScale = new Vector3(0, 0, 0);
    }

    public void OnClickRefreshButton(){
        coinAmount.text = player.coin.ToString();

        playerSkill = player.GetSkill();
        if (playerSkill[0]!=null){
            buttonSkill1.text = playerSkill[0].skillName;
        }else{
            buttonSkill1.text = "XXXX";
        }
        if (playerSkill[1]!=null){
            buttonSkill2.text = playerSkill[1].skillName;
        }else{
            buttonSkill2.text = "XXXX";
        }
        if (playerSkill[2]!=null){
            buttonSkill3.text = playerSkill[2].skillName;
        }else{
            buttonSkill3.text = "XXXX";
        }
        if (playerSkill[3]!=null){
            buttonSkill4.text = playerSkill[3].skillName;
        }else{
            buttonSkill4.text = "XXXX";
        }
    }

    // to open the shop skill panel
    public void OnClickBuySkill(){
        GameObject.Find ("ShopMenuPanel").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find ("MainMenu").transform.localScale = new Vector3(0, 0, 0);
    }

    // to open the bonus skill panel
    public void OnClickApplyBonus(){
        GameObject.Find ("ApplyBonusSkill").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find ("MainMenu").transform.localScale = new Vector3(0, 0, 0);
    }

    public void DeleteSkill(){
        tempSkill.Remove(tempSkill[0]);
    }

    // to show the current skill description
    public void ShowSkills(int index){

        skillName.text = "Name: " + playerSkill[index].skillName;
        skillDmg.text = "Damage: " + playerSkill[index].skillDmg.ToString();
        skillManaCost.text = "Mana Cost: " + playerSkill[index].skillManaCost.ToString();
        skillCoinCost.text = "Coin Cost: " + playerSkill[index].skillCoinCost.ToString();
        bonusEffect.text = "Bonus Effect: " + playerSkill[index].bonusEffect;

        if(tempSkill.Count == 0){
            tempSkill.Add(playerSkill[index]);
        }else{
            tempSkill[0]=playerSkill[index];
        }
    }

    public void OnClickSkill1(){
        ShowSkills(0);
    }
    public void OnClickSkill2(){
        ShowSkills(1);
    }
    public void OnClickSkill3(){
        ShowSkills(2);
    }
    public void OnClickSkill4(){
        ShowSkills(3);
    }
}
