using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
    public GameObject gameOverMenu;
    public GameObject winGameMenu;
    public Text leaderboardNames;
    public Text leaderboardRatings;

    #endregion

    [Tooltip("[0]=champion, [1]=diamond, etc.")]
    public Color[] divisionColors;
    public Image sleepOverlay;
    public Image needsMenuButton;
    public TimeManager timeManager;
    public GameManager gameManager;
    public OpponentManager opponentManager;
    public LeaderboardManager leaderboardManager;

    public void UpdateAll()
    {
        UpdateTime(timeManager.GetHour, timeManager.GetMinute, timeManager.GetYear, timeManager.GetMonth);
        UpdateProgress(gameManager.GetMoney, gameManager.GetRating, gameManager.GetFame);
        UpdateNeeds(gameManager.GetTiredness, gameManager.GetHunger, gameManager.GetThirst);
        UpdateSkills(gameManager.GetGameKnowledge, gameManager.GetTeamPlay, gameManager.GetMechanics);
        UpdateLeaderboard(leaderboardManager.GetLeaderboard);
    }

    public void UpdateTime(int hour, int minute, int year, int month)
    {
        if (hour < 10)
            time.text = "0" + hour.ToString() + ":" + minute.ToString() + "0";
        else
            time.text = hour.ToString() + ":" + minute.ToString() + "0";

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

    public void UpdateSkills(int gameKnowledge, int teamPlay, int mechanics)
    {
        gameKnowledgeValue.text = gameKnowledge.ToString();
        teamPlayValue.text = teamPlay.ToString();
        mechanicsValue.text = mechanics.ToString();
    }

    public void UpdateLeaderboard(List<Opponent> leaderboard)
    {
        leaderboardNames.text = "";
        leaderboardRatings.text = "";

        for (int i = 0; i < leaderboard.Count; i++)
        {
            leaderboardNames.text += i+1 + ". " + leaderboard[i].name + "\n";
            leaderboardRatings.text += leaderboard[i].eloRating.ToString() + "\n";
        }

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

    public void Initialize()
    {
        UpdateAll();
    }
}
