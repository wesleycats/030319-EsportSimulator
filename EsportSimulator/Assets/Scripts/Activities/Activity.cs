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

    public int GetSliderDuration()
    {
        if (!bar) return 0;

        return (int)(bar.value * bar.GetComponent<SliderToText>().TimeMultiplier);
    }

    public void ChangeActivity()
    {
        hourAmount = GetSliderDuration();
        activityManager.currentTrainType = trainType;
        activityManager.ChangeActivity(activity, hourAmount);
    }
}
