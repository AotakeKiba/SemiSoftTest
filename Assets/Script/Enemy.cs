using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string unitName;

    public int atk;
    public int maxHp;
    public int currentHp;
    public int maxMana;
    public int currentMana;

    public bool TakeDamage(int damage){
        currentHp -= damage;
        if (currentHp <= 0){
            return true;
        }else{
            return false;
        }
    }
    public bool ManaLost(int manaCost){
        if (currentMana - manaCost >= 0){
            currentMana -= manaCost;
            return true;
        } else{
            return false;
        }
    }

}
