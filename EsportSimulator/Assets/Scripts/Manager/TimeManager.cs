using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the time and date
/// </summary>
public class TimeManager : MonoBehaviour
{
    [SerializeField] private int hours;
    [SerializeField] private int minutes = 0;
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
                AddHours(1);

                if (hours >= 24)
                {
                    hours = 0;
                    month++;
                }

                if (month > 12)
                {
                    month = 0;
                    year++;
                }
            }
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
        AddHours(1);

        if (hours >= 24)
        {
            hours = 0;
            month++;
            resultManager.PayRent();
        }

        if (month > 12)
        {
            month = 0;
            year++;
        }
        
        switch (activityManager.currentActivity)
        {
            case ActivityManager.Activity.Battle:

                break;

            case ActivityManager.Activity.Contest:

                break;

            case ActivityManager.Activity.Idle:

                break;

            case ActivityManager.Activity.Sleep:

                resultManager.SleepResults();

                break;

            case ActivityManager.Activity.Stream:

                break;

            case ActivityManager.Activity.Train:

                break;

            case ActivityManager.Activity.Work:

                resultManager.WorkResults();

                break;

            default:

                break;

        }

        uiManager.UpdateProgress(gameManager.Money, gameManager.Rating, gameManager.Fame);
        uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
        uiManager.UpdateTime(hours, minutes, year, month);

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
    /// For debug is set public
    /// </summary>
    /// <param name="amount"></param>
    public void AddHours(int amount)
    {
        hours += amount;
        uiManager.UpdateTime(hours, minutes, year, month);
    }

    public void UnAndPauseGame(bool pause)
    {
        if (pause)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void InitializeGameData()
    {
        hours = gameData.Hour;
        //minutes = gameData.minutes;
        month = gameData.Hour;
        year = gameData.Hour;


    }

    #region Getters & Setters 

    public int Hour { get { return hours; } }
    public int Minutes { get { return minutes; } }
    public int Year { get { return year; } }
    public int Month { get { return month; } }

    #endregion
}
