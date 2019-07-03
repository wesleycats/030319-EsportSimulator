using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
	public string playerName;
	public LeagueForm league;

	[SerializeField] private int divisionSize = 10;
    [SerializeField] private List<Opponent> leaderboard;

	[Header("References")]
	public OpponentManager opponentManager;
	public UIManager uiManager;
	public GameManager gameManager;
    public PlayerData playerData;

	private List<Opponent> npcs;

	public void Initialize()
	{
		npcs = opponentManager.GetAllOpponents;
		leaderboard = npcs;
		SortLeaderboard();
	}

	public Opponent GetRandomOpponent(Opponent o)
	{
		DivisionForm division = GetOpponentDivision(GetPlayer);
		Opponent randomOpponent = new Opponent();

		do
		{
			randomOpponent = leaderboard[GetRealRandom(division.minRank, division.maxRank)];
		}
		while (randomOpponent.name == o.name);

        return randomOpponent;
	}

	public DivisionForm GetOpponentDivision(Opponent opponent)
    {
		int rank = GetOpponentRank(opponent, leaderboard);

		foreach (DivisionForm d in league.divisions)
		{
			if (rank <= d.minRank && rank >= d.maxRank)
			{
				return d;
			}
		}
        return null;
    }

    /// <summary>
    /// Returns the current rank of the opponent
    /// </summary>
    /// <param name="opponent"></param>
    /// <param name="allOpponents"></param>
    /// <returns></returns>
    public int GetOpponentRank(Opponent opponent, List<Opponent> list)
    {
		return list.IndexOf(opponent) + 1;
    }

	public void SortLeaderboard()
	{
		foreach (Opponent o in leaderboard)
		{
			if (o.name != playerName) continue;

			o.gameKnowledge = gameManager.GetGameKnowledge;
			o.teamPlay = gameManager.GetTeamPlay;
			o.mechanics = gameManager.GetMechanics;
			o.eloRating = gameManager.GetRating;
		}

		leaderboard.Sort(SortByRating);
		leaderboard.Reverse();

		if (leaderboard[0].name == playerName) gameManager.WinGame();

		uiManager.UpdateLeaderboard(leaderboard);
	}

	static int SortByRating(Opponent o1, Opponent o2)
    {
        return o1.eloRating.CompareTo(o2.eloRating);
    }

    static int SortByGameKnowledge(Opponent o1, Opponent o2)
    {
        return o1.gameKnowledge.CompareTo(o2.gameKnowledge);
    }

    static int SortByTeamPlay(Opponent o1, Opponent o2)
    {
        return o1.teamPlay.CompareTo(o2.teamPlay);
    }

    static int SortByMechanics(Opponent o1, Opponent o2)
    {
        return o1.mechanics.CompareTo(o2.mechanics);
    }

	public int GetRealRandom(int min, int max)
	{
		int randomNumber = Random.Range(min, max);
		int oldNumber = randomNumber;

		while (oldNumber == randomNumber)
			randomNumber = Random.Range(min, max);

		return randomNumber;
	}

    #region Getter & Setters

    public List<Opponent> Leaderboard { get { return leaderboard; } set { leaderboard = value; } }
	public Opponent GetPlayer
	{
		get
		{
			Opponent player = new Opponent();

			foreach (Opponent o in leaderboard)
			{
				if (o.name != playerName) continue;

				o.gameKnowledge = gameManager.GetGameKnowledge;
				o.teamPlay = gameManager.GetTeamPlay;
				o.mechanics = gameManager.GetMechanics;
				o.eloRating = gameManager.GetRating;
				return o;
			}

			player.name = "YOU";
			player.gameKnowledge = gameManager.GetGameKnowledge;
			player.teamPlay = gameManager.GetTeamPlay;
			player.mechanics = gameManager.GetMechanics;
			player.eloRating = gameManager.GetRating;

			return player;
		}
	}
	#endregion
}
