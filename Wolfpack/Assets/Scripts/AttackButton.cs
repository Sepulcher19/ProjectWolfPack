using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public Text buttonText;
    public int playerturn;

    public GameObject AlphaAbility;
    public GameObject betaAbility;
    public GameObject elderAbility;

    private void Awake()
    {
        AlphaAbility.SetActive(false);
        betaAbility.SetActive(false);
        elderAbility.SetActive(false);
    }
    public void UpdateText(string abilityText)
    {
        buttonText.text = abilityText;
    }

    public void OnMouseOver()
    {
        print("mouse over");
        switch (playerturn)
        {
            case 1:
                AlphaAbility.SetActive(true);
                betaAbility.SetActive(false);
                elderAbility.SetActive(false);
                break;
            case 2:
                AlphaAbility.SetActive(false); 
                betaAbility.SetActive(true);
                elderAbility.SetActive(false);
                break;
            case 3:
                AlphaAbility.SetActive(false);
                betaAbility.SetActive(false);
                elderAbility.SetActive(true);
                break;
        }
    }

    public void OnMouseExit()
    {
        AlphaAbility.SetActive(false);
        betaAbility.SetActive(false);
        elderAbility.SetActive(false);
    }




}
