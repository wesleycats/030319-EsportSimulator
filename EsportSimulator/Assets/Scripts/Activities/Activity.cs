using UnityEngine;
using UnityEngine.UI;
using System;

public class Activity : MonoBehaviour
{
    public ActivityManager activityManager;
    public Slider bar;

    public ActivityManager.Activity activity;

    [Tooltip("Use SetDurationSlider() and keep 0 if slider is used")]
    public int hourAmount;

    public void SetDurationSlider()
    {
        if (!bar) return;

        hourAmount = (int)(bar.value * bar.GetComponent<SliderToText>().TimeMultiplier);
    }

    public void ChangeActivity()
    {
        activityManager.ChangeActivity(activity, hourAmount);
    }
}
