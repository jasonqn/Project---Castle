using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//sets the state
public enum State{ START, PLAYERTURN, ENEMYTURN, WON, LOST , RAN }

public class BattleManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public Transform playerBattleStart;
    public Transform enemyBattleStart;

    Stats playerStats;
    Stats enemyStats;

    public BattleUI playerHUD;
    public BattleUI enemyHUD;

    Player playerAnimations;
    EnemyAnimation enemyAnimations;

    public GameObject buttonPanel;
    public TMP_Text dialogueText;

    public State state;

    GameObject ddol;
    DDOL ddolScript;

    DDOLstats savedStats;
    public SceneTransition ST;

    GameObject selectedEnemy;
    public int selectedEnemyNumber;
    public GameObject backgroundList;
    GameObject selectedBackground;
    public int selectedBackgroundNumber;

    public void Awake()
    {
        ddol = GameObject.FindGameObjectWithTag("DDOL");
        ddolScript = ddol.GetComponent<DDOL>();
        ST = ddol.GetComponentInChildren<SceneTransition>();
        savedStats = ddol.GetComponentInChildren<DDOLstats>();
        buttonPanel.SetActive(false);

        selectedEnemyNumber = ddolScript.selectedEnemyNumber;
        selectedBackgroundNumber = ddolScript.selectedBackgroundNumber;

        selectedBackground = backgroundList.transform.GetChild(selectedBackgroundNumber).gameObject;
        selectedBackground.SetActive(true);

        state = State.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(player, playerBattleStart);

        playerStats = playerGO.GetComponent<Stats>();
        playerStats.LoadStats();
        playerAnimations = playerGO.GetComponent<Player>();

        selectedEnemy = enemy.transform.GetChild(selectedEnemyNumber).gameObject;
        GameObject enemyGO = Instantiate(selectedEnemy, enemyBattleStart);
        enemyGO.SetActive(true);
        enemyStats = enemyGO.GetComponent<Stats>();
        enemyAnimations = enemyGO.GetComponent<EnemyAnimation>();

        dialogueText.text = "Ravenous " + enemyStats.unitName + " awaits...";

        playerHUD.SetHUD(playerStats);
        enemyHUD.SetHUD(enemyStats);


        yield return new WaitForSeconds(2f);

        state = State.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action";
        buttonPanel.SetActive(true);
    }

    public void OnAttackButton()
    {
        if(state != State.PLAYERTURN)
        {
            return;
        }
        else
        {
            dialogueText.text = "You Attack!";
            buttonPanel.SetActive(false);
            StartCoroutine(PlayerAttack());
        }
    }

    public void OnHealButton()
    {
        if (state != State.PLAYERTURN)
        {
            return;
        }
        else
        {
            dialogueText.text = "You Heal!";
            buttonPanel.SetActive(false);
            StartCoroutine(PlayerHeal());
        }
    }

    public void OnRunButton()
    {
        if (state != State.PLAYERTURN)
        {
            return;
        }
        else
        {
            dialogueText.text = "You Run Away Safely!";
            buttonPanel.SetActive(false);
            StartCoroutine(PlayerRun());
        }
    }

    IEnumerator PlayerAttack()
    {

        bool isDead = enemyStats.TakeDamage(playerStats.damage);
        enemyHUD.SetHP(enemyStats.currentHP);
        playerAnimations.Attack();

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = State.WON;
            StartCoroutine(Won());

        }
        else
        {
            state = State.ENEMYTURN;
            EnemyTurn();
        }
    }
    
    IEnumerator PlayerHeal()
    {

        playerStats.currentHP += 10;

        if(playerStats.currentHP > playerStats.maxHP)
        {
            playerStats.currentHP = playerStats.maxHP;
        }

        playerAnimations.Block();

        yield return new WaitForSeconds(2f);

        playerHUD.SetHP(playerStats.currentHP);

        state = State.ENEMYTURN;
        EnemyTurn();
        
    }    
    IEnumerator PlayerRun()
    {
        yield return new WaitForSeconds(2f);

        playerHUD.SetHP(playerStats.currentHP);

        state = State.RAN;
        Ran();
        
    }

    void EnemyTurn()
    {
        dialogueText.text = "The " + enemyStats.unitName + " takes its turn";
        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(2f);
        dialogueText.text = "The Enemy Attacks!";
        enemyAnimations.EnemyAttack();
        yield return new WaitForSeconds(1f);
        bool isDead = playerStats.TakeDamage(enemyStats.damage);
        playerHUD.SetHP(playerStats.currentHP);
        yield return new WaitForSeconds(1f);
        if(isDead)
        {
            state = State.LOST;
            StartCoroutine(Lose());
        }
        else
        {
            state = State.PLAYERTURN;
            PlayerTurn();
        }
    }

    void Ran()
    {
        savedStats.LoadStats();
        ddolScript.InCombat();
        StartCoroutine(ST.PreviousScene());
    }

    IEnumerator Won()
    {
        dialogueText.text = "You Win!";
        enemyAnimations.EnemyDie();
        yield return new WaitForSeconds(2f);
        savedStats.LoadStats();
        ddolScript.InCombat();
        StartCoroutine(ST.PreviousScene());
    }

    IEnumerator Lose()
    {
        dialogueText.text = "You Lose!";
        playerAnimations.Die();
        yield return new WaitForSeconds(2f);
        StartCoroutine(ST.PreviousScene());
    }

    


    
}
