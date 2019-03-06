using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderToText : MonoBehaviour
{
    public float timeMultiplier;
    public float resultMultiplier;

    private void Start()
    {
        GetComponent<Slider>().value = 0f;
    }

    public void ValueToTimeText(Text textToChange)
    {
        textToChange.text = GetComponent<Slider>().value.ToString() + " hours";
    }

    public void ValueToSleepTimeText(Text textToChange)
    {
        textToChange.text = (GetComponent<Slider>().value * timeMultiplier).ToString() + " hours";
    }

    public void ValueToSleepResult(Text textToChange)
    {
        textToChange.text = "+" + (GetComponent<Slider>().value * resultMultiplier).ToString() + "% energy";
    }

    public void ValueToWorkResult(Text textToChange)
    {
        textToChange.text = "+$" + (GetComponent<Slider>().value * resultMultiplier).ToString() + ", -" + (GetComponent<Slider>().value * resultMultiplier).ToString() + " energy";
    }
}
