using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [Header("Work")]
    [Tooltip("Results per hour")]
    [SerializeField] private ResultsForm workLevel1;
    [SerializeField] private ResultsForm workLevel2;
    [SerializeField] private ResultsForm workLevel3;
    [SerializeField] private ResultsForm workLevel4;

    [Header("Training")]
    [Tooltip("Results per hour")]
    [SerializeField] private ResultsForm trainLevel1;
    [SerializeField] private ResultsForm trainLevel2;
    [SerializeField] private ResultsForm trainLevel3;
    [SerializeField] private ResultsForm trainLevel4;
    
    [Header("Food")]
    [Tooltip("Results per hour")]
    [SerializeField] private ResultsForm foodBad;
    [SerializeField] private ResultsForm foodStandard;
    [SerializeField] private ResultsForm foodGood;
    [SerializeField] private ResultsForm foodExcellent;
    
    [Header("Drinks")]
    [Tooltip("Results per hour")]
    [SerializeField] private ResultsForm drinksBad;
    [SerializeField] private ResultsForm drinksStandard;
    [SerializeField] private ResultsForm drinksGood;
    [SerializeField] private ResultsForm drinksExcellent;
    
    [SerializeField] private bool debuff = false;
    
    [SerializeField] private float debuffMultiplier = 0.5f;
    [SerializeField] private float debuffMultiplierAmount = 1f;

    public UIManager uiManager;
    public GameManager gameManager;
    public PlayerData playerData;

    private void Start()
    {
        if (debuff)
            ApplyDebuffs(debuffMultiplier, debuffMultiplierAmount);
    }

    public void SleepResults(float duration)
    {
    }

    public void WorkResults()
    {
        switch (gameManager.WorkLevel)
        {
            case 0:
                gameManager.Money += workLevel1.Money * (debuffMultiplier * debuffMultiplierAmount);
                gameManager.Tiredness += workLevel1.Tiredness;
                gameManager.WorkExperience += workLevel1.WorkExperience;
                break;

            case 1:
                gameManager.Money += workLevel2.Money * (debuffMultiplier * debuffMultiplierAmount);
                gameManager.Tiredness += workLevel2.Tiredness;
                gameManager.WorkExperience += workLevel2.WorkExperience;
                gameManager.GameKnowledge += workLevel2.GameKnowledge * (debuffMultiplier * debuffMultiplierAmount);

                break;

            case 2:
                gameManager.Money += workLevel3.Money * (debuffMultiplier * debuffMultiplierAmount);
                gameManager.Tiredness += workLevel3.Tiredness;
                gameManager.WorkExperience += workLevel3.WorkExperience;
                gameManager.GameKnowledge += workLevel3.GameKnowledge * (debuffMultiplier * debuffMultiplierAmount);
                gameManager.GameKnowledge += workLevel3.Mechanics * (debuffMultiplier * debuffMultiplierAmount);

                break;

            case 3:
                gameManager.Money += workLevel4.Money * (debuffMultiplier * debuffMultiplierAmount);
                gameManager.Tiredness += workLevel4.Tiredness;
                gameManager.WorkExperience += workLevel4.WorkExperience;
                gameManager.GameKnowledge += workLevel4.GameKnowledge * (debuffMultiplier * debuffMultiplierAmount);
                gameManager.GameKnowledge += workLevel4.TeamPlay * (debuffMultiplier * debuffMultiplierAmount);

                break;

        }
    }

    public void TrainResults(string skillName, int trainLevel)
    {
        switch (trainLevel)
        {
            case 0:
                gameManager.Tiredness += trainLevel1.Tiredness;

                switch (skillName)
                {
                    case "Game Knowledge":

                        break;

                    case "Team Play":

                        break;

                    case "Mechanics":

                        break;
                }

                break;

            case 1:
                gameManager.Money += trainLevel2.Money;
                gameManager.Tiredness += trainLevel2.Tiredness;

                switch (skillName)
                {
                    case "Game Knowledge":

                        break;

                    case "Team Play":

                        break;

                    case "Mechanics":

                        break;
                }

                break;

            case 2:
                gameManager.Money += trainLevel3.Money;
                gameManager.Tiredness += trainLevel3.Tiredness;

                switch (skillName)
                {
                    case "Game Knowledge":

                        break;

                    case "Team Play":

                        break;

                    case "Mechanics":

                        break;
                }

                break;

            case 3:
                gameManager.Money += trainLevel4.Money;
                gameManager.Tiredness += trainLevel4.Tiredness;

                switch (skillName)
                {
                    case "Game Knowledge":

                        break;

                    case "Team Play":

                        break;

                    case "Mechanics":

                        break;
                }

                break;

        }
    }

    public void StreamResults(float fame, float duration)
    {

    }

    public void ApplyDebuffs(float debuffMultiplier, float debuffMultiplierAmount)
    {
        SliderToText[] sliders = Resources.FindObjectsOfTypeAll<SliderToText>();

        foreach (SliderToText s in sliders)
        {
            s.DebuffMultiplier = debuffMultiplier;
            s.DebuffMultiplierAmount = debuffMultiplierAmount;
        }
        
    }
}
