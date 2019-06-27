using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates, applies and manages opponents
/// </summary>
public class OpponentManager : MonoBehaviour
{
	public UIManager uiManager;
	public GameManager gameManager;
	public LeaderboardManager lbManager;

	[SerializeField] private int maxEloRating;
	[SerializeField] private List<string> opponentNames = new List<string>();
	[SerializeField] private List<Opponent> opponents = new List<Opponent>();

	private int opponentAmount;

	public void InitializeOpponents()
	{
		opponents = CreateNewOpponents();
		RandomizeOpponentAttributes(opponents);
	}

	/// <summary>
	/// Creates new opponents
	/// </summary>
	/// <param name="names"></param>
	/// <returns></returns>
	public List<Opponent> CreateNewOpponents()
	{
		List<Opponent> opponents = new List<Opponent>();
		opponentAmount = opponentNames.Count;

		for (int i = 0; i < opponentAmount; i++)
		{
			opponents.Add(new Opponent());
			opponents[i].name = opponentNames[i];
		}

		opponents.Add(lbManager.GetPlayer);

		return opponents;
	}

	/// <summary>
	/// Applies saved opponents
	/// </summary>
	/// <param name="savedOpponents"></param>
	/// <param name="existingOpponents"></param>
	public void ApplyOpponents(List<Opponent> savedOpponents, List<Opponent> existingOpponents)
	{
		existingOpponents.Clear();

		foreach (Opponent o in savedOpponents)
			existingOpponents.Add(o);
    }

    /// <summary>
    /// Randomizes the attributes of opponents
    /// </summary>
    /// <param name="opponents"></param>
    public void RandomizeOpponentAttributes(List<Opponent> opponents)
    {
		LeagueForm league = lbManager.league;

		foreach (Opponent o in opponents)
		{
            int randomRating = Random.Range(0, maxEloRating);
            int oldRating = randomRating;

            while (randomRating == oldRating)
            {
                randomRating = Random.Range(0, maxEloRating);
            }

			o.eloRating = randomRating;
        }

		opponents.Sort(SortByRating);
		opponents.Reverse();

		foreach (Opponent o in opponents)
		{
			int gameKnowledge = 0;
			int teamPlay = 0;
			int mechanics = 0;
			int rank = lbManager.GetOpponentRank(o, opponents);

			foreach (DivisionForm d in league.divisions)
			{
				if (rank <= d.minRank && rank >= d.maxRank)
				{
					gameKnowledge = Random.Range(d.minGameKnowledge, d.maxGameKnowledge);
					teamPlay = Random.Range(d.minTeamPlay, d.maxTeamPlay);
					mechanics = Random.Range(d.minMechanics, d.maxMechanics);
				}
			}

			o.gameKnowledge = gameKnowledge;
			o.teamPlay = teamPlay;
			o.mechanics = mechanics;
		}
    }

	static int SortByRating(Opponent o1, Opponent o2)
	{
		return o1.eloRating.CompareTo(o2.eloRating);
	}

	#region Getters & Setters

	public List<Opponent> GetAllOpponents { get { return opponents; } }
    public int GetOpponentAmount { get { return opponentAmount; } }
	public Opponent GetRandomOpponent(List<Opponent> opponentList)
	{
		Opponent randomOpponent = new Opponent();

		int randomIndex = Random.Range(0, opponentList.Count);
		int oldIndex = randomIndex;

		randomOpponent = opponentList[randomIndex];

		if (randomOpponent.name == "YOU")
		{
			while (randomIndex == oldIndex)
			{
				randomIndex = Random.Range(0, opponentList.Count);
			}

			randomOpponent = opponentList[randomIndex];
		}

		return randomOpponent;
	}

	#endregion
}
