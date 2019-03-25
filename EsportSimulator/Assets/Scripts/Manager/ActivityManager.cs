using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivityManager : MonoBehaviour
{
    public enum Activity { Idle, Sleep, Eat, Drink, Work, Train, Battle, Stream, Contest }
    public Activity currentActivity = Activity.Idle;

    public enum TrainType { None, WatchingGK, WatchingTP, WatchingM, CourseGK, CourseTP, CourseM, CoursePlusGK, CoursePlusTP, CoursePlusM }
    [Tooltip("GK = Game Knowledge, TP = Team Play, M = Mechanics")]
    public TrainType currentTrainType = TrainType.None;

    public Battle.Mode currentBattleMode = Battle.Mode.None;

    public UIManager uiManager;
    public ArtManager artManager;
    public TimeManager timeManager;
    public GameManager gameManager;
    public ResultManager resultsManager;

    public Debugger debug;

    private void Start()
    {
        ChangeActivity(Activity.Idle, 0);
    }

    /// <summary>
    /// Sends activity to all receivers
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="duration"></param>
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

            case Activity.Contest:
                uiManager.activityText.text = "Contesting...";
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
                //TODO create not enough money signal
                if (gameManager.GetMoney < resultsManager.GetTrainingCostAmount(currentTrainType, hourAmount)) return;
                
                uiManager.activityText.text = "Training...";
                break;

            case Activity.Work:                
                uiManager.activityText.text = "Working...";
                break;
        }

        if (hourAmount == 0)
        {
            currentActivity = Activity.Idle;
            currentTrainType = TrainType.None;
            currentBattleMode = Battle.Mode.None;
        }

        timeManager.IncreaseTime(hourAmount, false);
        artManager.ChangeArt(currentActivity);
    }
}
