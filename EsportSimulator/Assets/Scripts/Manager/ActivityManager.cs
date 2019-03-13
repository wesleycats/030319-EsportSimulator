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

    private void Start()
    {
        ChangeActivity(currentActivity);
    }

    /// <summary>
    /// Sends activity to all receivers
    /// </summary>
    /// <param name="activity"></param>
    /// <param name="duration"></param>
    public void ChangeActivity(Activity activity)
    {
        currentActivity = activity;
        artManager.ChangeArt(currentActivity);

        switch (currentActivity)
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
                uiManager.activityText.text = "Working...";

                break;

            default:
                uiManager.activityText.text = "Idle";

                break;

        }
    }
}
