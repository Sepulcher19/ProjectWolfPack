using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, ALPHATURN, BETATURN, ELDERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject AlphaPrefab;
    public GameObject BetaPrefab;
    public GameObject ElderPrefab;
    public GameObject enemyPrefab;

    
    public Transform AlphaBattlePosition;
    public Transform BetaBattlePosition;
    public Transform ElderBattlePosition;
    public Transform enemyBattlePosition;

    public BattleState state;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    Unit alphaUnit;
    Unit betaUnit;
    Unit elderUnit;
    Unit enemyUnit;

    public Text dialogueText;


    bool alphaAlive = true;
    bool betaAlive = true;
    int betaCount;
    bool elderAlive = true;

    int hitSuccess = 80;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }

    IEnumerator SetupBattle()
    {
        SpawnPlayers();
       

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattlePosition);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.Name + " approaches";

        playerHUD.SetHUD(alphaUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.ALPHATURN;
        PlayerTurn();
    }
    IEnumerator AlphaAttack()
    {
        if (CheckHit() == true)
        {

            bool isDead = enemyUnit.TakeDamage(alphaUnit.attackDamage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "The attack is successful";

            yield return new WaitForSeconds(2f);


            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.BETATURN;
                BetaTurn();
            }
        }
        else
        {
            dialogueText.text = "The attack missed...";

            yield return new WaitForSeconds(2f);

            state = BattleState.BETATURN;
            BetaTurn();
        }


    }
    IEnumerator BetaAttack()
    {
        if (CheckHit() == true)
        {

            bool isDead = enemyUnit.TakeDamage(betaUnit.attackDamage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "The attack is successful";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ELDERTURN;
                ElderTurn();
            }
        }
        else
        {
            dialogueText.text = "The attack missed...";

            yield return new WaitForSeconds(2f);

            state = BattleState.ELDERTURN;
            ElderTurn();
        }


    }
    IEnumerator ElderAttack()
    {
        if (CheckHit() == true)
        {

            bool isDead = enemyUnit.TakeDamage(elderUnit.attackDamage);
            dialogueText.text = "Elder attack successful";

            enemyHUD.SetHP(enemyUnit.currentHP);

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                print("isdead");
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;

                StartCoroutine(EnemyTurn());
            }
        }
        else// if it misses
        {
            dialogueText.text = "The attack missed...";
            yield return new WaitForSeconds(2f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }


    }
    IEnumerator EnemyTurn()
    {
        print("test");
        dialogueText.text = enemyUnit.Name + " attacks!";

        yield return new WaitForSeconds(1f);
        if (CheckHit() == true)
        {
            

            bool isDead = alphaUnit.TakeDamage(enemyUnit.attackDamage);

            playerHUD.SetHP(alphaUnit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.ALPHATURN;
                PlayerTurn();
            }
        }
        else
        {
            dialogueText.text = "The attack missed...";
            state = BattleState.ALPHATURN;
            PlayerTurn();
        }

    }


    public void SpawnPlayers()
    {
        if (alphaAlive)
        {
            GameObject playerGO = Instantiate(AlphaPrefab, AlphaBattlePosition);
            alphaUnit = playerGO.GetComponent<Unit>();
        }
        if (betaAlive)
        {
            GameObject playerGO = Instantiate(BetaPrefab, BetaBattlePosition);
            betaUnit = playerGO.GetComponent<Unit>();
        }
        if (elderAlive)
        {
            GameObject playerGO = Instantiate(ElderPrefab, ElderBattlePosition);
            elderUnit = playerGO.GetComponent<Unit>();
        }
        else { dialogueText.text = "youre out of wolves, you suck!"; }

    }
    public void AttackButton()
    {
        if (state == BattleState.ALPHATURN)
        {
            StartCoroutine(AlphaAttack());
        }
        if (state == BattleState.BETATURN)
        {
            StartCoroutine(BetaAttack());
        }
        if (state == BattleState.ELDERTURN)
        {
            StartCoroutine(ElderAttack());

        }

    }
   

    void PlayerTurn()
    {
        dialogueText.text = "Alpha ready to attack!";
    }
    void BetaTurn()
    {
        dialogueText.text = "Beta ready to attack!";
    }
    void ElderTurn()
    {
        dialogueText.text = "Elder ready to attack!";
    }
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";

        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "you were Defeated.";
        }

    }


    public bool CheckHit()
    {
        
        int hitChance = Random.Range(1, 100);
        print(hitChance);
        if  (hitChance <= hitSuccess) // if hit chance is lower than hit success, player hits 
        {
           
            return true;
        }
        else
        {
            
            return false;
        }

    }




}
