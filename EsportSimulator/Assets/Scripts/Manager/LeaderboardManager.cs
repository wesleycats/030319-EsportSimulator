using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private int divisionSize = 10;
    [SerializeField] private List<Opponent> leaderboard;

    public OpponentManager opponentManager;
    public GameManager gameManager;
    public PlayerData playerData;

    private List<Opponent> npcs;

    public void SortLeaderboard(List<Opponent> currentLeaderboard)
    {
        currentLeaderboard.Sort(SortByRating);
        currentLeaderboard.Reverse();

        if (opponentManager.GetPlayer == currentLeaderboard[0]) gameManager.WinGame();
    }

    public Opponent GetRandomOpponent(List<Opponent> leaderboard, DivisionForm division)
    {
        Opponent randomOpponent = new Opponent();

        int randomIndex = Random.Range(division.minRank, division.maxRank);
        randomOpponent = leaderboard[randomIndex - 1];
        int oldIndex = randomIndex;

        if (randomOpponent.name == "YOU")
        {
            Debug.Log(randomIndex);
            while (randomIndex == oldIndex)
            {
                randomIndex = Random.Range(division.minRank, division.maxRank);
            }

            randomOpponent = leaderboard[randomIndex];
        }

        return randomOpponent;
    }

    public DivisionForm GetOpponentDivision(Opponent opponent, List<Opponent> leaderboard, LeagueForm league)
    {
        DivisionForm division = null;
        
        for (int j = 0; j < league.divisions.Length; j++)
        {
            if (GetOpponentRank(opponent, leaderboard) >= league.divisions[j].maxRank && GetOpponentRank(opponent, leaderboard) <= league.divisions[j].minRank)
            {
                return league.divisions[j];
            }
        }

        return division;
    }

    /// <summary>
    /// Returns the current rank of the opponent. !Caution! This is not the index
    /// </summary>
    /// <param name="opponent"></param>
    /// <param name="allOpponents"></param>
    /// <returns></returns>
    public int GetOpponentRank(Opponent opponent, List<Opponent> allOpponents)
    {
        return allOpponents.IndexOf(opponent) + 1;
    }

    public void Initialize()
    {
        npcs = opponentManager.GetOpponents;
        leaderboard = npcs;
        leaderboard.Add(opponentManager.GetPlayer);
        SortLeaderboard(leaderboard);
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

    #region Getter & Setters

    public List<Opponent> GetLeaderboard { get { return leaderboard; } }
    public List<Opponent> SetLeaderboard { set { leaderboard = value; } }

    #endregion
}
