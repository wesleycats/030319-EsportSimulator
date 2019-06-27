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

	[SerializeField] private bool hungerDebuff = false;
	[SerializeField] private float hungerDebuffAddition = 0.5f;
	[SerializeField] private bool thirstDebuff = false;
	[SerializeField] private float thirstDebuffAddition = 0.5f;

	[SerializeField] private int winBiasPercentage;
	[SerializeField] private float winPercentagePerSkillPoint;

	[Header("Needs")]
	[SerializeField] private int maxSleepHours = 12;
	// How much tiredness you lose by sleeping
	[SerializeField] private int tirednessDecreaseRate = 25;

	// How much hunger you get per hour
	[SerializeField] private int hungerIncreaseRate = 10;

	// How much thirst you get per hour
	[SerializeField] private int thirstIncreaseRate = 5;

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

	[Header("References")]
	public UIManager uiManager;
	public GameManager gameManager;
	public LeaderboardManager lbManager;
	public OpponentManager opponentManager;
	public ShopManager shopManager;
	public ContestManager contestManager;
	public TimeManager timeManager;
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

		gameManager.IncreaseTiredness(tirednessAmount);
		gameManager.IncreaseHunger(hungerIncreaseRate);
		gameManager.IncreaseThirst(thirstIncreaseRate);
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
	public void BattleResults(Opponent opponent, Battle.Mode battleMode)
	{
		int randomizer = 0;
		int winChance = 0;
		int gkDelta = gameManager.GetGameKnowledge - opponent.gameKnowledge;
		int mDelta = gameManager.GetMechanics - opponent.mechanics;
		int tpDelta = gameManager.GetTeamPlay - opponent.teamPlay;
		int[] skillsDelta = new int[0];
		bool win = false;

		int gameKnowledgeReward = 0;
		int teamPlayReward = 0;
		int mechanicsReward = 0;
		int tiredness = 0;

		switch (battleMode)
		{
			case Battle.Mode.OneVersusOne:
				randomizer = Random.Range(0, 100);
				skillsDelta = new int[2];
				skillsDelta[0] = gkDelta;
				skillsDelta[1] = mDelta;
				winChance = GetWinChance(skillsDelta, winPercentagePerSkillPoint) + winBiasPercentage;

				if (randomizer < winChance || win)
					win = true;
				else
					win = false;

				if (win)
				{
					gameManager.IncreaseFame(oneVsOne.Fame);
					gameManager.IncreaseRating(oneVsOne.Rating);
				}
				else
				{
					gameManager.DecreaseFame(oneVsOne.Fame);
					gameManager.DecreaseRating(oneVsOne.Rating);
				}

				gameKnowledgeReward = oneVsOne.GameKnowledge;
				teamPlayReward = oneVsOne.TeamPlay;
				mechanicsReward = oneVsOne.Mechanics;
				tiredness = oneVsOne.Tiredness;
				break;

			case Battle.Mode.ThreeVersusThree:
				randomizer = Random.Range(0, 100);
				skillsDelta = new int[3];
				skillsDelta[0] = gkDelta;
				skillsDelta[1] = mDelta;
				skillsDelta[2] = tpDelta;
				winChance = GetWinChance(skillsDelta, winPercentagePerSkillPoint) + winBiasPercentage;

				if (randomizer < winChance || win)
					win = true;
				else
					win = false;

				if (win)
				{
					gameManager.IncreaseFame(threeVsThree.Fame);
					gameManager.IncreaseRating(threeVsThree.Rating);
				}
				else
				{
					gameManager.DecreaseFame(threeVsThree.Fame);
					gameManager.DecreaseRating(threeVsThree.Rating);
				}

				gameKnowledgeReward = threeVsThree.GameKnowledge;
				teamPlayReward = threeVsThree.TeamPlay;
				mechanicsReward = threeVsThree.Mechanics;
				tiredness = threeVsThree.Tiredness;
				break;

			case Battle.Mode.FiveVersusFive:
				randomizer = Random.Range(0, 100);
				skillsDelta = new int[3];
				skillsDelta[0] = gkDelta;
				skillsDelta[1] = mDelta;
				skillsDelta[2] = tpDelta;
				winChance = GetWinChance(skillsDelta, winPercentagePerSkillPoint) + winBiasPercentage;

				//Debug.Log("Player gk: " + gameManager.GetGameKnowledge + " Player m: " + gameManager.GetMechanics + " Player tp: " + gameManager.GetTeamPlay);
				//Debug.Log(opponent.name + " gk: " + opponent.gameKnowledge + " Opponent m: " + opponent.mechanics + " Opponent tp: " + opponent.teamPlay);
				//Debug.Log("Win chance + bias= " + winChance);
				//Debug.Log("randomizer= " + randomizer);

				if (randomizer < winChance || win)
					win = true;
				else
					win = false;

				//Debug.Log("Win=" + win);
				
				if (win)
				{
					gameManager.IncreaseFame(fiveVsFive.Fame);
					gameManager.IncreaseRating(fiveVsFive.Rating);
				}
				else
				{
					gameManager.DecreaseFame(fiveVsFive.Fame);
					gameManager.DecreaseRating(fiveVsFive.Rating);
				}

				gameKnowledgeReward = fiveVsFive.GameKnowledge;
				teamPlayReward = fiveVsFive.TeamPlay;
				mechanicsReward = fiveVsFive.Mechanics;
				tiredness = fiveVsFive.Tiredness;
				break;
		}

		gameManager.IncreaseGameKnowledge(gameKnowledgeReward);
		gameManager.IncreaseMechanics(mechanicsReward);
		gameManager.IncreaseTeamPlay(teamPlayReward);
		gameManager.IncreaseTiredness((int)(tiredness * GetDebuffMultiplier(gameManager.Hunger, gameManager.Thirst)));

		lbManager.SortLeaderboard();
	}

	/// <summary>
	/// Applies the results given by streaming per hour, based on the amount of fame and views
	/// </summary>
	/// <param name="fame"></param>
	public void StreamResults(int fame)
	{
		//TODO apply debuffs

		if (fame < minimalFameForViews) return;

		int fameCount = fame / minimalFameForViews;
		totalViews += GetStreamViews(fameCount, minViewsPerFame, maxViewsPerFame);

		int viewCount = totalViews / minimalViewsForIncome;
		int money = GetStreamIncome(viewCount, minIncomePerViews, maxIncomePerViews);

		gameManager.IncreaseMoney(money);
		gameManager.IncreaseTiredness(streamEnergyCost);
		totalIncome += money;
	}

	/// <summary>
	/// Applies the results given by contesting per hour, based on type of contest, current opponent,
	/// battle outcome and achieved position
	/// </summary>
	/// <param name="contest"></param>
	/// <param name="participants"></param>
	/// <returns></returns>
	public IEnumerator ContestResults(Event contest, List<Opponent> participants)
	{
		uiManager.battleMenu.SetActive(true);
		timeManager.SetPause = true;
		contestManager.IncreaseCurrentBattle();

		Opponent player = participants[participants.IndexOf(lbManager.GetPlayer)];
		Opponent opponent = participants[participants.IndexOf(player) - 1];

		bool win = false;
		int winChance = 0;
		int randomizer = 0;
		int[] skillsDelta = new int[0];
		int gkDelta = gameManager.GetGameKnowledge - opponent.gameKnowledge;
		int mDelta = gameManager.GetMechanics - opponent.mechanics;
		int tpDelta = gameManager.GetTeamPlay - opponent.teamPlay;

		randomizer = GetRealRandom(0, 100);

		//Debug.Log(opponent.name);
		//Debug.Log("randomizer = " + randomizer);

		switch (contest.battleMode)
		{
			case Battle.Mode.OneVersusOne:
				skillsDelta = new int[2];
				skillsDelta[0] = gkDelta;
				skillsDelta[1] = mDelta;
				break;

			case Battle.Mode.ThreeVersusThree:
				skillsDelta = new int[2];
				skillsDelta[0] = gkDelta;
				skillsDelta[1] = tpDelta;
				break;

			case Battle.Mode.FiveVersusFive:
				skillsDelta = new int[3];
				skillsDelta[0] = gkDelta;
				skillsDelta[1] = mDelta;
				skillsDelta[1] = tpDelta;
				break;
		}

		winChance = GetWinChance(skillsDelta, winPercentagePerSkillPoint) + winBiasPercentage;

		//Debug.Log("Win chance + bias = " + winChance);

		if (winChance > 100) winChance = 100;
		if (winChance < 0) winChance = 0;

		uiManager.UpdateCompetitorStats(player, opponent, contest.battleMode, winChance);

		yield return new WaitForSeconds(contestManager.GetBattleDuration);

		// Win battle
		if (randomizer < winChance)
		{
			SwapPlacement(participants, player, opponent);

			//Debug.Log("WIN BATTLE");

			// Win tournament
			if (participants.IndexOf(player) == 0)
			{
				contestManager.EndContest(player, contest);
				win = true;
				//Debug.Log("WIN Tournament");
			}

			if (!win)
			{
				if (contestManager.GetCurrentBattle != contestManager.GetBattlesPerHour)
				{
					StartCoroutine(ContestResults(contest, participants));
				}
				else
				{
					timeManager.SetPause = false;
					contestManager.ResetCurrentBattle();
				}
			}
		}
		else
		{
			// Lose battle & tournament

			// Adds 1 to get real placement instead index
			player.placement += 1;
			contestManager.EndContest(player, contest);
		}
	}

	public void TryBattle(Battle.Mode battleType)
	{
		BattleResults(lbManager.GetRandomOpponent(), battleType);
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
		ResultsForm results = new ResultsForm();

		switch (workLevel)
		{
			case 0:
				results.SetMoney = (int)(workLevel1.Money);
				results.WorkExperience = workLevel1.WorkExperience;
				results.SetGameKnowledge = (int)(workLevel1.GameKnowledge);
				results.SetTeamPlay = (int)(workLevel1.TeamPlay);
				results.SetMechanics = (int)(workLevel1.Mechanics);
				results.SetTiredness = (int)((workLevel1.Tiredness) * GetDebuffMultiplier(gameManager.Hunger, gameManager.Thirst));
				break;

			case 1:
				results.SetMoney = (int)(workLevel2.Money);
				results.WorkExperience = workLevel2.WorkExperience;
				results.SetGameKnowledge = (int)(workLevel2.GameKnowledge);
				results.SetTeamPlay = (int)(workLevel2.TeamPlay);
				results.SetMechanics = (int)(workLevel2.Mechanics);
				results.SetTiredness = (int)((workLevel2.Tiredness) * GetDebuffMultiplier(gameManager.Hunger, gameManager.Thirst));
				break;

			case 2:
				results.SetMoney = (int)(workLevel3.Money);
				results.WorkExperience = workLevel3.WorkExperience;
				results.SetGameKnowledge = (int)(workLevel3.GameKnowledge);
				results.SetTeamPlay = (int)(workLevel3.TeamPlay);
				results.SetMechanics = (int)(workLevel3.Mechanics);
				results.SetTiredness = (int)((workLevel3.Tiredness) * GetDebuffMultiplier(gameManager.Hunger, gameManager.Thirst));
				break;

			case 3:
				results.SetMoney = (int)(workLevel4.Money);
				results.WorkExperience = workLevel4.WorkExperience;
				results.SetGameKnowledge = (int)(workLevel4.GameKnowledge);
				results.SetTeamPlay = (int)(workLevel4.TeamPlay);
				results.SetMechanics = (int)(workLevel4.Mechanics);
				results.SetTiredness = (int)((workLevel4.Tiredness) * GetDebuffMultiplier(gameManager.Hunger, gameManager.Thirst));
				break;

			default:
				Debug.LogError("No work level has been given");
				break;
		}

		return results;
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

	/// <summary>
	/// Checks the delta of the skills between player and opponent and returns win percentage
	/// </summary>
	/// <param name="skills"></param>
	/// <returns></returns>
	private int GetWinChance(int[] skillsDelta, float percentagePerPoint)
    {
        float winChance = 0;

        foreach (int s in skillsDelta)
        {
			//Debug.Log("skill delta " + s);
			winChance += s * percentagePerPoint;
        }

        return (int)winChance;
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
				return trainLevel1.Tiredness * GetDebuffMultiplier(gameManager.Hunger, gameManager.Thirst) * duration;

			case Training.Type.Course:
				return trainLevel2.Tiredness * GetDebuffMultiplier(gameManager.Hunger, gameManager.Thirst) * duration;

			case Training.Type.CoursePlus:
				return trainLevel3.Tiredness * GetDebuffMultiplier(gameManager.Hunger, gameManager.Thirst) * duration;

			default:
				Debug.LogWarning("No training type is given");
				return 0;
		}
	}

	private int GetRealRandom(int min, int max)
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

	public float GetDebuffMultiplier(int hunger, int thirst)
	{
		float debuffMultiplier = 1f;

		if (hunger >= 100) debuffMultiplier += hungerDebuffAddition;
		if (thirst >= 100) debuffMultiplier += thirstDebuffAddition;

		return debuffMultiplier;
	}

	public void SwapPlacement(List<Opponent> participants, Opponent player, Opponent opponent)
	{
		int tmp = opponent.placement;
		player.placement--;
		opponent.placement++;

		contestManager.GetParticipants.Sort(SortByPlacement);
	}

	static int SortByPlacement(Opponent o1, Opponent o2)
	{
		return o1.placement.CompareTo(o2.placement);
	}

	#region Getters & Setters

	public int GetTirednessDecreaseRate { get { return tirednessDecreaseRate; } }
	public int GetTotalViews { get { return totalViews; } }
	public ResultsForm GetTrainingLevel1Results { get { return trainLevel1; } }
	public ResultsForm GetTrainingLevel2Results { get { return trainLevel2; } }
	public ResultsForm GetTrainingLevel3Results { get { return trainLevel3; } }
	public int GetMaxSleepHours { get { return maxSleepHours; } }

	#endregion
}
