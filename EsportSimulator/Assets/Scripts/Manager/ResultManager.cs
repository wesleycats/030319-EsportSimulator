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

	[SerializeField] private bool debuff = false;

	[SerializeField] private float currentDebuffMultiplier = 1f;
	[SerializeField] private float currentDebuffMultiplierAmount = 1f;
	[SerializeField] private float debuffMultiplier = 0.5f;
	[SerializeField] private float debuffMultiplierAmount = 1f;

	[SerializeField] private int winBiasPercentage;
	[SerializeField] private float winPercentagePerSkillPoint;

	[Header("Needs")]
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


	private List<Opponent> leaderboard;
	private Opponent player;

	[Header("References")]
	public UIManager uiManager;
	public GameManager gameManager;
	public LeaderboardManager lbManager;
	public OpponentManager opponentManager;
	public ShopManager shopManager;
	public ContestManager contestManager;
	public TimeManager timeManager;
	public PlayerData playerData;

	private void Start()
	{
		leaderboard = lbManager.GetLeaderboard;
		player = opponentManager.GetPlayer;

		if (debuff)
			ApplyDebuffs(debuffMultiplier, debuffMultiplierAmount);
	}

	public void ResetTotalViews()
	{
		totalIncome = 0;
		totalViews = 0;
	}

	public void TryBattle(Battle.Mode battleType)
	{
		BattleResults(lbManager.GetRandomOpponent(leaderboard, lbManager.GetOpponentDivision(player, leaderboard, opponentManager.league)), battleType);
	}

	public float GetTrainingCostAmount(ActivityManager.TrainType trainType, float duration)
	{
		currentDebuffMultiplier *= currentDebuffMultiplierAmount;

		switch (trainType)
		{
			case ActivityManager.TrainType.WatchingGK:
				return trainLevel1.Money * currentDebuffMultiplier * duration;

			case ActivityManager.TrainType.WatchingTP:
				return trainLevel1.Money * currentDebuffMultiplier * duration;

			case ActivityManager.TrainType.WatchingM:
				return trainLevel1.Money * currentDebuffMultiplier * duration;

			case ActivityManager.TrainType.CourseGK:
				return trainLevel2.Money * currentDebuffMultiplier * duration;

			case ActivityManager.TrainType.CourseTP:
				return trainLevel2.Money * currentDebuffMultiplier * duration;

			case ActivityManager.TrainType.CourseM:
				return trainLevel2.Money * currentDebuffMultiplier * duration;

			case ActivityManager.TrainType.CoursePlusGK:
				return trainLevel3.Money * currentDebuffMultiplier * duration;

			case ActivityManager.TrainType.CoursePlusTP:
				return trainLevel3.Money * currentDebuffMultiplier * duration;

			case ActivityManager.TrainType.CoursePlusM:
				return trainLevel3.Money * currentDebuffMultiplier * duration;

			default:
				Debug.LogError("No training type has been given");
				return 0;
		}
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

    public void StreamResults(int fame)
    {
		if (fame < minimalFameForViews) return;

		int fameCount = fame / minimalFameForViews;
		totalViews += GetStreamViews(fameCount, minViewsPerFame, maxViewsPerFame);

		int viewCount = totalViews / minimalViewsForIncome;
		int money = GetStreamIncome(viewCount, minIncomePerViews, maxIncomePerViews);

		gameManager.IncreaseMoney(money);
		totalIncome += money;
	}
	
    public void PayRent(AccommodationForm accommodation)
    {
        gameManager.DecreaseMoney(accommodation.cost);
    }

    /// <summary>
    /// Applies the results you will get from sleeping per hour
    /// </summary>
    public void SleepResults(int tirednessDecreaseRate)
    {
        DecreaseTiredness(tirednessDecreaseRate);
    }

    /// <summary>
    /// Applies the results you will get from working per hour
    /// </summary>
    /// <param name="workLevel"></param>
    public void WorkResults(int workLevel)
    {
        currentDebuffMultiplier *= currentDebuffMultiplierAmount;

        switch (workLevel)
        {
            case 0:
                gameManager.IncreaseMoney((int)(workLevel1.Money * currentDebuffMultiplier));
                gameManager.Tiredness += workLevel1.Tiredness;
                gameManager.WorkExperience += workLevel1.WorkExperience;
                break;

            case 1:
                gameManager.IncreaseMoney((int)(workLevel2.Money * currentDebuffMultiplier));
                gameManager.Tiredness += workLevel2.Tiredness;
                gameManager.WorkExperience += workLevel2.WorkExperience;
                gameManager.GameKnowledge += (int)(workLevel2.GameKnowledge * currentDebuffMultiplier);

                break;

            case 2:
                gameManager.IncreaseMoney((int)(workLevel3.Money * currentDebuffMultiplier));
                gameManager.Tiredness += workLevel3.Tiredness;
                gameManager.WorkExperience += workLevel3.WorkExperience;
                gameManager.GameKnowledge += (int)(workLevel3.GameKnowledge * currentDebuffMultiplier);
                gameManager.GameKnowledge += (int)(workLevel3.Mechanics * currentDebuffMultiplier);

                break;

            case 3:
                gameManager.IncreaseMoney((int)(workLevel4.Money * currentDebuffMultiplier));
                gameManager.Tiredness += workLevel4.Tiredness;
                gameManager.WorkExperience += workLevel4.WorkExperience;
                gameManager.GameKnowledge += (int)(workLevel4.GameKnowledge * currentDebuffMultiplier);
                gameManager.GameKnowledge += (int)(workLevel4.TeamPlay * currentDebuffMultiplier);

                break;

            default:
                Debug.LogError("No work level has been given");
                break;
        }

        IncreaseHunger(hungerIncreaseRate);
        IncreaseThirst(thirstIncreaseRate);
    }

    /// <summary>
    /// Applies the results you will get from training per hour
    /// </summary>
    /// <param name="trainType"></param>
    public void TrainResults(ActivityManager.TrainType trainType)
    {
        currentDebuffMultiplier *= currentDebuffMultiplierAmount;

        switch (trainType)
        {
            case ActivityManager.TrainType.WatchingGK:
                gameManager.IncreaseGameKnowledge((int)(gameManager.GetCurrentAccommodation.trainingLevel1Amount * currentDebuffMultiplier));
                gameManager.IncreaseTiredness(trainLevel1.Tiredness);
                break;

            case ActivityManager.TrainType.WatchingTP:
                gameManager.IncreaseTeamPlay((int)(gameManager.GetCurrentAccommodation.trainingLevel1Amount * currentDebuffMultiplier));
                gameManager.IncreaseTiredness(trainLevel1.Tiredness);
                break;

            case ActivityManager.TrainType.WatchingM:
                gameManager.IncreaseMechanics((int)(gameManager.GetCurrentAccommodation.trainingLevel1Amount * currentDebuffMultiplier));
                gameManager.IncreaseTiredness(trainLevel1.Tiredness);
                break;

            case ActivityManager.TrainType.CourseGK:
                gameManager.DecreaseMoney(trainLevel2.Money);
                gameManager.IncreaseGameKnowledge((int)(gameManager.GetCurrentAccommodation.trainingLevel2Amount * currentDebuffMultiplier));
                gameManager.IncreaseTiredness(trainLevel2.Tiredness);
                break;

            case ActivityManager.TrainType.CourseTP:
                gameManager.IncreaseTeamPlay((int)(gameManager.GetCurrentAccommodation.trainingLevel2Amount * currentDebuffMultiplier));
                gameManager.DecreaseMoney(trainLevel2.Money);
                gameManager.IncreaseTiredness(trainLevel2.Tiredness);
                break;

            case ActivityManager.TrainType.CourseM:
                gameManager.IncreaseMechanics((int)(gameManager.GetCurrentAccommodation.trainingLevel2Amount * currentDebuffMultiplier));
                gameManager.DecreaseMoney(trainLevel2.Money);
                gameManager.IncreaseTiredness(trainLevel2.Tiredness);
                break;

            case ActivityManager.TrainType.CoursePlusGK:
                gameManager.IncreaseGameKnowledge((int)(gameManager.GetCurrentAccommodation.trainingLevel3Amount * currentDebuffMultiplier));
                gameManager.DecreaseMoney(trainLevel3.Money);
                gameManager.IncreaseTiredness(trainLevel3.Tiredness);
                break;

            case ActivityManager.TrainType.CoursePlusTP:
                gameManager.IncreaseTeamPlay((int)(gameManager.GetCurrentAccommodation.trainingLevel3Amount * currentDebuffMultiplier));
                gameManager.DecreaseMoney(trainLevel3.Money);
                gameManager.IncreaseTiredness(trainLevel3.Tiredness);
                break;

            case ActivityManager.TrainType.CoursePlusM:
                gameManager.IncreaseMechanics((int)(gameManager.GetCurrentAccommodation.trainingLevel3Amount * currentDebuffMultiplier));
                gameManager.DecreaseMoney(trainLevel3.Money);
                gameManager.IncreaseTiredness(trainLevel3.Tiredness);
                break;

            default:
                Debug.LogError("No training type has been given");
                break;
        }
    }

    public void BattleResults(Opponent opponent, Battle.Mode battleMode)
    {
        int randomizer = 0;
        int winChance = 0;
        int gkDelta = gameManager.GetGameKnowledge - opponent.gameKnowledge;
        int mDelta = gameManager.GetMechanics - opponent.mechanics;
        int tpDelta = gameManager.GetTeamPlay - opponent.teamPlay;
        int[] skillsDelta = new int[0];
        bool win = false;

        currentDebuffMultiplier *= currentDebuffMultiplierAmount;

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
				
                gameManager.IncreaseGameKnowledge((int)oneVsOne.GameKnowledge );
                gameManager.IncreaseMechanics((int)oneVsOne.Mechanics);
                gameManager.IncreaseTeamPlay((int)threeVsThree.Mechanics);
                gameManager.IncreaseTiredness(oneVsOne.Tiredness);

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

                gameManager.IncreaseGameKnowledge((int)threeVsThree.GameKnowledge);
                gameManager.IncreaseMechanics((int)threeVsThree.Mechanics);
                gameManager.IncreaseTeamPlay((int)threeVsThree.Mechanics);
                gameManager.IncreaseTiredness(threeVsThree.Tiredness);

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
                break;

            case Battle.Mode.FiveVersusFive:
                randomizer = Random.Range(0, 100);
                skillsDelta = new int[3];
                skillsDelta[0] = gkDelta;
                skillsDelta[1] = mDelta;
                skillsDelta[2] = tpDelta;
                winChance = GetWinChance(skillsDelta, winPercentagePerSkillPoint) + winBiasPercentage;
                
                Debug.Log("Player gk: " + gameManager.GetGameKnowledge+ " Player m: " + gameManager.GetMechanics + " Player tp: " + gameManager.GetTeamPlay);
                Debug.Log(opponent.name + " gk: " + opponent.gameKnowledge + " Opponent m: " + opponent.mechanics + " Opponent tp: " + opponent.teamPlay);
                Debug.Log("Win chance + bias= " + winChance);
                Debug.Log("randomizer= " + randomizer);

                if (randomizer < winChance || win)
                    win = true;
                else
                    win = false;

                Debug.Log("Win=" + win);

                gameManager.IncreaseGameKnowledge((int)fiveVsFive.GameKnowledge);
                gameManager.IncreaseMechanics((int)fiveVsFive.Mechanics);
                gameManager.IncreaseTeamPlay((int)fiveVsFive.Mechanics);
                gameManager.IncreaseTiredness(fiveVsFive.Tiredness);

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
                break;
        }

        opponentManager.UpdatePlayerAttributes(player, gameManager);
        lbManager.SortLeaderboard(lbManager.GetLeaderboard);
        uiManager.UpdateAll();
    }

	public IEnumerator ContestResults(Event contest, List<Opponent> participants)
	{
		uiManager.battleMenu.SetActive(true);
		timeManager.SetPause = true;
		contestManager.IncreaseCurrentBattle();

		Opponent player = participants[participants.IndexOf(opponentManager.GetPlayer)];
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

    public void ApplyDebuffs(float debuffMultiplier, float debuffMultiplierAmount)
    {
        currentDebuffMultiplier = debuffMultiplier;
        currentDebuffMultiplierAmount = debuffMultiplierAmount;

        SliderToText[] sliders = Resources.FindObjectsOfTypeAll<SliderToText>();

        foreach (SliderToText s in sliders)
        {
            s.DebuffMultiplier = currentDebuffMultiplier;
            s.DebuffMultiplierAmount = currentDebuffMultiplierAmount;
        }
    }

    public void ResetDebuffs()
    {
        currentDebuffMultiplier = 1f;
        debuffMultiplierAmount = 1f;

        SliderToText[] sliders = Resources.FindObjectsOfTypeAll<SliderToText>();

        foreach (SliderToText s in sliders)
        {
            s.DebuffMultiplier = currentDebuffMultiplier;
            s.DebuffMultiplierAmount = currentDebuffMultiplierAmount;
        }
    }

    public void Eat(int foodLevel)
    {
        switch (foodLevel)
        {
            case 1:
                //TODO create not enough money signal
                if (gameManager.GetMoney < foodBad.Money) return;

                DecreaseHunger(foodBad.Hunger);
                gameManager.DecreaseMoney(foodBad.Money);
                break;
            case 2:
                //TODO create not enough money signal
                if (gameManager.GetMoney < foodStandard.Money) return;

                DecreaseHunger(foodStandard.Hunger);
                gameManager.DecreaseMoney(foodStandard.Money);
                break;
            case 3:
                //TODO create not enough money signal
                if (gameManager.GetMoney < foodGood.Money) return;

                DecreaseHunger(foodGood.Hunger);
                gameManager.DecreaseMoney(foodGood.Money);
                break;
            case 4:
                //TODO create not enough money signal
                if (gameManager.GetMoney < foodExcellent.Money) return;

                DecreaseHunger(foodExcellent.Hunger);
                gameManager.DecreaseMoney(foodExcellent.Money);
                break;

            default:
                Debug.LogError("No food level has been given");
                break;
        }

        uiManager.UpdateProgress(gameManager.GetMoney, gameManager.Rating, gameManager.Fame);
        uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
    }

    public void Drink(int drinkLevel)
    {
        switch (drinkLevel)
        {
            case 1:
                //TODO create not enough money signal
                if (gameManager.GetMoney < drinkBad.Money) return;

                DecreaseThirst(drinkBad.Thirst);
                gameManager.DecreaseMoney(drinkBad.Money);
                break;
            case 2:
                //TODO create not enough money signal
                if (gameManager.GetMoney < drinkStandard.Money) return;

                DecreaseThirst(drinkStandard.Thirst);
                gameManager.DecreaseMoney(drinkStandard.Money);
                break;
            case 3:
                //TODO create not enough money signal
                if (gameManager.GetMoney < drinkGood.Money) return;

                DecreaseThirst(drinkGood.Thirst);
                gameManager.DecreaseMoney(drinkGood.Money);
                break;
            case 4:
                //TODO create not enough money signal
                if (gameManager.GetMoney < drinkExcellent.Money) return;

                DecreaseThirst(drinkExcellent.Thirst);
                gameManager.DecreaseMoney(drinkExcellent.Money);
                break;

            default:
                Debug.LogError("No drink level has been given");
                break;
        }

        uiManager.UpdateProgress(gameManager.GetMoney, gameManager.Rating, gameManager.Fame);
        uiManager.UpdateNeeds(gameManager.Tiredness, gameManager.Hunger, gameManager.Thirst);
    }
    
    private void IncreaseHunger(int amount)
    {
        gameManager.Hunger += amount;

        if (gameManager.Hunger >= 70)
        {
            if (uiManager.needsMenuButton.GetComponent<LerpColor>().endColor != Color.red)
            {
                uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.yellow;
                uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
            }
        }

        if (gameManager.Hunger >= 100)
        {
            ApplyDebuffs(debuffMultiplier, debuffMultiplierAmount);
            uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.red;
        }
    }

    private void IncreaseThirst(int amount)
    {
        gameManager.Thirst += amount;

        if (gameManager.Thirst >= 70)
        {
            if (uiManager.needsMenuButton.GetComponent<LerpColor>().endColor != Color.red)
            {
                uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.yellow;
                uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
            }
        }

        if (gameManager.Thirst >= 100)
        {
            ApplyDebuffs(debuffMultiplier, debuffMultiplierAmount);
            uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.red;
        }
    }

    private void DecreaseTiredness(int amount)
    {
        gameManager.Tiredness -= amount;

        if (gameManager.Tiredness < 0)
        {
            gameManager.Tiredness = 0;
        }
        else if (gameManager.Tiredness < 70 && gameManager.Hunger < 70 && gameManager.Thirst < 70)
        {
            uiManager.needsMenuButton.GetComponent<Image>().color = uiManager.needsMenuButton.GetComponent<LerpColor>().startColor;
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = false;
        }
    }

    private void DecreaseHunger(int amount)
    {
        gameManager.Hunger -= amount;

        if (gameManager.Hunger < 0)
        {
            gameManager.Hunger = 0;
        }
        else if (gameManager.Tiredness < 70 && gameManager.Hunger < 70 && gameManager.Thirst < 70)
        {
            uiManager.needsMenuButton.GetComponent<Image>().color = uiManager.needsMenuButton.GetComponent<LerpColor>().startColor;
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = false;
        }
    }

    private void DecreaseThirst(int amount)
    {
        gameManager.Thirst -= amount;

        if (gameManager.Thirst < 0)
        {
            gameManager.Thirst = 0;
        }
        else if (gameManager.Tiredness < 70 && gameManager.Hunger < 70 && gameManager.Thirst < 70)
        {
            uiManager.needsMenuButton.GetComponent<Image>().color = uiManager.needsMenuButton.GetComponent<LerpColor>().startColor;
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = false;
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

	private int GetRealRandom(int min, int max)
	{
		int randomNumber = Random.Range(min, max);
		int oldNumber = randomNumber;

		while (oldNumber == randomNumber)
			randomNumber = Random.Range(min, max);

		return randomNumber;
	}

	#region Getters & Setters

	public int GetTirednessDecreaseRate { get { return tirednessDecreaseRate; } }
    public float GetDebuffMultiplier { get { return debuffMultiplier; } }
    public float GetDebuffMultiplierAmount { get { return debuffMultiplierAmount; } }
	public int GetTotalViews { get { return totalViews; } }

		#endregion
}
