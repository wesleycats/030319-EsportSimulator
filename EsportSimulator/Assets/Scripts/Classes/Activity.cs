using UnityEngine;
using UnityEngine.UI;
using System;

public class Activity : MonoBehaviour
{
    public ActivityManager activityManager;
    public Slider bar;

    [Tooltip("Used to let activity manager now which action to perform")]
    public ActivityManager.Activity activity;

    [Tooltip("If slider is used, use SetDurationSlider() and keep this 0 ")]
    public int hourAmount;

    [Tooltip("Only used for training action")]
    public ActivityManager.TrainType trainType;

    [Tooltip("Only used for battling & contests")]
    public Battle.Mode battleMode;

    public int GetSliderDuration()
    {
        return (int)(bar.value * bar.GetComponent<SliderToText>().TimeMultiplier);
    }

    public void ChangeActivity()
    {
        if (bar) hourAmount = GetSliderDuration();

        activityManager.currentTrainType = trainType;
        activityManager.currentBattleMode = battleMode;
        activityManager.ChangeActivity(activity, hourAmount);
    }
}
