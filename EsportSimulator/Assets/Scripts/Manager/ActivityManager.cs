using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivityManager : MonoBehaviour
{
    public enum Activity { Idle, Sleep, Eat, Drink, Work, Train, Battle, Stream, Contest, Plan }
    public Activity currentActivity = Activity.Idle;

	public Training currentTraining;
	public Skill.Type currentSkillType;

    public Battle.Mode currentBattleMode = Battle.Mode.None;

    public UIManager uiManager;
    public ArtManager artManager;
    public TimeManager timeManager;
    public GameManager gameManager;
    public ResultManager resultsManager;
    public ContestManager contestManager;
    public OpponentManager opponentManager;
    public ButtonManager buttonManager;
	public FoodMenu foodMenu;

	public Debugger debug;

	public bool trainingPaid;

    private void Start()
    {
        //ChangeActivity(Activity.Idle, 0);
    }

	/// <summary>
	/// 
	/// </summary>
	/// <param name="activity"></param>
	/// <param name="hourAmount"></param>
	/// <param name="foodLevel"></param>
    public void ChangeActivity(Activity activity, int hourAmount, int foodLevel)
    {
        currentActivity = activity;

        if (debug.noWait)
        {
            timeManager.IncreaseTime(hourAmount, debug.noWait);
            return;
        }

		switch (currentActivity)
		{
			case Activity.Eat:
				resultsManager.Eat(foodLevel);
				return;

			case Activity.Drink:
				resultsManager.Drink(foodLevel);
				return;
		}

        if (hourAmount == 0)
        {
            currentActivity = Activity.Idle;
			currentTraining = new Training(Training.Type.None, 0, new Skill(Skill.Type.None, 0));
            currentBattleMode = Battle.Mode.None;
			artManager.ChangeArt(currentActivity);
			return;
		}

		timeManager.IncreaseTime(hourAmount, false);
		artManager.ChangeArt(currentActivity);
    }

	/// <summary>
	/// 
	/// </summary>
	/// <param name="activity"></param>
	/// <param name="hourAmount"></param>
	public void ChangeActivity(Activity activity, int hourAmount)
	{
		currentActivity = activity;

		if (debug.noWait)
		{
			timeManager.IncreaseTime(hourAmount, debug.noWait);
			return;
		}

		switch (currentActivity)
		{
			case Activity.Battle:
				uiManager.activityText.text = "Battling...";
				break;
				
			case Activity.Plan:
				uiManager.activityText.text = "Planning...";
				break;

			case Activity.Contest:
				uiManager.activityText.text = "Contesting...";
				contestManager.SetParticipants = contestManager.CreateParticipantList(contestManager.GetParticipantAmount, opponentManager.GetAllOpponents);
				break;

			case Activity.Idle:
				uiManager.activityText.text = "Idle";
				break;

			case Activity.Sleep:
				uiManager.activityText.text = "Sleeping...";
				uiManager.ActivateSleepOverlay();
				break;

			case Activity.Stream:
				uiManager.activityText.text = "Streaming...";
				break;

			case Activity.Train:
				if (!gameManager.IsMoneyHighEnough((int)resultsManager.GetTrainingCostAmount(currentTraining.type, hourAmount)))
				{
					Debug.LogWarning("Not enough money");
					return;
				}

				gameManager.DecreaseMoney((int)resultsManager.GetTrainingCostAmount(currentTraining.type, hourAmount));

				uiManager.activityText.text = "Training...";
				break;

			case Activity.Work:
				uiManager.activityText.text = "Working...";
				break;
		}

		if (hourAmount == 0)
		{
			currentActivity = Activity.Idle;
			currentTraining = new Training(Training.Type.None, 0, new Skill(Skill.Type.None, 0));
			currentBattleMode = Battle.Mode.None;
			artManager.ChangeArt(currentActivity);
			return;
		}

		timeManager.IncreaseTime(hourAmount, false);
		artManager.ChangeArt(currentActivity);
	}

	#region Getters & Setters


	#endregion
}
