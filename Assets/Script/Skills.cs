using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skills : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public int skillDmg;
    public int skillManaCost;
    public int skillCoinCost;
    public string bonusEffect;

    public void SetBonusEffect(string bonus){
        bonusEffect = bonus;
    }
}
