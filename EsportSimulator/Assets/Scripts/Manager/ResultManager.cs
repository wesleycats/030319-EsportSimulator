using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [Header("Houses")]
    [SerializeField] private ResultsForm houseLevel1;
    [SerializeField] private ResultsForm houseLevel2;
    [SerializeField] private ResultsForm houseLevel3;
    [SerializeField] private ResultsForm houseLevel4;

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
    [Tooltip("Results per purchase")]
    [SerializeField] private ResultsForm foodBad;
    [SerializeField] private ResultsForm foodStandard;
    [SerializeField] private ResultsForm foodGood;
    [SerializeField] private ResultsForm foodExcellent;
    
    [Header("Drinks")]
    [Tooltip("Results per purchase")]
    [SerializeField] private ResultsForm drinkBad;
    [SerializeField] private ResultsForm drinkStandard;
    [SerializeField] private ResultsForm drinkGood;
    [SerializeField] private ResultsForm drinkExcellent;
    
    [SerializeField] private bool debuff = false;
    
    [SerializeField] private float debuffMultiplier = 0.5f;
    [SerializeField] private float debuffMultiplierAmount = 0f;

    [SerializeField] private float hungerIncreaseRate = 10;
    [SerializeField] private float thirstIncreaseRate = 5;

    // How much tiredness you lose by sleeping
    [SerializeField] private float tirednessDecreaseRate = 25;


    public UIManager uiManager;
    public GameManager gameManager;
    public PlayerData playerData;

    private void Start()
    {
        if (debuff)
            ApplyDebuffs(debuffMultiplier, debuffMultiplierAmount);
    }

    public void PayRent()
    {
        switch (gameManager.HouseLevel)
        {
            case 0:
                gameManager.Money -= houseLevel1.Money;
                break;
            case 1:
                gameManager.Money -= houseLevel2.Money;

                break;
            case 2:
                gameManager.Money -= houseLevel3.Money;

                break;
            case 3:
                gameManager.Money -= houseLevel4.Money;

                break;
        }
    }

    /// <summary>
    /// Determines the amount of tiredness you lose per hour while sleeping
    /// </summary>
    public void SleepResults()
    {
        DecreaseTiredness(tirednessDecreaseRate);
    }

    public void WorkResults()
    {
        switch (gameManager.WorkLevel)
        {
            case 0:
                gameManager.Money += workLevel1.Money - (workLevel1.Money * (debuffMultiplier * debuffMultiplierAmount));
                gameManager.Tiredness += workLevel1.Tiredness;
                gameManager.WorkExperience += workLevel1.WorkExperience;
                break;

            case 1:
                gameManager.Money += workLevel2.Money - (workLevel2.Money * (debuffMultiplier * debuffMultiplierAmount));
                gameManager.Tiredness += workLevel2.Tiredness;
                gameManager.WorkExperience += workLevel2.WorkExperience;
                gameManager.GameKnowledge += workLevel2.GameKnowledge - (workLevel2.GameKnowledge * (debuffMultiplier * debuffMultiplierAmount));

                break;

            case 2:
                gameManager.Money += workLevel3.Money - (workLevel3.Money * (debuffMultiplier * debuffMultiplierAmount));
                gameManager.Tiredness += workLevel3.Tiredness;
                gameManager.WorkExperience += workLevel3.WorkExperience;
                gameManager.GameKnowledge += workLevel3.GameKnowledge - (workLevel3.GameKnowledge * (debuffMultiplier * debuffMultiplierAmount));
                gameManager.GameKnowledge += workLevel3.Mechanics - (workLevel3.Mechanics * (debuffMultiplier * debuffMultiplierAmount));

                break;

            case 3:
                gameManager.Money += workLevel4.Money - (workLevel4.Money * (debuffMultiplier * debuffMultiplierAmount));
                gameManager.Tiredness += workLevel4.Tiredness;
                gameManager.WorkExperience += workLevel4.WorkExperience;
                gameManager.GameKnowledge += workLevel4.GameKnowledge - (workLevel4.GameKnowledge * (debuffMultiplier * debuffMultiplierAmount));
                gameManager.GameKnowledge += workLevel4.TeamPlay - (workLevel4.TeamPlay * (debuffMultiplier * debuffMultiplierAmount));

                break;
        }

        IncreaseHunger(hungerIncreaseRate);
        IncreaseThirst(thirstIncreaseRate);
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

    public void Eat(int foodLevel)
    {
        switch (foodLevel)
        {
            case 1:
                DecreaseHunger(foodBad.Hunger);
                DecreaseMoney(foodBad.Money);
                break;
            case 2:
                DecreaseHunger(foodStandard.Hunger);
                DecreaseMoney(foodStandard.Money);
                break;
            case 3:
                DecreaseHunger(foodGood.Hunger);
                DecreaseMoney(foodGood.Money);
                break;
            case 4:
                DecreaseHunger(foodExcellent.Hunger);
                DecreaseMoney(foodExcellent.Money);
                break;
        }

        uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
        uiManager.UpdateProgress(gameManager.Money, gameManager.Rating, gameManager.Fame);
    }

    public void Drink(int drinkLevel)
    {
        switch (drinkLevel)
        {
            case 1:
                DecreaseThirst(drinkBad.Thirst);
                DecreaseMoney(drinkBad.Money);
                break;
            case 2:
                DecreaseThirst(drinkStandard.Thirst);
                DecreaseMoney(drinkStandard.Money);
                break;
            case 3:
                DecreaseThirst(drinkGood.Thirst);
                DecreaseMoney(drinkGood.Money);
                break;
            case 4:
                DecreaseThirst(drinkExcellent.Thirst);
                DecreaseMoney(drinkExcellent.Money);
                break;
        }

        uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
        uiManager.UpdateProgress(gameManager.Money, gameManager.Rating, gameManager.Fame);
    }

    private void IncreaseTiredness(float amount)
    {
        gameManager.Tiredness += amount;

        if (gameManager.Tiredness > 100)
        {
            gameManager.Tiredness = 100;
        }
        else if (gameManager.Tiredness > 70)
        {
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
            Debug.Log("HI");
        }
    }

    
    private void IncreaseHunger(float amount)
    {
        gameManager.Hunger += amount;

        if (gameManager.Hunger > 100)
        {
            gameManager.Hunger = 100;
        }
        else if (gameManager.Hunger > 70)
        {
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
        }
    }

    private void IncreaseThirst(float amount)
    {
        gameManager.Thirst += amount;

        if (gameManager.Thirst > 100)
        {
            gameManager.Thirst = 100;
        }
        else if (gameManager.Thirst > 70)
        {
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
        }
    }

    private void DecreaseTiredness(float amount)
    {
        gameManager.Tiredness -= amount;

        if (gameManager.Tiredness < 0)
        {
            gameManager.Tiredness = 0;
        }
        else if (gameManager.Tiredness < 70 && gameManager.Hunger < 70 && gameManager.Thirst < 70)
        {
            uiManager.needsMenuButton.GetComponent<Image>().color = uiManager.needsMenuButton.GetComponent<LerpColor>().startColor;
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = false;
        }
    }

    private void DecreaseHunger(float amount)
    {
        gameManager.Hunger -= amount;

        if (gameManager.Hunger < 0)
        {
            gameManager.Hunger = 0;
        }
        else if (gameManager.Tiredness < 70 && gameManager.Hunger < 70 && gameManager.Thirst < 70)
        {
            uiManager.needsMenuButton.GetComponent<Image>().color = uiManager.needsMenuButton.GetComponent<LerpColor>().startColor;
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = false;
        }
    }

    private void DecreaseThirst(float amount)
    {
        gameManager.Thirst -= amount;

        if (gameManager.Thirst < 0)
        {
            gameManager.Thirst = 0;
        }
        else if (gameManager.Tiredness < 70 && gameManager.Hunger < 70 && gameManager.Thirst < 70)
        {
            uiManager.needsMenuButton.GetComponent<Image>().color = uiManager.needsMenuButton.GetComponent<LerpColor>().startColor;
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = false;
        }
    }

    private void IncreaseMoney(float amount)
    {
        gameManager.Money += amount;
    }

    private void DecreaseMoney(float amount)
    {
        gameManager.Money -= amount;
    }

}
