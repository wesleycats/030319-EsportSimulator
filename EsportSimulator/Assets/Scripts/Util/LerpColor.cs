using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpColor : MonoBehaviour
{
    [SerializeField] private bool lerpActivated;
    [Range(0, 1)]
    [SerializeField] private float lerpValue;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private bool lerpStop;
    [SerializeField] private bool lerpPause;
    [SerializeField] private float waitTime;
    [SerializeField] private int lerpMaxAmount;

    private bool lerping = true;
    private bool increasing = true;
    private float lerpSteps;
    private int timer;
    private int lerpAmount;

    public Color startColor;
    public Color endColor;
    public Image image;

    private void Start()
    {
        lerpActivated = false;
        lerping = true;
    }

    void Update()
    {
        if (!lerpActivated) return;

        if (lerpPause && !lerping)
        {
            timer++;

            if (timer % (waitTime * 60) == 0)
            {
                lerping = true;
                timer = 0;
            }
        }

        if (!lerping) return;

        lerpSteps = Time.deltaTime * lerpSpeed;

        if (lerpMaxAmount > 0)
            lerpPause = true;

        if (lerpAmount >= lerpMaxAmount-1)
        {
            lerpStop = true;
        }

        if (increasing)
            lerpValue += lerpSteps;
        else
            lerpValue -= lerpSteps;

        image.color = Color.Lerp(startColor, endColor, lerpValue);

        if (lerpValue > 1)
        {
            lerpAmount++;
            lerpValue = 1;
            increasing = false;

            if (lerpPause)
                lerping = false;

            if (lerpStop)
            {
                lerping = false;
                lerpPause = false;
                lerpStop = false;
                lerpAmount = 0;
            }
        }

        if (lerpValue < 0)
        {
            lerpAmount++;
            lerpValue = 0;
            increasing = true;

            if (lerpPause)
                lerping = false;

            if (lerpStop)
            {
                lerping = false;
                lerpPause = false;
                lerpStop = false;
                lerpAmount = 0;
            }
        }

    }

    #region Getters & Setters 

    public bool LerpActivated { get { return lerpActivated; } set { lerpActivated = value; } }
    public bool Lerp { get { return lerping; } set { lerping = value; } }
    public bool LerpPause { get { return lerpPause; } set { lerpPause = value; } }
    public bool LerpStop { get { return lerpStop; } set { lerpStop = value; } }
    public int LerpAmount { get { return lerpAmount; } set { lerpAmount = value; } }
    public int LerpMaxAmount { get { return lerpMaxAmount; } set { lerpMaxAmount = value; } }
    public bool Increasing { get { return increasing; } set { increasing = value; } }
    public float LerpValue { get { return lerpValue; } }

    #endregion
}
