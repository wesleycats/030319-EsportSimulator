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
    private Opponent player = new Opponent();

    public LeagueForm league;
    
    public void UpdatePlayerAttributes(Opponent player, GameManager gameManager)
    {
        player.name = "YOU";
        player.gameKnowledge = gameManager.GetGameKnowledge;
        player.teamPlay = gameManager.GetTeamPlay;
        player.mechanics = gameManager.GetMechanics;
        player.eloRating = gameManager.GetRating;
    }

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

        for (int i = 0; i < opponents.Count; i++)
        {
            int gameKnowledge = 0;
            int teamPlay = 0;
            int mechanics = 0;

            for (int j = 0; j < league.divisions.Length; j++)
            {
                if (GetOpponentRank(opponents[i], opponents) >= league.divisions[j].maxRank && GetOpponentRank(opponents[i], opponents) <= league.divisions[j].minRank)
                {
                    gameKnowledge = Random.Range(league.divisions[j].minGameKnowledge, league.divisions[j].maxGameKnowledge);
                    teamPlay = Random.Range(league.divisions[j].minTeamPlay, league.divisions[j].maxTeamPlay);
                    mechanics = Random.Range(league.divisions[j].minMechanics, league.divisions[j].maxMechanics);
                }
            }
            opponents[i].gameKnowledge = gameKnowledge;
            opponents[i].teamPlay = teamPlay;
            opponents[i].mechanics = mechanics;
        }
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

    public void InitializeOpponents(GameManager gameManager)
    {
        opponents = CreateNewOpponents(opponentNames);
        RandomizeOpponentAttributes(opponents);
        UpdatePlayerAttributes(player, gameManager);
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

    #region Getters & Setters

    public List<Opponent> GetOpponents { get { return opponents; } }
    public int GetOpponentAmount { get { return opponentAmount; } }
    public Opponent GetPlayer { get { return player; } }

    #endregion
}
