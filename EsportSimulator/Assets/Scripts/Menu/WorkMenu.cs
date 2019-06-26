using PixelsoftGames.PixelUI;
using UnityEngine;
using UnityEngine.UI;

public class WorkMenu : MonoBehaviour
{
	[Header("References")]
	public GameManager gameManager;
	public UIExperienceBar expBar;
	public Text currentExpText;
	public Text currentLevelText;
	public Text currentPercentage;

	private void OnEnable()
	{
		Initialize();
	}

	public void Initialize()
	{
		expBar.Initialize();
		expBar.GiveExperiencePoints(gameManager.WorkExperience);
		currentExpText.text = gameManager.WorkExperience + " exp";
		currentLevelText.text = "lvl " + expBar.GetCurrentLevel.ToString();
		int percentage = (int)((double)expBar.GetExperienceTowardsLevel / (expBar.GetExperienceToNextLevel + expBar.GetExperienceTowardsLevel) * 100);
		currentPercentage.text = percentage + "%";
	}
}
