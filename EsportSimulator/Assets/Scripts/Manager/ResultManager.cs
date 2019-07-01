using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the results of most actions
/// </summary>
public class ResultManager : MonoBehaviour
{
	#region Forms

	[Header("Work")]
	[Tooltip("Results per hour")]
	[SerializeField] private ResultsForm workLevel1;
	[SerializeField] private ResultsForm workLevel2;
	[SerializeField] private ResultsForm workLevel3;
	[SerializeField] private ResultsForm workLevel4;

	[Header("Default Training")]
	[Tooltip("Results per hour")]
	[SerializeField] private ResultsForm trainLevel1;
	[SerializeField] private ResultsForm trainLevel2;
	[SerializeField] private ResultsForm trainLevel3;

	[Header("Battles")]
	[SerializeField] private ResultsForm oneVsOne;
	[SerializeField] private ResultsForm threeVsThree;
	[SerializeField] private ResultsForm fiveVsFive;

	[Header("Food")]
	[Tooltip("Results per purchase")]
	[SerializeField] private ResultsForm foodBad;
	[SerializeField] private ResultsForm foodStandard;
	[SerializeField] private ResultsForm foodGood;
	[SerializeField] private ResultsForm foodExcellent;

	[Header("Drinks")]
	[Tooltip("Results per purchase")]
	[SerializeField] private ResultsForm drinkBad;
	[SerializeField] private ResultsForm drinkStandard;
	[SerializeField] private ResultsForm drinkGood;
	[SerializeField] private ResultsForm drinkExcellent;

	#endregion

	[Header("Needs")]
	// How much tiredness you lose by sleeping
	[SerializeField] private int tirednessDecreaseRate;

	// How much hunger you get per hour
	[SerializeField] private int hungerIncreaseRate;

	// How much thirst you get per hour
	[SerializeField] private int thirstIncreaseRate;

	[Header("Streaming")]
	[SerializeField] private int minimalFameForViews;
	[SerializeField] private int minViewsPerFame;
	[SerializeField] private int maxViewsPerFame;
	[SerializeField] private int minimalViewsForIncome;
	[SerializeField] private int minIncomePerViews;
	[SerializeField] private int maxIncomePerViews;
	[SerializeField] private int totalViews;
	[SerializeField] private int totalIncome;
	public int streamEnergyCost;

	[Header("Battle")]
	[Tooltip("Bias to influence battle result")]
	[SerializeField] private int playerBattleBias;
	[Tooltip("Percentage to win given per point")]
	[SerializeField] private float pointPercentage;

	private Battle battle;
	private ResultsForm battleResults;
	private Opponent opponent1;
	private Opponent opponent2;

	[Header("References")]
	public UIManager uiManager;
	public GameManager gameManager;
	public LeaderboardManager lbManager;
	public OpponentManager opponentManager;
	public ShopManager shopManager;
	public ContestManager contestManager;
	public TimeManager timeManager;
	public ActivityManager activityManager;
	public PlayerData playerData;

	public void Eat(int foodLevel)
	{
		//TODO combine with Drink()

		if (gameManager.Hunger <= 0)
		{
			Debug.LogWarning("You are currently full, you can buy food when you are hungry");
			return;
		}

		int cost = 0;
		int hunger = 0;

		switch (foodLevel)
		{
			case 0:
				cost = foodBad.Money;
				hunger = foodBad.Hunger;
				break;

			case 1:
				cost = foodStandard.Money;
				hunger = foodStandard.Hunger;
				break;

			case 2:
				cost = foodGood.Money;
				hunger = foodGood.Hunger;
				break;

			case 3:
				cost = foodExcellent.Money;
				hunger = foodExcellent.Hunger;
				break;

			default:
				Debug.LogError("No food level has been given");
				return;
		}

		if (!gameManager.IsMoneyHighEnough(cost))
		{
			Debug.LogWarning("Not Enough Money");
			return;
		}

		gameManager.DecreaseHunger(hunger);
		gameManager.DecreaseMoney(cost);

		uiManager.UpdateProgress(gameManager.GetMoney, gameManager.Rating, gameManager.Fame);
		uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
	}

	public void Drink(int drinkLevel)
	{
		//TODO combine with Eat()

		if (gameManager.Thirst <= 0)
		{
			Debug.LogWarning("You are currently hydrated, you can buy drinks when you are thirsty");
			return;
		}

		int cost = 0;
		int thirst = 0;

		switch (drinkLevel)
		{
			case 0:
				cost = drinkBad.Money;
				thirst = drinkBad.Thirst;
				break;

			case 1:
				cost = drinkStandard.Money;
				thirst = drinkStandard.Thirst;
				break;

			case 2:
				cost = drinkGood.Money;
				thirst = drinkGood.Thirst;
				break;

			case 3:
				cost = drinkExcellent.Money;
				thirst = drinkExcellent.Thirst;
				break;

			default:
				Debug.LogError("No food level has been given");
				return;
		}

		if (!gameManager.IsMoneyHighEnough(cost))
		{
			Debug.LogWarning("Not Enough Money");
			return;
		}

		gameManager.DecreaseThirst(thirst);
		gameManager.DecreaseMoney(cost);

		uiManager.UpdateProgress(gameManager.GetMoney, gameManager.Rating, gameManager.Fame);
		uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
	}

	/// <summary>
	/// Applies the results given by sleeping per hour, based on tiredness decrease rate
	/// </summary>
	public void SleepResults(int tirednessDecreaseRate)
	{
		gameManager.DecreaseTiredness(tirednessDecreaseRate);
	}

	/// <summary>
	/// Applies the results given by training per hour, based on training type and accommodation level
	/// </summary>
	/// <param name="trainType"></param>
	public void TrainResults(Training training, int totalDuration, Skill.Type skill)
	{
		int tirednessAmount = 0;

		tirednessAmount = (int)(GetTrainingTirednessAmount(training.type, totalDuration) / totalDuration);

		switch (skill)
		{
			case Skill.Type.GameKnowledge:
				gameManager.IncreaseGameKnowledge(GetTrainingRate(training.type));
				break;

			case Skill.Type.TeamPlay:
				gameManager.IncreaseTeamPlay(GetTrainingRate(training.type));
				break;

			case Skill.Type.Mechanics:
				gameManager.IncreaseMechanics(GetTrainingRate(training.type));
				break;
			default:
				Debug.LogWarning("No skill is given");
				return;
		}

		gameManager.IncreaseHunger(hungerIncreaseRate);
		gameManager.IncreaseThirst(thirstIncreaseRate);
		gameManager.IncreaseTiredness(tirednessAmount);
	}

	/// <summary>
	/// Applies the results given by working per hour, based on work level
	/// </summary>
	/// <param name="workLevel"></param>
	public void WorkResults(int workLevel)
	{
		ResultsForm workResults = GetWorkResultForm(workLevel);

		gameManager.IncreaseMoney(workResults.Money);
		gameManager.IncreaseWorkExp(workResults.WorkExperience);
		gameManager.IncreaseGameKnowledge(workResults.GameKnowledge);
		gameManager.IncreaseTeamPlay(workResults.TeamPlay);
		gameManager.IncreaseMechanics(workResults.Mechanics);

		gameManager.IncreaseTiredness(workResults.Tiredness);
		gameManager.IncreaseHunger(hungerIncreaseRate);
		gameManager.IncreaseThirst(thirstIncreaseRate);
	}

	/// <summary>
	/// Applies the results given by battling per hour, based on battle mode and outcome of the battle
	/// </summary>
	/// <param name="opponent"></param>
	/// <param name="battleMode"></param>
	public void BattleResult(Battle.Mode mode)
	{
		opponent1 = lbManager.GetPlayer;
		opponent2 = lbManager.GetRandomOpponent(opponent1);
		battle = new Battle(mode, pointPercentage, playerBattleBias);

		Opponent winner = battle.Between(opponent1, opponent2);

		uiManager.battleMenu.SetActive(true);
		uiManager.UpdateBattleStats(opponent1, opponent2, battle.mode, (int)battle.winPercentage);

		int gameKnowledgeReward = 0;
		int teamPlayReward = 0;
		int mechanicsReward = 0;
		int eloReward = 0;
		int fameReward = 0;
		int tiredness = 0;

		switch (mode)
		{
			case Battle.Mode.OneVersusOne:
				gameKnowledgeReward = oneVsOne.GameKnowledge;
				teamPlayReward = oneVsOne.TeamPlay;
				mechanicsReward = oneVsOne.Mechanics;
				tiredness = oneVsOne.Tiredness;

				if (winner == opponent1)
				{
					eloReward = oneVsOne.Rating;
					fameReward = oneVsOne.Fame;
				}
				else
				{
					eloReward = -oneVsOne.Rating;
					fameReward = -oneVsOne.Fame;
				}
				break;

			case Battle.Mode.ThreeVersusThree:
				gameKnowledgeReward = threeVsThree.GameKnowledge;
				teamPlayReward = threeVsThree.TeamPlay;
				mechanicsReward = threeVsThree.Mechanics;
				tiredness = threeVsThree.Tiredness;

				if (winner == opponent1)
				{
					eloReward = threeVsThree.Rating;
					fameReward = threeVsThree.Fame;
				}
				else
				{
					eloReward = -threeVsThree.Rating;
					fameReward = -threeVsThree.Fame;
				}
				break;

			case Battle.Mode.FiveVersusFive:
				gameKnowledgeReward = fiveVsFive.GameKnowledge;
				teamPlayReward = fiveVsFive.TeamPlay;
				mechanicsReward = fiveVsFive.Mechanics;
				tiredness = fiveVsFive.Tiredness;

				if (winner == opponent1)
				{
					eloReward = fiveVsFive.Rating;
					fameReward = fiveVsFive.Fame;
				} 
				else
				{
					eloReward = -fiveVsFive.Rating;
					fameReward = -fiveVsFive.Fame;
				}

				break;
		}

		battleResults = new ResultsForm();
		battleResults.GameKnowledge = gameKnowledgeReward;
		battleResults.Mechanics = mechanicsReward;
		battleResults.TeamPlay = teamPlayReward;
		battleResults.Tiredness = tiredness;
		battleResults.Rating = eloReward;
		battleResults.Fame = fameReward;
	}

	public void ApplyBattleResult()
	{
		if (activityManager.currentActivity != ActivityManager.Activity.Battle && activityManager.currentActivity != ActivityManager.Activity.Idle) return;

		Opponent winner = battle.Between(opponent1, opponent2);
		bool playerWin;

		if (winner == opponent1)
			playerWin = true;
		else
			playerWin = false;

		uiManager.battleMenu.SetActive(false);
		uiManager.ActivateBattleResult(playerWin);

		gameManager.IncreaseGameKnowledge(battleResults.GameKnowledge);
		gameManager.IncreaseMechanics(battleResults.Mechanics);
		gameManager.IncreaseTeamPlay(battleResults.TeamPlay);
		gameManager.IncreaseTiredness(battleResults.Tiredness);
		gameManager.IncreaseRating(battleResults.Rating);
		gameManager.IncreaseFame(battleResults.Fame);

		lbManager.SortLeaderboard();

		ResetBattleStats();
	}

	public void ResetBattleStats()
	{
		opponent1 = null;
		opponent2 = null;
		battleResults = null;
		timeManager.battleStarted = false;
	}

	/// <summary>
	/// Applies the results given by streaming per hour, based on the amount of fame and views
	/// </summary>
	/// <param name="fame"></param>
	public void StreamResults(int fame)
	{
		if (fame < minimalFameForViews) return;

		int fameCount = fame / minimalFameForViews;
		totalViews += GetStreamViews(fameCount, minViewsPerFame, maxViewsPerFame);

		int viewCount = totalViews / minimalViewsForIncome;
		int money = GetStreamIncome(viewCount, minIncomePerViews, maxIncomePerViews);

		totalIncome += money;
		gameManager.IncreaseMoney(money);
		gameManager.IncreaseTiredness(streamEnergyCost);
		gameManager.IncreaseHunger(hungerIncreaseRate);
		gameManager.IncreaseThirst(thirstIncreaseRate);
	}
	
	public void PayRent(Accommodation accommodation)
	{
		if (gameManager.IsMoneyHighEnough(accommodation.rent))
			gameManager.DecreaseMoney(accommodation.rent);
		else
		{
			gameManager.GameOver();
			FindObjectOfType<ButtonManager>().EnableAllButtonsOf("GameOver");
		}
	}

	public void ResetTotalViews()
	{
		totalIncome = 0;
		totalViews = 0;
	}

	static int SortByPlacement(Opponent o1, Opponent o2)
	{
		return o1.placement.CompareTo(o2.placement);
	}

	public int GetMinStreamIncome(int fame, int duration)
	{
		int views = 0;
		int incomeAmount = 0;
		int fameCount = fame / minimalFameForViews;

		for (int i = 0; i < fameCount * duration; i++)
		{
			views += minViewsPerFame;
		}

		int viewCount = views / minimalViewsForIncome;


		for (int i = 0; i < viewCount * duration; i++)
		{
			incomeAmount += minIncomePerViews;
		}

		return incomeAmount;
	}

	public int GetMaxStreamIncome(int fame, int duration)
	{
		int views = 0;
		int incomeAmount = 0;
		int fameCount = fame / minimalFameForViews;

		for (int i = 0; i < fameCount * duration; i++)
		{
			views += minViewsPerFame;
		}

		int viewCount = views / minimalViewsForIncome;


		for (int i = 0; i < viewCount * duration; i++)
		{
			incomeAmount += maxIncomePerViews;
		}

		return incomeAmount;
	}

	/// <summary>
	/// Returns a random amount of views the player gets per hour based on amount of fame
	/// </summary>
	/// <param name="fameCount"></param>
	/// <param name="minViewsPerFame"></param>
	/// <param name="maxViewsPerFame"></param>
	/// <returns></returns>
	public int GetStreamViews(int fameCount, int minViewsPerFame, int maxViewsPerFame)
	{
		int views = 0;
		for (int i = 0; i < fameCount; i++)
		{
			views += Random.Range(minViewsPerFame, maxViewsPerFame);
		}
		return views;
	}

	/// <summary>
	/// Returns a random amount of money the player gets per hour based on amount of views
	/// </summary>
	/// <param name="viewCount"></param>
	/// <param name="minIncomePerViews"></param>
	/// <param name="maxIncomePerViews"></param>
	/// <returns></returns>
	public int GetStreamIncome(int viewCount, int minIncomePerViews, int maxIncomePerViews)
	{
		int incomeAmount = 0;
		for (int i = 0; i < viewCount; i++)
		{
			incomeAmount += Random.Range(minIncomePerViews, maxIncomePerViews);
		}

		return incomeAmount;
	}

	public ResultsForm GetWorkResultForm(int workLevel)
	{
		switch (workLevel)
		{
			case 0:
				return workLevel1;

			case 1:
				return workLevel2;

			case 2:
				return workLevel3;

			case 3:
				return workLevel4;

			default:
				Debug.LogError("Given work level is unavailable");
				return null;
		}
	}

	public ResultsForm GetTrainingResultsForm(Training.Type type)
	{
		switch (type)
		{
			case Training.Type.Watching:
				return trainLevel1;

			case Training.Type.Course:
				return trainLevel2;

			case Training.Type.CoursePlus:
				return trainLevel3;

			default:
				Debug.LogWarning("Given training type is unavailable");
				return null;
		}
	}

	public float GetTrainingCostAmount(Training.Type trainType, int duration)
	{
		switch (trainType)
		{
			case Training.Type.Watching:
				return trainLevel1.Money * duration;

			case Training.Type.Course:
				return trainLevel2.Money * duration;

			case Training.Type.CoursePlus:
				return trainLevel3.Money * duration;

			default:
				Debug.LogWarning("No training type is given");
				return 0;
		}
	}

	public float GetTrainingTirednessAmount(Training.Type trainType, int duration)
	{
		switch (trainType)
		{
			case Training.Type.Watching:
				return trainLevel1.Tiredness * duration;

			case Training.Type.Course:
				return trainLevel2.Tiredness * duration;

			case Training.Type.CoursePlus:
				return trainLevel3.Tiredness * duration;

			default:
				Debug.LogWarning("No training type is given");
				return 0;
		}
	}

	public int GetRealRandom(int min, int max)
	{
		int randomNumber = Random.Range(min, max);
		int oldNumber = randomNumber;

		while (oldNumber == randomNumber)
			randomNumber = Random.Range(min, max);

		return randomNumber;
	}

	public int GetTrainingRate(Training.Type trainType)
	{
		switch (trainType)
		{
			case Training.Type.Watching:
				return gameManager.CurrentAccommodation.trainingRates[0];

			case Training.Type.Course:
				return gameManager.CurrentAccommodation.trainingRates[1];

			case Training.Type.CoursePlus:
				return gameManager.CurrentAccommodation.trainingRates[2];

			default:
				Debug.LogWarning("Given training type is unavailable");
				return 0;
		}
	}

	#region Getters & Setters

	public int GetTirednessDecreaseRate { get { return tirednessDecreaseRate; } }
	public int GetTotalViews { get { return totalViews; } }
	public ResultsForm GetTrainingLevel1Results { get { return trainLevel1; } }
	public ResultsForm GetTrainingLevel2Results { get { return trainLevel2; } }
	public ResultsForm GetTrainingLevel3Results { get { return trainLevel3; } }

	#endregion
}
