using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpColor : MonoBehaviour
{
    [SerializeField] private bool lerpActivated;
    [SerializeField] private bool lerping = true;
    [Range(0, 1)]
    [SerializeField] private float lerpValue;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private bool lerpStop;
    [SerializeField] private bool lerpPause;
    [SerializeField] private float lerpPauseTime = 2f;
    [SerializeField] private int lerpMaxAmount;

    private bool increasing = true;
    private float lerpSteps;
    private int timer;
    private int lerpAmount;

    public Color startColor;
    public Color endColor;
    public Image image;
    
    void Update()
    {
        if (!lerpActivated) return;

        if (lerpPause && !lerping)
        {
            timer++;

            if (timer % (lerpPauseTime * 60) == 0)
            {
                lerping = true;
                timer = 0;
            }
        }

        if (!lerping) return;

        lerpSteps = Time.deltaTime * lerpSpeed;

        if (lerpMaxAmount > 0)
            lerpPause = true;

        if (lerpAmount >= lerpMaxAmount-1 && lerpMaxAmount != 0)
            lerpStop = true;

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
				lerpActivated = false;
				lerping = true;
                lerpPause = false;
                lerpStop = false;
                lerpAmount = 0;
            }
        }
    }

    public bool isLerping()
    {
        return lerping;
    }

    #region Getters & Setters 

    public bool LerpActivated { get { return lerpActivated; } set { lerpActivated = value; } }
    public bool Lerping { get { return lerping; } set { lerping = value; } }
    public bool LerpPause { get { return lerpPause; } set { lerpPause = value; } }
    public float LerpPauseTime { get { return lerpPauseTime; } set { lerpPauseTime = value; } }
    public bool LerpStop { get { return lerpStop; } set { lerpStop = value; } }
    public int LerpAmount { get { return lerpAmount; } set { lerpAmount = value; } }
    public int LerpMaxAmount { get { return lerpMaxAmount; } set { lerpMaxAmount = value; } }
    public bool Increasing { get { return increasing; } set { increasing = value; } }
    public float LerpValue { get { return lerpValue; } }

    #endregion
}
