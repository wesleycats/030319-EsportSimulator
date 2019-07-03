using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderToText : MonoBehaviour
{
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float costMultiplier;
    [SerializeField] private float incomeMultiplier;
    [SerializeField] private float skillMultiplier;
	[SerializeField] private float tirednessMultiplier;
    [SerializeField] private string skillName;
    [SerializeField] private bool sleep;
	[SerializeField] private List<Skill> skills = new List<Skill>();

	private float duration;
    private int tiredness;
    private float hunger;
    private float thirst;
    private float money;
    private float moneyMin;
    private float moneyMax;
    private float rating;
    private float fame;
    private float skill;

    private float debuffMultiplier = 1f;
    private float debuffMultiplierAmount = 1f;

	[Header("References")]
	public GameManager gameManager;
	public ResultManager resultManager;
	public Activity activityButton;
	public Text debuffMultiplierText;

	private Slider slider;

	private void Awake()
	{
		slider = GetComponent<Slider>();
	}

	private void Start()
    {
		ResetSlider();
	}

	private void OnDisable()
	{
		ResetValues();
	}

	public void ValueToTimeText(Text textToChange)
    {
        duration = slider.value * timeMultiplier;

        textToChange.text = duration.ToString() + " hours";
    }

    public void ValueToSleepResult(Text textToChange)
    {
        tiredness = (int)(slider.value * resultManager.GetTirednessDecreaseRate);

        textToChange.text = "-" + tiredness.ToString() + "% tiredness";
    }

	public void ValueToResult(Text textToChange)
	{
		switch (activityButton.activity)
		{
			case ActivityManager.Activity.Work:
				tiredness = GetTotalTiredness(resultManager.GetWorkResultForm(gameManager.GetWorkLevel).Tiredness);
				money = slider.value * resultManager.GetWorkResultForm(gameManager.GetWorkLevel).Money;

				textToChange.text = "+$" + money.ToString() + ", -" + tiredness.ToString() + "% tiredness";
				break;

			case ActivityManager.Activity.Train:
				tiredness = GetTotalTiredness((int)resultManager.GetTrainingTirednessAmount(activityButton.training.type, 1));
				money = slider.value * costMultiplier;

				string skillText = GetSkillText(skills, slider);

				textToChange.text = skillText + "\n+" + tiredness.ToString() + "% tiredness\n-$" + money.ToString();
				break;

			case ActivityManager.Activity.Stream:

				break;
		}
    }

    public void ValueToStreamResult(Text textToChange)
    {
        tiredness = (int)(slider.value * resultManager.streamEnergyCost);
        moneyMin = resultManager.GetMinStreamIncome(gameManager.GetFame, (int)slider.value);
        moneyMax = resultManager.GetMaxStreamIncome(gameManager.GetFame, (int)slider.value);

        textToChange.text = "between +$" + moneyMin.ToString() + "-$" + moneyMax.ToString() + ", -" + tiredness.ToString() + "% tiredness";
    }

	public void ResetSlider()
	{
		float origMax = slider.maxValue;
		slider.maxValue = 1f;
		slider.value = 1f;
		slider.maxValue = origMax;
		slider.value = 0;
	}

	public void ResetValues()
    {
        slider.value = 0f;
        tiredness = 0;
        hunger = 0f;
        thirst = 0f;
        money = 0f;
        rating = 0f;
        fame = 0f;
        skill = 0f;
		costMultiplier = 0f;
		incomeMultiplier = 0f;
		skillMultiplier = 0f;
		tirednessMultiplier = 0f;
		skills.Clear();
	}

	public int GetTotalTiredness(int rate)
	{
		int tiredness = 0;
		int hungerRate = resultManager.GetHungerIncreaseRate;
		int thirstRate = resultManager.GetThirstIncreaseRate;
		//Debug.Log("hunger at " + (slider.value) + " will be " + (gameManager.Hunger + (hungerRate * slider.value)));
		float debuffMultiplier = gameManager.GetDebuffMultiplier((int)(gameManager.Hunger + (hungerRate * slider.value)), (int)(gameManager.Thirst + (thirstRate * slider.value)));
		//Debug.Log("debuff multiplier at " + (slider.value) + " will be " + debuffMultiplier);

		float buffer = 0;

		for (int i = 0; i < slider.value + 1; i++)
		{
			debuffMultiplier = gameManager.GetDebuffMultiplier((int)(gameManager.Hunger + (hungerRate * i)), (int)(gameManager.Thirst + (thirstRate * i)));

			if (debuffMultiplier > 1)
			{
				tiredness = rate * (i - 1);
				//Debug.Log("tiredness= " + tiredness);

				for (int j = i; j < slider.value + 1; j++)
				{
					float difference = slider.value + 1 - j;
					//Debug.Log("from j" + j + " to " + (slider.value + 1));
					buffer = rate * debuffMultiplier * difference;
					//Debug.Log("buffer = " + buffer);
					break;
				}
				break;
			}

			tiredness = rate * i;
			//Debug.Log("tiredness= " + tiredness);
		}

		debuffMultiplier = gameManager.GetDebuffMultiplier((int)(gameManager.Hunger + (hungerRate * slider.value)), (int)(gameManager.Thirst + (thirstRate * slider.value)));
		SetMultiplierText(debuffMultiplier);

		return (int)(tiredness + buffer);
	}

	public string GetSkillText(List<Skill> skills, Slider slider)
	{
		string skillText = "";

		for (int i = 0; i < skills.Count; i++)
		{
			if (skills.Count > 1 && i < skills.Count - 1)
				skillText += "+" + (skills[i].amount * slider.value * (debuffMultiplier * debuffMultiplierAmount)).ToString() + " " + skills[i].type.ToString() + ", ";
			else
				skillText += "+" + (skills[i].amount * slider.value * (debuffMultiplier * debuffMultiplierAmount)).ToString() + " " + skills[i].type.ToString();
		}

		return skillText;
	}

	public void SetSliderMaxValue(ActivityManager.Activity activity, float divisor, float amount)
	{
		if (activity == ActivityManager.Activity.Sleep)
			slider.maxValue = Mathf.CeilToInt(amount / divisor);
		else
			slider.maxValue = Mathf.FloorToInt(amount / divisor);

		if (amount == 0) slider.maxValue = 0;
	}

	private void SetMultiplierText(float multiplier)
	{
		debuffMultiplierText.text = "debuff multiplier = " + multiplier + "x";
	}

	#region Getters & Setters

	public float GetDuration { get { return duration; } }
    public float GetTiredness { get { return tiredness; } }
    public float GetHunger { get { return hunger; } }
    public float GetThirst { get { return thirst; } }
    public float GetMoney { get { return money; } }
    public float GetMoneyMin { get { return moneyMin; } }
    public float GetMoneyMax { get { return moneyMax; } }
    public float GetRating { get { return rating; } }
    public float GetFame { get { return fame; } }

    public float GetTimeMultiplier { get { return timeMultiplier; } }
    public float GetIncomeMultiplier { get { return incomeMultiplier; } }
    public float GetTirednessMultiplier { get { return tirednessMultiplier; } }
    public string GetSkillName { get { return skillName; } }
	public List<Skill> GetSkills { get { return skills; } }

	public List<Skill> SetSkills { set { skills = value; } }
	public string SetSkillName { set { skillName = value; } }
	public float SetCostMultiplier { set { costMultiplier = value; } }
	public float SetIncomeMultiplier { set { incomeMultiplier = value; } }
	public float SetTirednessMultiplier { set { tirednessMultiplier = value; } }

	public float DebuffMultiplier { get { return debuffMultiplier; } set { debuffMultiplier = value; } }
    public float DebuffMultiplierAmount { get { return debuffMultiplierAmount; } set { debuffMultiplierAmount = value; } }
    
    #endregion
}
