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

    [SerializeField] private float currentDebuffMultiplier = 1f;
    [SerializeField] private float currentDebuffMultiplierAmount = 1f;
    [SerializeField] private float debuffMultiplier = 0.5f;
    [SerializeField] private float debuffMultiplierAmount = 1f;

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

    public float GetTrainingCostAmount(ActivityManager.TrainType trainType, float duration)
    {
        currentDebuffMultiplier *= currentDebuffMultiplierAmount;

        switch (trainType)
        {
            case ActivityManager.TrainType.WatchingGK:

                return 0;

            case ActivityManager.TrainType.WatchingTP:

                return 0;

            case ActivityManager.TrainType.WatchingM:

                return 0;

            case ActivityManager.TrainType.CourseGK:

                return trainLevel2.Money * currentDebuffMultiplier * duration;

            case ActivityManager.TrainType.CourseTP:

                return trainLevel2.Money * currentDebuffMultiplier * duration;

            case ActivityManager.TrainType.CourseM:

                return trainLevel2.Money * currentDebuffMultiplier * duration;

            case ActivityManager.TrainType.CoursePlusGK:

                return trainLevel3.Money * currentDebuffMultiplier * duration;

            case ActivityManager.TrainType.CoursePlusTP:

                return trainLevel3.Money * currentDebuffMultiplier * duration;

            case ActivityManager.TrainType.CoursePlusM:

                return trainLevel3.Money * currentDebuffMultiplier * duration;

            default:
                Debug.LogError("No training type has been given");
                return 0;
        }
    }

    public void PayRent(int houseLevel)
    {
        switch (houseLevel)
        {
            case 0:
                gameManager.DecreaseMoney(houseLevel1.Money);

                break;
            case 1:
                gameManager.DecreaseMoney(houseLevel2.Money);

                break;
            case 2:
                gameManager.DecreaseMoney(houseLevel3.Money);

                break;
            case 3:
                gameManager.DecreaseMoney(houseLevel4.Money);

                break;
        }
    }

    /// <summary>
    /// Applies the results you will get from sleeping per hour
    /// </summary>
    public void SleepResults(float tirednessDecreaseRate)
    {
        DecreaseTiredness(tirednessDecreaseRate);
    }

    /// <summary>
    /// Applies the results you will get from working per hour
    /// </summary>
    /// <param name="workLevel"></param>
    public void WorkResults(int workLevel)
    {
        currentDebuffMultiplier *= currentDebuffMultiplierAmount;

        switch (workLevel)
        {
            case 0:
                gameManager.IncreaseMoney(workLevel1.Money * currentDebuffMultiplier);
                gameManager.Tiredness += workLevel1.Tiredness;
                gameManager.WorkExperience += workLevel1.WorkExperience;
                break;

            case 1:
                gameManager.IncreaseMoney(workLevel2.Money * currentDebuffMultiplier);
                gameManager.Tiredness += workLevel2.Tiredness;
                gameManager.WorkExperience += workLevel2.WorkExperience;
                gameManager.GameKnowledge += (int)(workLevel2.GameKnowledge * currentDebuffMultiplier);

                break;

            case 2:
                gameManager.IncreaseMoney(workLevel3.Money * currentDebuffMultiplier);
                gameManager.Tiredness += workLevel3.Tiredness;
                gameManager.WorkExperience += workLevel3.WorkExperience;
                gameManager.GameKnowledge += (int)(workLevel3.GameKnowledge * currentDebuffMultiplier);
                gameManager.GameKnowledge += (int)(workLevel3.Mechanics * currentDebuffMultiplier);

                break;

            case 3:
                gameManager.IncreaseMoney(workLevel4.Money * currentDebuffMultiplier);
                gameManager.Tiredness += workLevel4.Tiredness;
                gameManager.WorkExperience += workLevel4.WorkExperience;
                gameManager.GameKnowledge += (int)(workLevel4.GameKnowledge * currentDebuffMultiplier);
                gameManager.GameKnowledge += (int)(workLevel4.TeamPlay * currentDebuffMultiplier);

                break;

            default:
                Debug.LogError("No work level has been given");
                break;
        }

        IncreaseHunger(hungerIncreaseRate);
        IncreaseThirst(thirstIncreaseRate);
    }

    /// <summary>
    /// Applies the results you will get from training per hour
    /// </summary>
    /// <param name="trainType"></param>
    public void TrainResults(ActivityManager.TrainType trainType)
    {
        currentDebuffMultiplier *= currentDebuffMultiplierAmount;

        switch (trainType)
        {
            case ActivityManager.TrainType.WatchingGK:
                gameManager.Tiredness += trainLevel1.Tiredness;
                gameManager.GameKnowledge += (int)(trainLevel1.GameKnowledge * currentDebuffMultiplier);

                break;

            case ActivityManager.TrainType.WatchingTP:
                gameManager.Tiredness += trainLevel1.Tiredness;
                gameManager.TeamPlay += (int)(trainLevel1.TeamPlay * currentDebuffMultiplier);

                break;

            case ActivityManager.TrainType.WatchingM:
                gameManager.Tiredness += trainLevel1.Tiredness;
                gameManager.Mechanics += (int)(trainLevel1.Mechanics * currentDebuffMultiplier);

                break;
                
            case ActivityManager.TrainType.CourseGK:
                gameManager.DecreaseMoney(trainLevel2.Money);
                gameManager.Tiredness += trainLevel2.Tiredness;
                gameManager.GameKnowledge += (int)(trainLevel2.GameKnowledge * currentDebuffMultiplier);

                break;

            case ActivityManager.TrainType.CourseTP:
                gameManager.DecreaseMoney(trainLevel2.Money);
                gameManager.Tiredness += trainLevel2.Tiredness;
                gameManager.TeamPlay += (int)(trainLevel2.TeamPlay * currentDebuffMultiplier);

                break;

            case ActivityManager.TrainType.CourseM:
                gameManager.DecreaseMoney(trainLevel2.Money);
                gameManager.Tiredness += trainLevel2.Tiredness;
                gameManager.Mechanics += (int)(trainLevel2.Mechanics * currentDebuffMultiplier);

                break;
                
            case ActivityManager.TrainType.CoursePlusGK:
                gameManager.DecreaseMoney(trainLevel3.Money);
                gameManager.Tiredness += trainLevel3.Tiredness;
                gameManager.GameKnowledge += (int)(trainLevel3.GameKnowledge * currentDebuffMultiplier);

                break;

            case ActivityManager.TrainType.CoursePlusTP:
                gameManager.DecreaseMoney(trainLevel3.Money);
                gameManager.Tiredness += trainLevel3.Tiredness;
                gameManager.TeamPlay += (int)(trainLevel3.TeamPlay * currentDebuffMultiplier);

                break;

            case ActivityManager.TrainType.CoursePlusM:
                gameManager.DecreaseMoney(trainLevel3.Money);
                gameManager.Tiredness += trainLevel3.Tiredness;
                gameManager.Mechanics += (int)(trainLevel3.Mechanics * currentDebuffMultiplier);

                break;

            default:
                Debug.LogError("No training type has been given");
                break;
        }
    }

    public void StreamResults(float fame, float duration)
    {

    }

    public void ApplyDebuffs(float debuffMultiplier, float debuffMultiplierAmount)
    {
        currentDebuffMultiplier = debuffMultiplier;
        currentDebuffMultiplierAmount = debuffMultiplierAmount;

        SliderToText[] sliders = Resources.FindObjectsOfTypeAll<SliderToText>();

        foreach (SliderToText s in sliders)
        {
            s.DebuffMultiplier = currentDebuffMultiplier;
            s.DebuffMultiplierAmount = currentDebuffMultiplierAmount;
        }
    }

    public void ResetDebuffs()
    {
        currentDebuffMultiplier = 1f;
        debuffMultiplierAmount = 1f;

        SliderToText[] sliders = Resources.FindObjectsOfTypeAll<SliderToText>();

        foreach (SliderToText s in sliders)
        {
            s.DebuffMultiplier = currentDebuffMultiplier;
            s.DebuffMultiplierAmount = currentDebuffMultiplierAmount;
        }
    }

    public void Eat(int foodLevel)
    {
        switch (foodLevel)
        {
            case 1:
                //TODO create not enough money signal
                if (gameManager.GetMoney < foodBad.Money) return;

                DecreaseHunger(foodBad.Hunger);
                gameManager.DecreaseMoney(foodBad.Money);
                break;
            case 2:
                //TODO create not enough money signal
                if (gameManager.GetMoney < foodStandard.Money) return;

                DecreaseHunger(foodStandard.Hunger);
                gameManager.DecreaseMoney(foodStandard.Money);
                break;
            case 3:
                //TODO create not enough money signal
                if (gameManager.GetMoney < foodGood.Money) return;

                DecreaseHunger(foodGood.Hunger);
                gameManager.DecreaseMoney(foodGood.Money);
                break;
            case 4:
                //TODO create not enough money signal
                if (gameManager.GetMoney < foodExcellent.Money) return;

                DecreaseHunger(foodExcellent.Hunger);
                gameManager.DecreaseMoney(foodExcellent.Money);
                break;

            default:
                Debug.LogError("No food level has been given");
                break;
        }
        
        uiManager.UpdateProgress(gameManager.GetMoney, gameManager.Rating, gameManager.Fame);
        uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
    }

    public void Drink(int drinkLevel)
    {
        switch (drinkLevel)
        {
            case 1:
                //TODO create not enough money signal
                if (gameManager.GetMoney < drinkBad.Money) return;

                DecreaseThirst(drinkBad.Thirst);
                gameManager.DecreaseMoney(drinkBad.Money);
                break;
            case 2:
                //TODO create not enough money signal
                if (gameManager.GetMoney < drinkStandard.Money) return;

                DecreaseThirst(drinkStandard.Thirst);
                gameManager.DecreaseMoney(drinkStandard.Money);
                break;
            case 3:
                //TODO create not enough money signal
                if (gameManager.GetMoney < drinkGood.Money) return;

                DecreaseThirst(drinkGood.Thirst);
                gameManager.DecreaseMoney(drinkGood.Money);
                break;
            case 4:
                //TODO create not enough money signal
                if (gameManager.GetMoney < drinkExcellent.Money) return;

                DecreaseThirst(drinkExcellent.Thirst);
                gameManager.DecreaseMoney(drinkExcellent.Money);
                break;

            default:
                Debug.LogError("No drink level has been given");
                break;
        }

        uiManager.UpdateProgress(gameManager.GetMoney, gameManager.Rating, gameManager.Fame);
        uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
    }

    private void IncreaseTiredness(float amount)
    {
        gameManager.Tiredness += amount;

        if (gameManager.Tiredness >= 70)
        {
            if (uiManager.needsMenuButton.GetComponent<LerpColor>().endColor != Color.red)
            {
                uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.yellow;
                uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
            }
        }

        if (gameManager.Tiredness >= 100)
        {
            ApplyDebuffs(debuffMultiplier, debuffMultiplierAmount);
            uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.red;
        }
    }
    
    private void IncreaseHunger(float amount)
    {
        gameManager.Hunger += amount;

        if (gameManager.Hunger >= 70)
        {
            if (uiManager.needsMenuButton.GetComponent<LerpColor>().endColor != Color.red)
            {
                uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.yellow;
                uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
            }
        }

        if (gameManager.Hunger >= 100)
        {
            ApplyDebuffs(debuffMultiplier, debuffMultiplierAmount);
            uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.red;
        }
    }

    private void IncreaseThirst(float amount)
    {
        gameManager.Thirst += amount;

        if (gameManager.Thirst >= 70)
        {
            if (uiManager.needsMenuButton.GetComponent<LerpColor>().endColor != Color.red)
            {
                uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.yellow;
                uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
            }
        }

        if (gameManager.Thirst >= 100)
        {
            ApplyDebuffs(debuffMultiplier, debuffMultiplierAmount);
            uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.red;
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
    
    #region Getters & Setters

    public float GetTirednessDecreaseRate { get { return tirednessDecreaseRate; } }

    #endregion
}
