using System;
using UnityEngine;
using UnityEngine.UI;

public class LerpColor : MonoBehaviour
{
	public Action<bool> LerpStopped;

	[SerializeField] private bool lerpActivated;
    [SerializeField] private bool lerping = true;
    [Range(0, 1)]
    [SerializeField] private float lerpValue;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private bool lerpStop;
    [SerializeField] private bool lerpPause;
    [SerializeField] private float lerpPauseTime = 2f;
    [SerializeField] private int lerpMaxAmount = 2;
	[SerializeField] private bool paused = false;

    private bool increasing = true;
    private float lerpSteps;
    [SerializeField] private int timer;
	[SerializeField] private int lerpAmount;

    public Color startColor;
    public Color endColor;
    public Image image;

	/// <summary>
	/// Initiates lerping functionality
	/// </summary>
	/// <param name="amount"></param>
	public void Lerp(int amount)
	{
		lerpActivated = true;
		lerping = true;
		paused = false;
		lerpMaxAmount = amount;

		if (lerpValue >= 1)
			increasing = false;
		else
			increasing = true;

		lerping = true;
	}
    
    void Update()
    {
        if (!lerpActivated) return;

		// Activates pause timer
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

        if (increasing)
            lerpValue += lerpSteps;
        else
            lerpValue -= lerpSteps;

        image.color = Color.Lerp(startColor, endColor, lerpValue);

		if (lerpValue < 0 || lerpValue > 1)
		{
			lerpAmount += 1;

			if (lerpValue > 1)
			{
				lerpValue = 1;
				increasing = false;
			}
			else
			{
				lerpValue = 0;
				increasing = true;
			}

			if (lerpMaxAmount > 0)
				lerpPause = true;

			if (lerpPause)
			{
				lerping = false;
				paused = true;
			}

			if (lerpAmount >= lerpMaxAmount)
				StopLerp();
		}
    }

	private void StopLerp()
	{
		lerpActivated = false;
		lerping = false;
		lerpPause = false;
		lerpStop = false;
		paused = false;
		lerpAmount = 0;

		if (LerpStopped == null) return;

		LerpStopped(true);
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
    public float LerpValue { get { return lerpValue; } set { lerpValue = value; } }
	public bool isPaused { get { return paused; } }

    #endregion
}
