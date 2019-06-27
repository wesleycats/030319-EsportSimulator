using UnityEngine;
using UnityEngine.UI;
using System;

public class Activity : MonoBehaviour
{
    [Tooltip("Used to let activity manager now which action to perform")]
    public ActivityManager.Activity activity;

    [Tooltip("If slider is used, use SetDurationSlider() and keep this 0 ")]
    public int hourAmount;

	[Tooltip("Only used for training action")]
	public Training training;

    [Tooltip("Only used for battling & contests")]
    public Battle.Mode battleMode;

	[SerializeField] int foodLevel;

	[Header("References")]
	public ActivityManager activityManager;
	public ContestManager contestManager;
	public FoodMenu foodMenu;
	public Slider slider;

	public void OnEnable()
	{
		training.skills.Clear();
		training.type = Training.Type.None;
	}

    public int GetSliderDuration()
    {
		return (int)(slider.value * slider.GetComponent<SliderToText>().GetTimeMultiplier);
    }

	public void ChangeActivity()
	{
		if (slider) hourAmount = GetSliderDuration();

		if (contestManager) hourAmount = contestManager.GetContestDuration;

		activityManager.currentTraining = training;
		if (activity == ActivityManager.Activity.Train) activityManager.currentSkillType = training.skills[0].type;
		activityManager.currentBattleMode = battleMode;
		
		activityManager.ChangeActivity(activity, hourAmount);
	}

	public void Consume(int foodLevel)
	{
		if (!foodMenu) return;
			
		if (foodMenu.GetFoodType == FoodMenu.FoodType.eat)
		{
			activity = ActivityManager.Activity.Eat;
		}
		else if (foodMenu.GetFoodType == FoodMenu.FoodType.drink)
		{
			activity = ActivityManager.Activity.Drink;
		}

		activityManager.ChangeActivity(activity, hourAmount, foodLevel);
	}
}
