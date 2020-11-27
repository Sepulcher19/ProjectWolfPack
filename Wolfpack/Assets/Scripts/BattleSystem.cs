using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, ALPHATURN, BETATURN, ELDERTURN, ENEMYTURN, ENEMYTURN2, ENEMYTURN3, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    [Header("Characters")]
    public GameObject AlphaPrefab;
    public GameObject BetaPrefab;
    public GameObject ElderPrefab;
    public GameObject enemyPrefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    [Header("Targets")]
    public GameObject targets;

    [Header("Battle Positions")]
    public Transform AlphaBattlePosition;
    public Transform BetaBattlePosition;
    public Transform ElderBattlePosition;
    public Transform enemyBattlePosition;
    public Transform enemy2BattlePosition;
    public Transform enemy3BattlePosition;

    public BattleState state;

    [Header("Player HUD")]
    public BattleHUD alphaHUD;
    public BattleHUD betaHUD;
    public BattleHUD elderHUD;
    [Header("enemy HUD")]
    public BattleHUD enemyAlphaHUD;
    public BattleHUD enemyBetaHUD;
    public BattleHUD enemyElderHUD;
    [Header("Particles")]
    public ParticleSystem damageParticle;

    Unit alphaUnit;
    Unit betaUnit;
    Unit elderUnit;
    Unit enemyAlphaUnit;
    Unit enemyBetaUnit;
    Unit enemyElderUnit;

    public Text dialogueText;


    bool alphaAlive = true;
    bool betaAlive = true;
    int betaCount;
    bool elderAlive = true;
    bool secondaryAttack;
    bool damageIncreased;

    int hitSuccess = 80;
    int critSuccess = 95;

    int attackDamage;

    public List<Unit> playerWolves = new List<Unit>();
    public List<Unit> enemyWolves = new List<Unit>();


    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());


        alphaHUD.transform.parent = alphaUnit.transform;
        betaHUD.transform.parent = betaUnit.transform;
        elderHUD.transform.parent = elderUnit.transform;

        enemyAlphaHUD.transform.parent = enemyAlphaUnit.transform;
        enemyBetaHUD.transform.parent = enemyBetaUnit.transform;
        enemyElderHUD.transform.parent = enemyElderUnit.transform;

        targets.SetActive(false);

    }



    IEnumerator SetupBattle()
    {
        SpawnPlayers();
        SpawnEnemy();
        SetupHUD();

        dialogueText.text = "A wild " + enemyAlphaUnit.Name + " approaches";

        yield return new WaitForSeconds(2f);

        state = BattleState.ALPHATURN;
        AlphaTurn();
    }







    public bool CheckWin()
    {
        if (playerWolves.Count == 0)
        {

            state = BattleState.LOST;
            EndBattle();
            return true;
        }
        else if (enemyWolves.Count == 0)
        {
            state = BattleState.WON;
            EndBattle();
            return true;
        }
        else
        {
            return false;
        }
    }


    IEnumerator AlphaAttack(Unit confirmedTarget)
    {
        CheckWin();
        if (!alphaUnit.isDead)
        {
            if (CheckHit() == true)
            {


                Unit target = confirmedTarget;
                target.TakeDamage(alphaUnit.attackDamage);
                DamageParticle(target);



                print(target.gameObject.name + " hit");
                target.gameObject.GetComponentInChildren<BattleHUD>().SetHP(target.currentHP);
                dialogueText.text = "The attack is successful";

                yield return new WaitForSeconds(2f);


                if (target.isDead)
                {
                    target.gameObject.SetActive(false);
                    enemyWolves.Remove(target);
                    if (!CheckWin())
                    {
                        state = BattleState.BETATURN;
                        BetaTurn();
                    }

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
        else
        {
            state = BattleState.BETATURN;
            BetaTurn();
        }

    }

    IEnumerator SummonBeta()
    {

        if (betaUnit.level < 10)
        {
            var newBetas = Random.Range(1, 3);
            dialogueText.text = newBetas + " more wolves join your struggle";
            betaUnit.SetLevel(newBetas);



            yield return new WaitForSeconds(1f);
            state = BattleState.BETATURN;
            BetaTurn();
        }
        else
        {
            dialogueText.text = "Large packs are uncontrollable, try something else";
            yield return new WaitForSeconds(1f);
            AlphaTurn();

        }
    }


    IEnumerator BetaAttack(Unit confirmedTarget)
    {
        CheckWin();
        if (!betaUnit.isDead)
        {
            if (CheckHit() == true)
            {
                if (secondaryAttack)
                {
                    attackDamage = betaUnit.specialDamage;
                }
                else
                {
                    attackDamage = betaUnit.attackDamage;
                }
                Unit target = confirmedTarget;
                target.TakeDamage(attackDamage);
                DamageParticle(target);
                target.gameObject.GetComponentInChildren<BattleHUD>().SetHP(target.currentHP);
                dialogueText.text = "The attack is successful";

                yield return new WaitForSeconds(2f);

                if (target.isDead)
                {
                    target.gameObject.SetActive(false);
                    enemyWolves.Remove(target);
                    if (!CheckWin())
                    {
                        state = BattleState.ELDERTURN;
                        ElderTurn();
                    }
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
        else
        {
            state = BattleState.ELDERTURN;
            ElderTurn();
        }
    }
    IEnumerator ElderAttack(Unit confirmedTarget)
    {
        CheckWin();
        if (!elderUnit.isDead)
        {


            if (CheckHit() == true)
            {

                attackDamage = elderUnit.attackDamage;

                Unit target = confirmedTarget;
                target.TakeDamage(attackDamage);
                DamageParticle(target);

                dialogueText.text = "Elder attack successful";

                target.gameObject.GetComponentInChildren<BattleHUD>().SetHP(target.currentHP);


                yield return new WaitForSeconds(2f);

                if (target.isDead)
                {
                    enemyWolves.Remove(target);
                    target.gameObject.SetActive(false);
                    enemyWolves.Remove(target);
                    if (!CheckWin())
                    {
                        state = BattleState.ENEMYTURN;

                        StartCoroutine(EnemyTurn());
                    }
                    else
                    {

                        state = BattleState.ENEMYTURN;

                        StartCoroutine(EnemyTurn());
                    }
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
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator ElderBuff()
    {
        if (damageIncreased == false)
        {

            alphaUnit.attackDamage = alphaUnit.attackDamage * 2;
            betaUnit.attackDamage = betaUnit.attackDamage * 2;
            elderUnit.attackDamage = elderUnit.attackDamage * 2;
            damageIncreased = true;

            dialogueText.text = "your team was buffed!";

            yield return new WaitForSeconds(1f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            dialogueText.text = "you cant do that right now";
            yield return new WaitForSeconds(1f);


        }
    }

    IEnumerator EnemyTurn()
    {
        CheckWin();
        if (!enemyAlphaUnit.isDead)
        {
            dialogueText.text = enemyAlphaUnit.Name + " attacks!";

            yield return new WaitForSeconds(1f);
            if (CheckHit() == true)
            {

                Unit target = EnemyChooseTarget();
                target.TakeDamage(enemyAlphaUnit.attackDamage);
                DamageParticle(target);
                target.gameObject.GetComponentInChildren<BattleHUD>().SetHP(target.currentHP);

                yield return new WaitForSeconds(1f);

                if (target.isDead)
                {
                   
                    target.gameObject.SetActive(false);
                    playerWolves.Remove(target);
                    if (!CheckWin())
                    {
                        state = BattleState.ENEMYTURN2;
                        StartCoroutine(EnemyBetaTurn());
                    }
                }
                else
                {
                    state = BattleState.ENEMYTURN2;
                    StartCoroutine(EnemyBetaTurn());
                }
            }
            else
            {
                dialogueText.text = "The attack missed...";
                state = BattleState.ENEMYTURN2;
                StartCoroutine(EnemyBetaTurn());
            }
        }
        else
        {
            state = BattleState.ENEMYTURN2;
            StartCoroutine(EnemyBetaTurn());
        }
    }


    IEnumerator EnemyBetaTurn()
    {
        CheckWin();
        if (!enemyBetaUnit.isDead)
        {


            dialogueText.text = enemyBetaUnit.Name + " attacks!";

            yield return new WaitForSeconds(1f);
            if (CheckHit() == true)
            {
                Unit target = EnemyChooseTarget();

                target.TakeDamage(enemyBetaUnit.attackDamage);
                DamageParticle(target);
                target.gameObject.GetComponentInChildren<BattleHUD>().SetHP(target.currentHP);

                yield return new WaitForSeconds(1f);

                if (target.isDead)
                {
                    
                    target.gameObject.SetActive(false);
                    playerWolves.Remove(target);
                    if (!CheckWin())
                    {
                        state = BattleState.ENEMYTURN2;
                        StartCoroutine(EnemyBetaTurn());
                    }
                }
                else
                {
                    state = BattleState.ENEMYTURN3;
                    StartCoroutine(EnemyElderTurn());
                }
            }
            else
            {
                dialogueText.text = "The attack missed...";
                state = BattleState.ENEMYTURN3;
                StartCoroutine(EnemyElderTurn());
            }
        }
        else
        {
            state = BattleState.ENEMYTURN3;
            StartCoroutine(EnemyElderTurn());
        }
    }
    IEnumerator EnemyElderTurn()
    {
        CheckWin();

        if (!enemyElderUnit.isDead)
        {

            dialogueText.text = enemyElderUnit.Name + " attacks!";

            yield return new WaitForSeconds(1f);
            if (CheckHit() == true)
            {
                Unit target = EnemyChooseTarget();

                target.TakeDamage(enemyElderUnit.attackDamage);
                DamageParticle(target);
                target.gameObject.GetComponentInChildren<BattleHUD>().SetHP(target.currentHP);

                yield return new WaitForSeconds(1f);

                if (target.isDead)
                {
                    target.gameObject.SetActive(false);
                    playerWolves.Remove(target);
                    if (!CheckWin())
                    {
                        state = BattleState.ALPHATURN;
                        AlphaTurn();
                    }
                }
                else
                {
                    state = BattleState.ALPHATURN;
                    AlphaTurn();
                }
            }
            else
            {
                dialogueText.text = "The attack missed...";
                state = BattleState.ALPHATURN;
                AlphaTurn();
            } 
        }
        else
        {
            state = BattleState.ALPHATURN;
            AlphaTurn();
        }
    }



    public Unit EnemyChooseTarget()
    {
        int wolfIndex = Random.Range(0, 3);
        return playerWolves[wolfIndex];
    }


    public void SpawnPlayers()
    {
        if (alphaAlive)
        {
            GameObject playerGO = Instantiate(AlphaPrefab, AlphaBattlePosition);
            alphaUnit = playerGO.GetComponent<Unit>();
            playerWolves.Add(alphaUnit);
        }
        if (betaAlive)
        {
            GameObject playerGO = Instantiate(BetaPrefab, BetaBattlePosition);
            betaUnit = playerGO.GetComponent<Unit>();
            playerWolves.Add(betaUnit);
        }
        if (elderAlive)
        {
            GameObject playerGO = Instantiate(ElderPrefab, ElderBattlePosition);
            elderUnit = playerGO.GetComponent<Unit>();
            playerWolves.Add(elderUnit);
        }
        else { dialogueText.text = "youre out of wolves, you suck!"; }

    }
    public void SpawnEnemy()
    {
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattlePosition);
        enemyAlphaUnit = enemyGO.GetComponent<Unit>();
        enemyWolves.Add(enemyAlphaUnit);

        GameObject enemy2GO = Instantiate(enemy2Prefab, enemy2BattlePosition);
        enemyBetaUnit = enemy2GO.GetComponent<Unit>();
        enemyWolves.Add(enemyBetaUnit);

        GameObject enemy3GO = Instantiate(enemy3Prefab, enemy3BattlePosition);
        enemyElderUnit = enemy3GO.GetComponent<Unit>();
        enemyWolves.Add(enemyElderUnit);
    }
    public void AttackButton()
    {
        secondaryAttack = false;
        targets.SetActive(true);
        dialogueText.text = "Choose a target";
    }

    public void SecondaryAttackButton()
    {
        if (state == BattleState.ELDERTURN)
        {
            secondaryAttack = true;
            StartCoroutine(ElderBuff());

        }
        else if (state == BattleState.ALPHATURN)
        {
            secondaryAttack = true;
            StartCoroutine(SummonBeta());
        }
        else
        {
            secondaryAttack = true;
            targets.SetActive(true);
            dialogueText.text = "Choose a target";
        }
    }

    public void ChooseTarget(string targetName)
    {
        Unit confirmedTarget = null;
        switch (targetName)
        {
            case "Alpha":
                confirmedTarget = enemyAlphaUnit;
                break;
            case "Beta":
                confirmedTarget = enemyBetaUnit;
                break;
            case "Elder":
                confirmedTarget = enemyElderUnit;
                break;

        }

        if (state == BattleState.ALPHATURN)
        {
            targets.SetActive(false);
            StartCoroutine(AlphaAttack(confirmedTarget));

        }
        if (state == BattleState.BETATURN)
        {
            targets.SetActive(false);
            StartCoroutine(BetaAttack(confirmedTarget));
        }
        if (state == BattleState.ELDERTURN)
        {
            targets.SetActive(false);
            StartCoroutine(ElderAttack(confirmedTarget));

        }
    }

    void AlphaTurn()
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

        if (hitChance <= hitSuccess) // if hit chance is lower than hit success, player hits 
        {

            return true;
        }
        else
        {

            return false;
        }

    }




    public void SetupHUD()
    {
        alphaHUD.SetHUD(alphaUnit);
        betaHUD.SetHUD(betaUnit);
        elderHUD.SetHUD(elderUnit);


        enemyAlphaHUD.SetHUD(enemyAlphaUnit);
        enemyBetaHUD.SetHUD(enemyBetaUnit);
        enemyElderHUD.SetHUD(enemyElderUnit);

    }

    public void DamageParticle(Unit target)
    {
        var particleObject = Instantiate(damageParticle, target.transform.position, Quaternion.identity);
        // Destroy(particleObject, particleObject.time);
    }


}
