using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Overview")]
    public string Name;
    
    public int level;
    [Header("Health")]
    int baseMaxHP = 40;
    public int maxHP;
    public int currentHP;

    [Header("Combat")]
    public int attackDamage;
    public int specialDamage;
    public int specialCooldown;

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
        attackDamage *= level;
        specialDamage *= level;

        maxHP *= level;
        currentHP = currentHP + (baseMaxHP * addedLevel);
        baseMaxHP = 40;


        GetComponentInChildren<BattleHUD>().SetLevel(level);
        GetComponentInChildren<BattleHUD>().SetHP(currentHP,maxHP);
    }

    


}
