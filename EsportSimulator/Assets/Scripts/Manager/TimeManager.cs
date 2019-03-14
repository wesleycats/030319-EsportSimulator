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

    public void IncreaseTime(int hourAmount)
    {
        if (hourAmount <= 0) return;

        float duration = 0;
        duration = hourAmount;

        StartCoroutine(TimeTimer(duration));
    }

    private IEnumerator TimeTimer(float duration)
    {
        float totalDuration = duration;

        buttonManager.DisEnableAllButtons(false, "Navigation");

        yield return new WaitForSeconds(waitTime);
        duration--;
        hour++;

        if (hour >= 24)
        {
            hour = 0;
            month++;
        }

        if (month > 12)
        {
            month = 0;
            year++;
        }

        resultManager.WorkResults();

        uiManager.UpdateProgress(gameManager.Money, gameManager.Rating, gameManager.Fame);
        uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
        uiManager.UpdateTime(hour, minutes, year, month);

        if (duration <= 0)
        {
            activityManager.ChangeActivity(ActivityManager.Activity.Idle, 0);
            buttonManager.DisEnableAllButtons(true);
        }
        else
        {
            StartCoroutine(TimeTimer(duration));
        }
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
        hour = gameData.Hour;
        //minutes = gameData.minutes;
        month = gameData.Hour;
        year = gameData.Hour;


    }

    #region Getters & Setters 

    public int Hour { get { return hour; } }
    public int Minutes { get { return minutes; } }
    public int Year { get { return year; } }
    public int Month { get { return month; } }

    #endregion
}
