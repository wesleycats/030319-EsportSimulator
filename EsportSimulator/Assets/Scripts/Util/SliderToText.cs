using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderToText : MonoBehaviour
{
    public float timeMultiplier;
    public float costMultiplier;
    public float resultMultiplier;
    public float energyMultiplier;
    public string skillName;

    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.value = 0f;
    }

    public void ValueToTimeText(Text textToChange)
    {
        textToChange.text = slider.value.ToString() + " hours";
    }

    public void ValueToSleepTimeText(Text textToChange)
    {
        textToChange.text = (slider.value * timeMultiplier).ToString() + " hours";
    }

    public void ValueToSleepResult(Text textToChange)
    {
        textToChange.text = "+" + (slider.value * energyMultiplier).ToString() + "% energy";
    }

    public void ValueToWorkResult(Text textToChange)
    {
        textToChange.text = "+$" + (slider.value * resultMultiplier).ToString() + ", -" + (slider.value * energyMultiplier).ToString() + "% energy";
    }

    public void ValueToTrainFreeResult(Text textToChange)
    {
        textToChange.text = "+" + (slider.value * resultMultiplier).ToString() + " " + skillName + ", -" + (slider.value * energyMultiplier).ToString() + "% energy";
    }

    public void ValueToTrainResult(Text textToChange)
    {
        textToChange.text = "+" + (slider.value * resultMultiplier).ToString() + " " + skillName + ", -" + (slider.value * energyMultiplier).ToString() + "% energy, -$" + (slider.value * costMultiplier).ToString();
    }

	public void ValueToSreamResult(Text textToChange)
	{
		//TODO get fame amount

		textToChange.text = "between +$" + (slider.value * resultMultiplier).ToString() + "-$" + (slider.value * resultMultiplier * 3).ToString() + ", -" + (slider.value * energyMultiplier).ToString() + "% energy";
	}
}
