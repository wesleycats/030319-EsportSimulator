using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderToText : MonoBehaviour
{
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float costMultiplier;
    [SerializeField] private float resultMultiplier;
    [SerializeField] private float tirednessMultiplier;
    [SerializeField] private string skillName;
    [SerializeField] private bool sleep;

    private float duration;
    private float tiredness;
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

    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        SetSliderMaxValue(tirednessMultiplier);
        ResetValues();
    }

    public void ValueToTimeText(Text textToChange)
    {
        duration = slider.value * timeMultiplier;
        textToChange.text = duration.ToString() + " hours";
    }

    public void ValueToSleepResult(Text textToChange)
    {
        tiredness = slider.value * tirednessMultiplier;
        textToChange.text = "-" + tiredness.ToString() + "% tiredness";
    }

    public void ValueToWorkResult(Text textToChange)
    {
        tiredness = slider.value * tirednessMultiplier;
        money = slider.value * resultMultiplier * (debuffMultiplier * debuffMultiplierAmount);
        textToChange.text = "+$" + money.ToString() + ", -" + tiredness.ToString() + "% tiredness";
    }

    public void ValueToTrainFreeResult(Text textToChange)
    {
        tiredness = slider.value * tirednessMultiplier;
        skill = slider.value * resultMultiplier * (debuffMultiplier * debuffMultiplierAmount);
        textToChange.text = "+" + skill.ToString() + " " + skillName + ", -" + tiredness.ToString() + "% tiredness";
    }

    public void ValueToTrainResult(Text textToChange)
    {
        tiredness = slider.value * tirednessMultiplier;
        money = slider.value * costMultiplier;
        skill = slider.value * resultMultiplier * (debuffMultiplier * debuffMultiplierAmount);
        textToChange.text = "+" + skill.ToString() + " " + skillName + ", -" + tiredness.ToString() + "% tiredness, -$" + money.ToString();
    }

    public void ValueToSreamResult(Text textToChange)
    {
        //TODO get fame amount
        tiredness = slider.value * tirednessMultiplier;
        moneyMin = slider.value * resultMultiplier * (debuffMultiplier * debuffMultiplierAmount);
        moneyMax = slider.value * (resultMultiplier * 3) * (debuffMultiplier * debuffMultiplierAmount);
        textToChange.text = "between +$" + moneyMin.ToString() + "-$" + moneyMax.ToString() + ", -" + tiredness.ToString() + "% tiredness";
    }

    public void SetSliderMaxValue(float divisor)
    {
        slider.maxValue = Mathf.Floor(100 / divisor);
        slider.value = slider.maxValue;
    }
    
    private void OnDisable()
    {
        ResetValues();
    }

    private void ResetValues()
    {
        slider.value = 0f;
        tiredness = 0f;
        hunger = 0f;
        thirst = 0f;
        money = 0f;
        rating = 0f;
        fame = 0f;
        skill = 0f;
    }

    #region Getters & Setters

    public float Duration { get { return duration; } }
    public float Tiredness { get { return tiredness; } }
    public float Hunger { get { return hunger; } }
    public float Thirst { get { return thirst; } }
    public float Money { get { return money; } }
    public float MoneyMin { get { return moneyMin; } }
    public float MoneyMax { get { return moneyMax; } }
    public float Rating { get { return rating; } }
    public float Fame { get { return fame; } }

    public float TimeMultiplier { get { return timeMultiplier; } }
    public float CostMultiplier { get { return costMultiplier; } }
    public float ResultMultiplier { get { return resultMultiplier; } }
    public float TirednessMultiplier { get { return tirednessMultiplier; } }
    public string SkillName { get { return skillName; } }

    public float DebuffMultiplier { get { return debuffMultiplier; } set { debuffMultiplier = value; } }
    public float DebuffMultiplierAmount { get { return debuffMultiplierAmount; } set { debuffMultiplierAmount = value; } }
    
    #endregion
}
