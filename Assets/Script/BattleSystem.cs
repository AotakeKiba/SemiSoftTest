using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState{ START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    Player playerUnit;
    Enemy enemyUnit;
    
    public battleHUDPlayer playerHUD;
    public battleHUDEnemy enemyHUD;

    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StopCoroutine(SetupBattle());
        StartCoroutine(SetupBattle());
    }

    // start the first phase of the battle
    IEnumerator SetupBattle(){
        GameObject playerGO = Instantiate(playerPrefab);
        playerUnit = GameObject.Find("Player").GetComponent<Player>();

        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = GameObject.Find("Enemy").GetComponent<Enemy>();

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
                EndBattle();
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
                EndBattle();
            }else{
                state = BattleState.PLAYERTURN;
            }
        } else{
            dialogueText.text = "Enemy Recharge";
            yield return new WaitForSeconds(1f);
            // recharge mana
            enemyHUD.SetMana(enemyUnit.currentMana += 25);
        }
        //recharge mana
        playerHUD.SetMana(playerUnit.currentMana += 5);
        PlayerTurn();
    }

    void EndBattle(){
        if (state == BattleState.WON){
            dialogueText.text = "You Won";
        }else if (state == BattleState.LOST){
            dialogueText.text = "You Lost";
        }
    }

    public void OnAttack1Button(){
        if (state != BattleState.PLAYERTURN){
            return;
        }
        dialogueText.text = "You Attack 1";
        StopCoroutine(PlayerAttack(5,5));
        StartCoroutine(PlayerAttack(5,5));
    }
    public void OnAttack2Button(){
        if (state != BattleState.PLAYERTURN){
            return;
        }
        dialogueText.text = "You Attack 2";
        StopCoroutine(PlayerAttack(10,10));
        StartCoroutine(PlayerAttack(10,10));
    }
    public void OnAttack3Button(){
        if (state != BattleState.PLAYERTURN){
            return;
        }
        dialogueText.text = "You Attack 3";
        StopCoroutine(PlayerAttack(30,30));
        StartCoroutine(PlayerAttack(30,30));
    }
    public void OnAttack4Button(){
        if (state != BattleState.PLAYERTURN){
            return;
        }
        dialogueText.text = "You Attack 4";
        StopCoroutine(PlayerAttack(50,50));
        StartCoroutine(PlayerAttack(50,50));
    }

}
