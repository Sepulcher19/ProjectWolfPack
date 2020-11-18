using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;

    public Slider hpSlider;

    
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

}
