using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Just for initialization
    #region UI Elements

    public Text time;
    public Text date;
    public Text moneyValue;
    public Text ratingValue;
    public Text fameValue;
    public Text tirednessValue;
    public Slider tirednessBar;
    public Text hungerValue;
    public Slider hungerBar;
    public Text thirstValue;
    public Slider thirstBar;
    public Text gameKnowledgeValue;
    public Text teamPlayValue;
    public Text mechanicsValue;
    public Text activityText;

    #endregion
    
    public Image sleepOverlay;
    public Image needsMenuButton;
    public PlayerData playerData;
    public TimeManager timeManager;

    public void UpdateTime(int hour, int minutes, int year, int month)
    {
        if (hour < 10)
            time.text = "0" + hour.ToString() + ":" + minutes.ToString() + "0";
        else
            time.text = hour.ToString() + ":" + minutes.ToString() + "0";

        date.text = "Month " + month.ToString() + " of year " + year.ToString();
    }

    public void UpdateProgress(float money, float rating, float fame)
    {
        moneyValue.text = money.ToString();
        ratingValue.text = rating.ToString();
        fameValue.text = fame.ToString();
    }

    public void UpdateNeeds(float tiredness, float hunger, float thirst)
    {
        tirednessValue.text = tiredness.ToString() + "%";
        hungerValue.text = hunger.ToString() + "%";
        thirstValue.text = thirst.ToString() + "%";

        tirednessBar.value = tiredness / 100;
        hungerBar.value = hunger / 100;
        thirstBar.value = thirst / 100;
    }

    public void UpdateSkills(float gameKnowledge, float teamPlay, float mechanics)
    {
        gameKnowledgeValue.text = gameKnowledge.ToString();
        teamPlayValue.text = teamPlay.ToString();
        mechanicsValue.text = mechanics.ToString();
    }

    public void ActivateSleepOverlay()
    {
        sleepOverlay.GetComponent<LerpColor>().Increasing = true;
        sleepOverlay.GetComponent<LerpColor>().LerpMaxAmount = 2;
        sleepOverlay.GetComponent<LerpColor>().LerpActivated = true;
    }

    public void DeactivateSleepOverlay()
    {
        sleepOverlay.GetComponent<LerpColor>().Increasing = false;
        sleepOverlay.GetComponent<LerpColor>().LerpActivated = true;
    }
    
    public void InitializePlayerData()
    {
        moneyValue.text = playerData.Money.ToString();
        ratingValue.text = playerData.Rating.ToString();
        fameValue.text = playerData.Fame.ToString();
        tirednessValue.text = playerData.Tiredness.ToString() + "%";
        hungerValue.text = playerData.Hunger.ToString() + "%";
        thirstValue.text = playerData.Thirst.ToString() + "%";
        gameKnowledgeValue.text = playerData.GameKnowledge.ToString();
        teamPlayValue.text = playerData.TeamPlay.ToString();
        mechanicsValue.text = playerData.Mechanics.ToString();

        tirednessBar.value = playerData.Tiredness / 100;
        hungerBar.value = playerData.Hunger / 100;
        thirstBar.value = playerData.Thirst / 100;
    }
    
    public void InitializeGameData()
    {
        time.text = timeManager.Hour.ToString() + "0:" + timeManager.Minutes.ToString() + "0";
        date.text = "Month " + timeManager.Month.ToString() + " of year " + timeManager.Year.ToString();
    }
}
