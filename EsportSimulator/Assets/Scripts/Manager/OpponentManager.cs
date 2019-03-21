using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates, applies and manages opponents
/// </summary>
public class OpponentManager : MonoBehaviour
{
    public UIManager uiManager;

    [SerializeField] private int maxEloRating;
    [SerializeField] private List<string> opponentNames = new List<string>();
    [SerializeField] private List<Opponent> opponents = new List<Opponent>();
    
    private int opponentAmount;

    public LeagueForm league;

    /// <summary>
    /// Creates new opponents
    /// </summary>
    /// <param name="names"></param>
    /// <returns></returns>
    public List<Opponent> CreateNewOpponents(List<string> names)
    {

        List<Opponent> opponents = new List<Opponent>();
        opponentAmount = names.Count;

        for (int i = 0; i < opponentAmount; i++)
        {
            opponents.Add(new Opponent());
            opponents[i].name = opponentNames[i];
        }

        return opponents;
    }

    /// <summary>
    /// Applies saved opponents
    /// </summary>
    /// <param name="savedOpponents"></param>
    /// <param name="existingOpponents"></param>
    public void ApplyOpponents(Opponent[] savedOpponents, List<Opponent> existingOpponents)
    {
        for (int i = 0; i < existingOpponents.Count; i++)
        {
            existingOpponents[i] = savedOpponents[i];

            if (savedOpponents[i] == null) existingOpponents.RemoveAt(i);
        }

        existingOpponents.Sort(SortByRating);
        existingOpponents.Reverse();
        uiManager.UpdateLeaderboard(existingOpponents);
    }

    /// <summary>
    /// Randomizes the attributes of opponents
    /// </summary>
    /// <param name="opponents"></param>
    public void RandomizeOpponentAttributes(List<Opponent> opponents)
    {
        for (int i = 0; i < opponents.Count; i++)
        {
            int randomRating = Random.Range(0, maxEloRating);
            int oldRating = randomRating;

            while (randomRating == oldRating)
            {
                randomRating = Random.Range(0, maxEloRating);
            }
            
            opponents[i].eloRating = randomRating;
        }

        opponents.Sort(SortByRating);
        opponents.Reverse();
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

    public void InitializeOpponents()
    {
        opponents = CreateNewOpponents(opponentNames);
        RandomizeOpponentAttributes(opponents);
        uiManager.UpdateLeaderboard(opponents);
    }

    public List<Opponent> GetOpponents { get { return opponents; } }
    public int GetOpponentAmount { get { return opponentAmount; } }
}
