using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState{ START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    //public GameObject playerPrefab;
    public GameObject enemyPrefab;

    Player playerUnit;
    Enemy enemyUnit;
    
    public battleHUDPlayer playerHUD;
    public battleHUDEnemy enemyHUD;

    public Text dialogueText;

    public Text atk1;
    public Text atk2;
    public Text atk3;
    public Text atk4;

    List<Skills> skill = new List<Skills>();
    int bonusDmg = 0;
    int stunStattus = 0;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StopCoroutine(SetupBattle());
        StartCoroutine(SetupBattle());
    }

    // start the first phase of the battle
    IEnumerator SetupBattle(){
        GameObject playerGO = Instantiate(GameObject.Find("Player"));
        playerUnit = GameObject.Find("Player").GetComponent<Player>();

        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = GameObject.Find("Enemy").GetComponent<Enemy>();

        skill = playerUnit.GetSkill();
        atk1.text = skill[0].skillName;
        atk2.text = skill[1].skillName;
        atk3.text = skill[2].skillName;
        atk4.text = skill[3].skillName;

        dialogueText.text = enemyUnit.unitName + " Appear";

        playerHUD.setHUD(playerUnit);
        enemyHUD.setHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn(){
        dialogueText.text = "Choose an action";
    }

    IEnumerator PlayerAttack(int bonusDmg, int manaCost){
        // Attack the enemy, check first wheter the player has enough mana
        // if yes, attack go through. if not, enemy's turn.
        bool isAtk = playerUnit.ManaLost(manaCost);
        if (isAtk){
            bool isDead = enemyUnit.TakeDamage(playerUnit.atk + bonusDmg);
            enemyHUD.SetHp(enemyUnit.currentHp);
            playerHUD.SetMana(playerUnit.currentMana);

            yield return new WaitForSeconds(2f);

            if (isDead){
                state = BattleState.WON;
                StopCoroutine(EndBattle());
                StartCoroutine(EndBattle());
            }else{
                state = BattleState.ENEMYTURN;
                StopCoroutine(EnemyTurn());
                StartCoroutine(EnemyTurn());
            }
        }else{
            dialogueText.text = "No Mana";
            yield return new WaitForSeconds(1f);
            StopCoroutine(EnemyTurn());
            StartCoroutine(EnemyTurn());
        }
        
    }

    IEnumerator EnemyTurn(){
        // enemy attacks player, if the enemy doesn't have enough mana, he will recharge his mana and end his turn.
        // at the end of enemy's turn, recharge both player's and enemy's mana
        if (stunStattus == 1){
            dialogueText.text = "Enemy is stun";
            state = BattleState.PLAYERTURN;
            yield return new WaitForSeconds(1f);
        }
        else
        {
            dialogueText.text = "Enemy Attack";
            yield return new WaitForSeconds(2f);
            bool isAtk = enemyUnit.ManaLost(10);
            if (isAtk){
                bool isDead = playerUnit.TakeDamage(enemyUnit.atk);
                playerHUD.SetHp(playerUnit.currentHp);
                enemyHUD.SetMana(enemyUnit.currentMana);

                yield return new WaitForSeconds(1f);

                if (isDead){
                    state = BattleState.LOST;
                    StopCoroutine(EndBattle());
                    StartCoroutine(EndBattle());
                }else{
                    state = BattleState.PLAYERTURN;
                }
            } else{
                dialogueText.text = "Enemy Recharge";
                yield return new WaitForSeconds(1f);
                // recharge mana
                enemyHUD.SetMana(enemyUnit.currentMana += 25);
                state = BattleState.PLAYERTURN;
            }
        }
        //recharge mana
        playerHUD.SetMana(playerUnit.currentMana += 5);
        bonusDmg = 0;
        stunStattus = 0;
        PlayerTurn();
    }

    IEnumerator EndBattle(){
        if (state == BattleState.WON){
            dialogueText.text = "You Won";
        }else if (state == BattleState.LOST){
            dialogueText.text = "You Lost";
        }
        yield return new  WaitForSeconds(5f);
        Application.Quit();
    }

    public void setBonusStatus(string effect){
        if (effect.Contains("Atk+")){
            bonusDmg = 10;
        }else if (effect.Contains("Atk++")){
            bonusDmg = 20;
        }
        else if (effect.Contains("Atk+++")){
            bonusDmg = 30;
        }else if (effect.Contains("Stun")){
            bonusDmg = 0;
            stunStattus = 1;
        }
    }

    public void OnAttack1Button(){
        if (state != BattleState.PLAYERTURN){
            return;
        }
        setBonusStatus(skill[0].bonusEffect);
        dialogueText.text = "You Attack with " + skill[0].skillName;
        StopCoroutine(PlayerAttack(skill[0].skillDmg + bonusDmg, skill[0].skillManaCost));
        StartCoroutine(PlayerAttack(skill[0].skillDmg + bonusDmg, skill[0].skillManaCost));
    }
    public void OnAttack2Button(){
        if (state != BattleState.PLAYERTURN){
            return;
        }
        setBonusStatus(skill[1].bonusEffect);
        dialogueText.text = "You Attack with " + skill[1].skillName;
        StopCoroutine(PlayerAttack(skill[1].skillDmg + bonusDmg,skill[1].skillManaCost));
        StartCoroutine(PlayerAttack(skill[1].skillDmg + bonusDmg,skill[1].skillManaCost));
    }
    public void OnAttack3Button(){
        if (state != BattleState.PLAYERTURN){
            return;
        }
        setBonusStatus(skill[2].bonusEffect);
        dialogueText.text = "You Attack with " + skill[2].skillName;
        StopCoroutine(PlayerAttack(skill[2].skillDmg + bonusDmg,skill[2].skillManaCost));
        StartCoroutine(PlayerAttack(skill[2].skillDmg + bonusDmg,skill[2].skillManaCost));
    }
    public void OnAttack4Button(){
        if (state != BattleState.PLAYERTURN){
            return;
        }
        setBonusStatus(skill[3].bonusEffect);
        dialogueText.text = "You Attack with " + skill[3].skillName;
        StopCoroutine(PlayerAttack(skill[3].skillDmg + bonusDmg,skill[3].skillManaCost));
        StartCoroutine(PlayerAttack(skill[3].skillDmg + bonusDmg,skill[3].skillManaCost));
    }

}
