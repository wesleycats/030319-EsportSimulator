using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContestManager : MonoBehaviour
{
	[SerializeField] private bool createList;
	[SerializeField] private int participantAmount;
	[SerializeField] private int contestDuration;
	[SerializeField] private float battleDuration;
	[SerializeField] private List<DivisionForm> placementDivisions;
	[Tooltip("[0]=1v1, [1]=3v3, [2]=5v5")]
	[SerializeField] private List<int> tournamentMonths;

	[Header("Debug")]
	[SerializeField] private int currentBattle;
	[SerializeField] private int battlesPerHour;
	[SerializeField] private int currentPlacement;
	[SerializeField] private List<Opponent> participants;

	[Header("References")]
	public OpponentManager opponentManager;
	public LeaderboardManager lbManager;
	public ResultManager resultManager;
	public ActivityManager activityManager;
	public GameManager gameManager;
	public UIManager uiManager;

	private void Start()
	{
		battlesPerHour = Mathf.FloorToInt(participantAmount / contestDuration);
	}

	private void Update()
	{
		if (!createList) return;

		participants = CreateParticipantList(participantAmount, lbManager.Leaderboard);
		createList = false;
	}

	public void ResetCurrentBattle()
	{
		currentBattle = 0;
	}

	public void IncreaseCurrentBattle()
	{
		currentBattle++;
	}

	public List<Opponent> CreateParticipantList(int contestSize, List<Opponent> allOpponents)
	{
		List<Opponent> participants = new List<Opponent>();

		for (int i = 0; i < contestSize-1; i++)
		{
			Opponent randomOpponent = opponentManager.GetRandomOpponent(allOpponents);
			
			while (IsOpponentInList(randomOpponent, participants))
				randomOpponent = opponentManager.GetRandomOpponent(allOpponents);

			randomOpponent.defeated = false;
			participants.Add(randomOpponent);
		}

		participants.Sort(SortByRating);

		// Puts the player in placement
		participants.Reverse();
		lbManager.GetPlayer.placement = participants.Count;
		participants.Add(lbManager.GetPlayer);

		for (int i = 0; i < contestSize-1; i++)
		{
			participants[i].placement = i + 1;
		}

		return participants;
	}

	public void EndContest(Opponent opponent, Event contest)
	{
		activityManager.ChangeActivity(ActivityManager.Activity.Idle, 0);
		uiManager.battleMenu.SetActive(false);
		uiManager.contestResultMenu.SetActive(true);
		uiManager.darkOverlay.SetActive(true);
		uiManager.buttonManager.EnableAllButtons();
		uiManager.UpdateContestResults(opponent.placement, GetPlacementDivision(opponent, placementDivisions, contest.battleMode));
		gameManager.IncreaseFame(GetPlacementDivision(opponent, placementDivisions, contest.battleMode).fameReward);
		gameManager.IncreaseMoney(GetPlacementDivision(opponent, placementDivisions, contest.battleMode).moneyReward);
		gameManager.GetPlannedEvents.RemoveAt(gameManager.GetPlannedEvents.IndexOf(contest));
		uiManager.UpdateAll();
	}

	private bool IsOpponentInList(Opponent opponent, List<Opponent> opponentList)
	{
		for (int i = 0; i < opponentList.Count; i++)
			if (opponent == opponentList[i])
				return true;

		return false;
	}

	public DivisionForm GetPlacementDivision(Opponent opponent, List<DivisionForm> allDivisions, Battle.Mode mode)
	{
		DivisionForm division = null;

		// Reverses the list so it can be checked properly on highest achieved division
		allDivisions.Reverse();

		foreach (DivisionForm f in allDivisions)
		{
			if (opponent.placement <= f.maxRank && mode == f.mode)
			{
				division = f;
			}
		}

		return division;
	}

	public void PlanTournament(Battle.Mode mode)
	{
		Event tournament = new Event(Event.Type.Tournament, mode, 0);
		tournament.month = GetTournamentDate(tournament);

		if (IsTournamentPlanned(tournament, gameManager.GetPlannedEvents)) return;

		gameManager.GetPlannedEvents.Add(tournament);
	}

	public int GetTournamentDate(Event eventToPlan)
	{
		if (eventToPlan.type == Event.Type.Tournament)
		{
			switch (eventToPlan.battleMode)
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

	#region Getters & Setters

	public float GetBattleDuration { get { return battleDuration; } }
	public int GetContestDuration { get { return contestDuration; } }
	public int GetCurrentBattle { get { return currentBattle; } }
	public int GetBattlesPerHour { get { return battlesPerHour; } }
	public int GetParticipantAmount { get { return participantAmount; } }
	public List<Opponent> GetParticipants { get { return participants; } }

	public List<Opponent> SetParticipants { set { participants = value; } }

	#endregion
}
