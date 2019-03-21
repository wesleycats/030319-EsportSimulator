using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the time and date
/// </summary>
public class TimeManager : MonoBehaviour
{
    [SerializeField] private int hour;
    [SerializeField] private int minute = 0;
    [SerializeField] private int month;
    [SerializeField] private int year;

    [SerializeField] private float waitTime;

    public GameManager gameManager;
    public ActivityManager activityManager;
    public UIManager uiManager;
    public ButtonManager buttonManager;
    public ResultManager resultManager;
    public GameData gameData;

    public void IncreaseTime(int hourAmount, bool instant)
    {
        if (hourAmount <= 0) return;
        
        if (instant)
        {
            for (int i = 0; i < hourAmount; i++)
            {
                IncreaseHours(1);
                
                switch (activityManager.currentActivity)
                {
                    case ActivityManager.Activity.Battle:

                        break;

                    case ActivityManager.Activity.Contest:

                        break;

                    case ActivityManager.Activity.Idle:

                        break;

                    case ActivityManager.Activity.Sleep:

                        resultManager.SleepResults(resultManager.GetTirednessDecreaseRate);

                        break;

                    case ActivityManager.Activity.Stream:

                        break;

                    case ActivityManager.Activity.Train:

                        resultManager.TrainResults(activityManager.currentTrainType);

                        break;

                    case ActivityManager.Activity.Work:

                        resultManager.WorkResults(gameManager.WorkLevel);

                        break;

                    default:

                        break;

                }
            }
            
            activityManager.ChangeActivity(ActivityManager.Activity.Idle, 0);
            uiManager.UpdateSkills(gameManager.GameKnowledge, gameManager.TeamPlay, gameManager.Mechanics);
            uiManager.UpdateProgress(gameManager.GetMoney, gameManager.Rating, gameManager.Fame);
            uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
            uiManager.UpdateTime(hour, minute, year, month);
        }
        else
        {
            StartCoroutine(TimeTimer(hourAmount));
        }

    }

    private IEnumerator TimeTimer(float duration)
    {
        float totalDuration = duration;

        buttonManager.DisableAllButtons("Navigation");

        yield return new WaitForSeconds(waitTime);
        duration--;
        IncreaseHours(1);
        
        switch (activityManager.currentActivity)
        {
            case ActivityManager.Activity.Battle:

                break;

            case ActivityManager.Activity.Contest:

                break;

            case ActivityManager.Activity.Idle:

                break;

            case ActivityManager.Activity.Sleep:

                resultManager.SleepResults(resultManager.GetTirednessDecreaseRate);

                break;

            case ActivityManager.Activity.Stream:

                break;

            case ActivityManager.Activity.Train:

                resultManager.TrainResults(activityManager.currentTrainType);

                break;

            case ActivityManager.Activity.Work:

                resultManager.WorkResults(gameManager.WorkLevel);

                break;

            default:

                break;

        }

        uiManager.UpdateSkills(gameManager.GameKnowledge, gameManager.TeamPlay, gameManager.Mechanics);
        uiManager.UpdateProgress(gameManager.GetMoney, gameManager.Rating, gameManager.Fame);
        uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
        uiManager.UpdateTime(hour, minute, year, month);

        if (duration <= 0)
        {
            activityManager.ChangeActivity(ActivityManager.Activity.Idle, 0);
            buttonManager.EnableAllButtons();
        }
        else
        {
            StartCoroutine(TimeTimer(duration));
        }
    }

    /// <summary>
    /// Increases hours and checks time and months
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseHours(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            hour++;

            if (hour >= 24)
            {
                resultManager.PayRent(gameManager.HouseLevel);
                hour = 0;
                month++;
            }

            if (month > 12)
            {
                month = 1;
                year++;
            }
        }

        uiManager.UpdateTime(hour, minute, year, month);
    }

    /// <summary>
    /// Decreases hours and checks time and months
    /// </summary>
    /// <param name="amount"></param>
    public void DecreaseHours(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            hour--;

            if (hour < 0)
            {
                hour = 23;
                month--;
            }

            if (month < 1)
            {
                month = 12;
                year--;
            }
        }

        uiManager.UpdateTime(hour, minute, year, month);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

    public void InitializeGameData()
    {
        hour = gameData.GetHour;
        minute = gameData.GetMinute;
        month = gameData.GetMonth;
        year = gameData.GetYear;
    }

    #region Getters & Setters 

    public int GetHour { get { return hour; } }
    public int GetMinute { get { return minute; } }
    public int GetYear { get { return year; } }
    public int GetMonth { get { return month; } }
    public int SetHour { set { hour = value; } }
    public int SetMinutes { set { minute = value; } }
    public int SetYear { set { year = value; } }
    public int SetMonth { set { month = value; } }

    #endregion
}
