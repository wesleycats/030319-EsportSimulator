using System.Collections;
using System;
using UnityEngine;

[Serializable]

public class Opponent
{
    public string name;
    public int eloRating;
    public int gameKnowledge;
    public int teamPlay;
    public int mechanics;
	public bool defeated;
	public int placement;

	/*public Opponent(string aName, int aEloRating, int aGameKnowledge, int aTeamplay, int aMechancis)
    {
        name = aName;
        eloRating = aEloRating;
        gameKnowledge = aGameKnowledge;
        teamPlay = aTeamplay;
        mechanics = aMechancis;
    }*/
}
