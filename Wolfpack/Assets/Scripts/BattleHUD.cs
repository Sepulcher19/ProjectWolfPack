﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;

    public Slider hpSlider;
    public Text hpHealth;

    public GameObject woundTear;

    // Start is called before the first frame update
    void Start()
    {
        woundTear.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.Name;
        levelText.text = "Lv" + unit.level;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;

        hpHealth.text = hpSlider.value + "/" + hpSlider.maxValue;
    }
    public void SetHP(int hp,int hpMax)
    {
        hpSlider.maxValue = hpMax;
        hpSlider.value = hp;
        hpHealth.text = hpSlider.value + "/" + hpSlider.maxValue;
    }

    public void SetLevel(int level)
    {
        levelText.text = "Lv" + level;
       
    }

    public void ShowWound()
    {
        woundTear.SetActive(true);
    }

    public void HideWound()
    {
        woundTear.SetActive(false);
    }



}
