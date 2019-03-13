using UnityEngine;
using UnityEngine.UI;
using System;

public class Activity : MonoBehaviour
{
    public ActivityManager activityManager;
    public Slider bar;

    public ActivityManager.Activity activity;

    [Tooltip("Use SetDurationSlider() and keep 0 if slider is used")]
    public int duration;

    public void SetDurationSlider()
    {
        if (!bar) return;

        duration = (int)(bar.value * bar.GetComponent<SliderToText>().timeMultiplier);
    }

    public void ChangeActivity()
    {
        activityManager.ChangeActivity(activity);
    }
}
