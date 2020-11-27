using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Overview")]
    public string Name;
    
    public int level;
    [Header("Health")]
    public int maxHP;
    public int currentHP;
    [Header("Combat")]
    public int attackDamage;
    public int specialDamage;
    public int specialCooldown;

    public bool isDead = false;

    private void Awake()
    {
        currentHP = maxHP;
        level = 1;
    }



    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            isDead = true;
        else
            isDead = false;
    }

  
    public void SetLevel(int addedLevel)
    {
        level += addedLevel;
        GetComponentInChildren<BattleHUD>().SetLevel(level);

        attackDamage *= level;
        specialDamage *= level;
    }

}
