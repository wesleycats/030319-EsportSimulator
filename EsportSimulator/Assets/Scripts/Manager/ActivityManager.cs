using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivityManager : MonoBehaviour
{
    public enum Activity { Idle, Sleep, Eat, Drink, Work, Train, Battle, Stream, Contest }
    public Activity currentActivity = Activity.Idle;

    public UIManager uiManager;
    public ArtManager artManager;
    public TimeManager timeManager;

    private void Start()
    {
        ChangeActivity(currentActivity, 0);
    }

    /// <summary>
    /// Sends activity to all receivers
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="duration"></param>
    public void ChangeActivity(Activity activity, int hourAmount)
    {
        switch (activity)
        {
            case Activity.Battle:
                uiManager.activityText.text = "Battling...";

                break;

            case Activity.Contest:
                uiManager.activityText.text = "Contesting...";

                break;

            case Activity.Drink:
                uiManager.activityText.text = "Drinking...";

                break;

            case Activity.Eat:
                uiManager.activityText.text = "Eating...";

                break;

            case Activity.Idle:
                uiManager.activityText.text = "Idle";

                break;

            case Activity.Sleep:
                uiManager.activityText.text = "Sleep...";

                break;

            case Activity.Stream:
                uiManager.activityText.text = "Streaming...";

                break;

            case Activity.Train:
                uiManager.activityText.text = "Training...";

                break;

            case Activity.Work:
                if (hourAmount <= 0) return;
                
                uiManager.activityText.text = "Working...";
                timeManager.IncreaseTime(hourAmount);

                break;

            default:
                uiManager.activityText.text = "Idle";

                break;

        }

        artManager.ChangeArt(activity);
    }
}
