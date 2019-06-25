using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuManager : MonoBehaviour
{
	[Header("References")]
	public ResultManager resultManager;
	public GameManager gameManager;
	public Activity actionButton;
	public Slider durationSlider;
	public Text menuTitle;
	public Text resultText;

	public void SetupSubMenu(string skill)
	{
		if (skill != "")
		{
			actionButton.training.type = Training.Type.Watching;
			actionButton.training.skills.Add(new Skill(GetTrainingSkill(skill), resultManager.GetTrainingRate(actionButton.training.type)));
			menuTitle.text = actionButton.training.skills[0].type.ToString();
		}

		UpdateSliderValues(actionButton.activity);
	}

	public void UpdateTrainingType(string trainType)
	{
		actionButton.training.type = GetTrainingType(trainType);

		foreach (Skill s in actionButton.training.skills)
		{
			s.amount = resultManager.GetTrainingRate(actionButton.training.type);
		}
	}

	public void UpdateSliderValues(ActivityManager.Activity activity)
	{
		SliderToText sliderText = durationSlider.GetComponent<SliderToText>();

		switch (activity)
		{
			case ActivityManager.Activity.Sleep:
				sliderText.SetSliderMaxValue(activity, resultManager.GetTirednessDecreaseRate, gameManager.GetTiredness);
				sliderText.SetTirednessMultiplier = resultManager.GetTirednessDecreaseRate;
				sliderText.ValueToSleepResult(resultText);

				break;

			case ActivityManager.Activity.Work:
				sliderText.SetSliderMaxValue(activity, resultManager.GetWorkResultForm(gameManager.WorkLevel).Tiredness, 100 - gameManager.GetTiredness);
				break;

			case ActivityManager.Activity.Stream:
				sliderText.SetSliderMaxValue(activity, resultManager.streamEnergyCost, 100 - gameManager.GetTiredness);
				sliderText.ValueToStreamResult(resultText);
				break;

			case ActivityManager.Activity.Train:
				sliderText.SetSliderMaxValue(activity, resultManager.GetTrainingResultsForm(actionButton.training.type).Tiredness, 100 - gameManager.GetTiredness);
				sliderText.SetSkills = actionButton.training.skills;
				sliderText.SetTirednessMultiplier = resultManager.GetTrainingTirednessAmount(actionButton.training.type, 1);
				sliderText.SetCostMultiplier = resultManager.GetTrainingCostAmount(actionButton.training.type, 1);
				sliderText.ValueToTrainResult(resultText);
				break;

			default:
				Debug.Log("Given activity is unavailable");
				break;
		}
	}

	public void UpdateSliderValues()
	{
		UpdateSliderValues(actionButton.activity);
	}

	public Skill.Type GetTrainingSkill(string skill)
	{
		switch (skill)
		{
			case "GameKnowledge":
				return Skill.Type.GameKnowledge;

			case "TeamPlay":
				return Skill.Type.TeamPlay;

			case "Mechanics":
				return Skill.Type.Mechanics;

			default:
				Debug.LogWarning("Given skill is unavailable");
				return Skill.Type.None;
		}
	}

	public Training.Type GetTrainingType(string trainingType)
	{
		switch (trainingType)
		{
			case "Watching":
				return Training.Type.Watching;

			case "Course":
				return Training.Type.Course;

			case "CoursePlus":
				return Training.Type.CoursePlus;

			default:
				Debug.LogWarning("Given training type is unavailable");
				return Training.Type.None;
		}
	}

	public int GetMaxTrainingHours(Training.Type trainType)
	{
		//int requiredEnergy = 0;
		int addedTiredness = 0;

		switch (trainType)
		{
			case Training.Type.Watching:
				addedTiredness = resultManager.GetTrainingLevel1Results.Tiredness;
				break;

			case Training.Type.Course:
				addedTiredness = resultManager.GetTrainingLevel2Results.Tiredness;
				break;

			case Training.Type.CoursePlus:
				addedTiredness = resultManager.GetTrainingLevel3Results.Tiredness;
				break;

			default:
				Debug.LogWarning("Given training type is unavailable");
				return 0;
		}

		return 100 / addedTiredness;
	}
}
