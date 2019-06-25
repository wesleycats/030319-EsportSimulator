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
	[SerializeField] private bool pause = false;

	private int totalDuration;

    public GameManager gameManager;
    public ActivityManager activityManager;
    public UIManager uiManager;
    public ButtonManager buttonManager;
    public ResultManager resultManager;
    public LeaderboardManager lbManager;
    public OpponentManager opponentManager;
    public ContestManager contestManager;
	public GameData gameData;

    public void IncreaseTime(int hourAmount, bool instant)
    {
        if (hourAmount <= 0) return;
        
        if (instant)
        {
            for (int i = 0; i < hourAmount; i++)
            {
                IncreaseHours(1);

                CheckActivity(activityManager.currentActivity, activityManager.currentTraining, gameManager.GetWorkLevel, activityManager.currentBattleMode);
            }

            activityManager.ChangeActivity(ActivityManager.Activity.Idle, 0);
            uiManager.UpdateAll();
        }
        else
        {
            StartCoroutine(TimeTimer(hourAmount));
			totalDuration = hourAmount;
        }

    }

    private IEnumerator TimeTimer(float duration)
    {
		if (Time.timeScale > 0)
			buttonManager.DisableAllButtons("Navigation");

		yield return new WaitForSeconds(waitTime);

		if (!pause)
		{
			duration--;
			IncreaseHours(1);

			if (activityManager.currentActivity == ActivityManager.Activity.Sleep)
			{
				// Every 2 hours the tiredness will be decreased
				int durationDelta = totalDuration - (int)duration;
				if (durationDelta % 2 == 0)
					CheckActivity(activityManager.currentActivity, activityManager.currentTraining, gameManager.GetWorkLevel, activityManager.currentBattleMode);
			}
			else
				CheckActivity(activityManager.currentActivity, activityManager.currentTraining, gameManager.GetWorkLevel, activityManager.currentBattleMode);

			uiManager.UpdateAll();
		}

		if (activityManager.currentActivity == ActivityManager.Activity.Idle || activityManager.currentActivity == ActivityManager.Activity.Plan)
			duration = 0;

		if (duration <= 0)
        {
            activityManager.ChangeActivity(ActivityManager.Activity.Idle, 0);
			resultManager.ResetTotalViews();
			buttonManager.EnableAllButtons("Navigation");
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
                resultManager.PayRent(gameManager.CurrentAccommodation);
                hour = 0;
                month++;

				StartCoroutine(WaitTillIdle());
            }

            if (month > 12)
            {
                month = 1;
                year++;
            }
        }

        uiManager.UpdateTime(hour, minute, year, month);
    }

	public IEnumerator WaitTillIdle()
	{
		yield return new WaitForSeconds(0.5f);

		if (activityManager.currentActivity == ActivityManager.Activity.Idle)
		{
			for (int i = 0; i < gameManager.GetPlannedEvents.Count; i++)
			{
				if (gameManager.GetPlannedEventOn(month, gameManager.GetPlannedEvents) != null)
					activityManager.ChangeActivity(ActivityManager.Activity.Contest, contestManager.GetContestDuration);
			}
		}
		else
		{
			StartCoroutine(WaitTillIdle());
		}
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

    private void CheckActivity(ActivityManager.Activity currentActivity, Training training, int workLevel, Battle.Mode currentBattleMode)
    {
        switch (currentActivity)
        {
            case ActivityManager.Activity.Battle:
                resultManager.BattleResults(lbManager.GetRandomOpponent(lbManager.GetLeaderboard, lbManager.GetOpponentDivision(opponentManager.GetPlayer, lbManager.GetLeaderboard, opponentManager.league)), currentBattleMode);
                break;

			case ActivityManager.Activity.Plan:
				contestManager.PlanTournament(currentBattleMode);
				break;

			case ActivityManager.Activity.Contest:
				for (int i = 0; i < gameManager.GetPlannedEvents.Count; i++)
				{
					if (month == gameManager.GetPlannedEvents[i].month)
					{
						PauseGame();

						StartCoroutine(resultManager.ContestResults(gameManager.GetPlannedEvents[i], contestManager.GetParticipants));

						uiManager.darkOverlay.SetActive(true);
						uiManager.darkOverlay.GetComponent<Button>().interactable = false;
						uiManager.ActivateContestAnnouncement(gameManager.GetPlannedEvents[i].battleMode.ToString());
					}
				}
				break;

            case ActivityManager.Activity.Sleep:
                resultManager.SleepResults(resultManager.GetTirednessDecreaseRate);
                break;

            case ActivityManager.Activity.Stream:
				resultManager.StreamResults(gameManager.GetFame);
                break;

            case ActivityManager.Activity.Train:
                resultManager.TrainResults(training, totalDuration, activityManager.currentSkillType);
                break;

            case ActivityManager.Activity.Work:
                resultManager.WorkResults(workLevel);
                break;

            default:
                Debug.Log("Given activity is not available");
                break;
        }
    }

	#region Getters & Setters 

	public int GetHour { get { return hour; } }
    public int GetMinute { get { return minute; } }
    public int GetYear { get { return year; } }
    public int GetMonth { get { return month; } }
	public bool GetPause { get { return pause; } }

    public int SetHour { set { hour = value; } }
    public int SetMinutes { set { minute = value; } }
    public int SetYear { set { year = value; } }
    public int SetMonth { set { month = value; } }
	public bool SetPause { set { pause = value; } }

	#endregion
}
