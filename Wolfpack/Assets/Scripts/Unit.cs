using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Overview")]
    public string Name;
    public int count;
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
    }



    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            isDead = true;
        else
            isDead = false;
    }

    private void Update()
    {
        attackDamage = attackDamage * count;
        specialDamage = attackDamage * count;
    }

}
