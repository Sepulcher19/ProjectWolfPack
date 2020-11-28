using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Overview")]
    public string Name;
    
    public int level;
    [Header("Health")]
    int baseMaxHP = 200;
    public int maxHP;
    public int currentHP;

    [Header("Combat")]
    public int attackDamage;
    public int specialDamage;
    public int specialCooldown;

    int betaBaseAttack = 6;
    int betaSpecialAttack = 10;
    public bool isDead = false;
    public bool wounded = false;
    public int woundCounter = 3;


    [Header("Other")]
    public GameObject damageTextPrefab;

    private void Awake()
    {
        
        currentHP = maxHP;
        level = 1;
    }

    private void Update()
    {
        if (CheckDead())
        {
            level = 0;
            isDead = true;
            gameObject.SetActive(false);
        }

        if (level >= 1)
        {
            gameObject.SetActive(true);
            isDead = false;
        }
    }

    public bool CheckDead()
    {
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        GameObject damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity) as GameObject;
        damageText.GetComponentInChildren<TextMesh>().text = dmg.ToString();
        if (currentHP <= 0)
            isDead = true;
        else
            isDead = false;
    }
    
  
    public void SetLevel(int addedLevel)
    {
        level += addedLevel;
        attackDamage = (betaBaseAttack * level);
        specialDamage = (betaSpecialAttack * level);

        maxHP = (baseMaxHP * level);
        
        currentHP = currentHP + (baseMaxHP * addedLevel);
        baseMaxHP = 40;


        GetComponentInChildren<BattleHUD>().SetLevel(level);
        GetComponentInChildren<BattleHUD>().SetHP(currentHP,maxHP);
    }

    
   


}
