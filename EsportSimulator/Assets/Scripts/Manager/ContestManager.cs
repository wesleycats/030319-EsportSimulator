using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestManager : MonoBehaviour
{
	[Tooltip("[0]=1v1, [1]=3v3, [2]=5v5")]
	[SerializeField] private List<int> tournamentMonths;
	[SerializeField] private List<Opponent> participants;

	[Header("Contest")]
	[SerializeField] private int contestDuration;
	[SerializeField] private int contestParticipantAmount;
	[SerializeField] private int battlesPerHour;
	[SerializeField] private List<DivisionForm> placementDivisions;
	[Tooltip("Bias to influence battle result")]
	[SerializeField] private int playerBattleBias;
	[Tooltip("Percentage to win given per point")]
	[SerializeField] private float pointPercentage;

	private Opponent player;
	private Opponent opponent;
	private Event currentEvent;
	private int battleIndex = 0;
	private bool battleWon;

	[Header("References")]
	public OpponentManager opponentManager;
	public LeaderboardManager lbManager;
	public ResultManager resultManager;
	public ActivityManager activityManager;
	public GameManager gameManager;
	public UIManager uiManager;
	public TimeManager timeManager;

	public void StartContest(Event contest)
	{
		uiManager.ActivateContestAnnouncement(contest.battleMode);
		uiManager.buttonManager.EnableAllButtonsOf("Contest");
		participants = CreateParticipantList(contestParticipantAmount, lbManager.Leaderboard);
		battlesPerHour = Mathf.FloorToInt(participants.Count / contestDuration);
		currentEvent = contest;
		ResetBattleIndex();
	}

	public void StartBattle()
	{
		Event contest = currentEvent;
		player = participants[participants.IndexOf(lbManager.GetPlayer)];
		opponent = participants[participants.IndexOf(player) - 1];

		uiManager.battleMenu.SetActive(true);

		int[] skillsDelta = new int[0];
		int gkDelta = gameManager.GetGameKnowledge - opponent.gameKnowledge;
		int mDelta = gameManager.GetMechanics - opponent.mechanics;
		int tpDelta = gameManager.GetTeamPlay - opponent.teamPlay;

		int winChance = 0;
		int randomizer = resultManager.GetRealRandom(0, 100);

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
				skillsDelta[2] = tpDelta;
				break;
		}

		winChance = GetWinChance(skillsDelta, pointPercentage) + playerBattleBias;

		if (winChance > 100) winChance = 100;
		if (winChance < 0) winChance = 0;

		uiManager.UpdateBattleStats(player, opponent, contest.battleMode, winChance);
		
		if (randomizer < winChance)
		{
			battleWon = true;
		}
		else
		{
			battleWon = false;
		}
	}

	public void BattleResult()
	{
		if (activityManager.currentActivity != ActivityManager.Activity.Contest) return;

		uiManager.ActivateBattleResult(battleWon);
		SwapPlacement(participants, player, opponent);
	}

	public void AdvanceBattle()
	{
		if (activityManager.currentActivity != ActivityManager.Activity.Contest) return;

		uiManager.darkOverlay.SetActive(true);

		if (!battleWon)
		{
			EndContest(false);
			return;
		}

		if (participants.IndexOf(player) <= 0)
		{
			// Win tournament
			EndContest(true);
			return;
		}
		else
		{
			// Continue tournament
			if (battleIndex >= GetBattlesPerHour)
			{
				timeManager.IncreaseHours(1);
				ResetBattleIndex();
			}		
			else
				IncreaseBattleIndex();

			StartBattle();
		}
	}

	public void EndContest(bool win)
	{
		Battle.Mode battleMode = currentEvent.battleMode;
		DivisionForm divison = GetPlacementDivision(player, placementDivisions, battleMode);
		activityManager.ChangeActivity(ActivityManager.Activity.Idle, 0);

		timeManager.UnPauseTimer();
		timeManager.UnPauseGame();
		timeManager.contestStarted = false;

		gameManager.IncreaseEarnedEventRewards(divison.moneyReward);
		gameManager.IncreaseMoney(divison.moneyReward);
		gameManager.IncreaseFame(divison.fameReward);
		gameManager.GetPlannedEvents.RemoveAt(gameManager.GetPlannedEvents.IndexOf(currentEvent));

		uiManager.battleMenu.SetActive(false);
		uiManager.darkOverlay.SetActive(true);
		uiManager.contestResultMenu.SetActive(true);
		uiManager.UpdateContestResults(player.placement, divison);
		uiManager.UpdateAll();
	}

	public void ResetBattleIndex()
	{
		battleIndex = 1;
	}

	public void IncreaseBattleIndex()
	{
		battleIndex++;
	}

	public List<Opponent> CreateParticipantList(int contestSize, List<Opponent> allOpponents)
	{
		List<Opponent> participants = new List<Opponent>();

		for (int i = 0; i < contestSize - 1; i++)
		{
			Opponent randomOpponent = opponentManager.GetRandomOpponent(allOpponents);

			while (IsOpponentInList(randomOpponent, participants))
				randomOpponent = opponentManager.GetRandomOpponent(allOpponents);

			randomOpponent.defeated = false;
			participants.Add(randomOpponent);
		}

		participants.Sort(SortByRating);
		participants.Reverse();

		lbManager.GetPlayer.placement = participants.Count;

		participants.Add(lbManager.GetPlayer);

		for (int i = 0; i < contestSize - 1; i++)
		{
			participants[i].placement = i + 1;
		}

		return participants;
	}

	public DivisionForm GetPlacementDivision(Opponent opponent, List<DivisionForm> allDivisions, Battle.Mode mode)
	{
		foreach (DivisionForm f in allDivisions)
		{
			if (opponent.placement >= f.minRank && mode == f.mode)
			{
				return f;
			}
		}
		return null;
	}

	public void PlanTournament(Battle.Mode mode)
	{
		Event tournament = new Event(Event.Type.Tournament, mode, 0);
		tournament.month = GetTournamentDate(tournament);

		if (IsTournamentPlanned(tournament, gameManager.GetPlannedEvents)) return;

		gameManager.GetPlannedEvents.Add(tournament);
	}

	private bool IsOpponentInList(Opponent opponent, List<Opponent> opponentList)
	{
		for (int i = 0; i < opponentList.Count; i++)
			if (opponent == opponentList[i])
				return true;

		return false;
	}

	public bool IsTournamentPlanned(Event tournament, List<Event> plannedEvents)
	{
		for (int i = 0; i < plannedEvents.Count; i++)
		{
			if (tournament == plannedEvents[i])
			{
				return true;
			}
		}
		return false;
	}

	static int SortByRating(Opponent o1, Opponent o2)
	{
		return o1.eloRating.CompareTo(o2.eloRating);
	}

	static int SortByPlacement(Opponent o1, Opponent o2)
	{
		return o1.placement.CompareTo(o2.placement);
	}

	public void SwapPlacement(List<Opponent> participants, Opponent player, Opponent opponent)
	{
		int tmp = opponent.placement;
		player.placement--;
		opponent.placement++;

		participants.Sort(SortByPlacement);
	}

	public int GetTournamentDate(Event e)
	{
		if (e.type == Event.Type.Tournament)
		{
			switch (e.battleMode)
			{
				case Battle.Mode.OneVersusOne:
					return tournamentMonths[0];

				case Battle.Mode.ThreeVersusThree:
					return tournamentMonths[1];

				case Battle.Mode.FiveVersusFive:
					return tournamentMonths[2];
			}
		}

		return 0;
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

	#region Getters & Setters

	public int GetContestDuration { get { return contestDuration; } }
	public int GetBattlesPerHour { get { return battlesPerHour; } }

	public List<Opponent> Participants { get { return participants; } set { participants = value; } }

	#endregion
}
