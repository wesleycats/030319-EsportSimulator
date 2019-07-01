using UnityEngine;

[System.Serializable]
public class Battle
{
	public enum Mode { None, OneVersusOne, ThreeVersusThree, FiveVersusFive }

	public Mode mode;
	[Tooltip("Percentage to win given per point")]
	public float pointPercentage;
	[Tooltip("Bias to influence battle result")]
	public float bias;

	public float winPercentage;

	public Battle(Mode aMode, float aPointPercentage, float aBias)
	{
		mode = aMode;
		pointPercentage = aPointPercentage;
		bias = aBias;
	}

	public Opponent Between(Opponent o1, Opponent o2)
	{
		int[] skillsDelta = new int[0];

		int gkDelta = o1.gameKnowledge - o2.gameKnowledge;
		int mDelta = o1.mechanics - o2.mechanics;
		int tpDelta = o1.teamPlay - o2.teamPlay;
		int randomizer = GetRealRandom(0, 100);

		if (pointPercentage == 0) pointPercentage = 0.5f;

		switch (mode)
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

		//Debug.Log(mode.ToString());
		//Debug.Log(o1.name + " vs " + o2.name);
		//Debug.Log("randomizer = " + randomizer);

		winPercentage = GetWinPercentage(skillsDelta);

		if (randomizer < winPercentage)
			return o1;
		else
			return o2;
	}

	private int GetRealRandom(int min, int max)
	{
		int randomNumber = Random.Range(min, max);
		int oldNumber = randomNumber;

		while (oldNumber == randomNumber)
			randomNumber = Random.Range(min, max);

		return randomNumber;
	}

	/// <summary>
	/// Checks the delta of the skills between player and opponent and returns win percentage
	/// </summary>
	/// <param name="skills"></param>
	/// <returns></returns>
	private int GetWinPercentage(int[] skillsDelta)
	{
		float percentage = 50f;

		foreach (int s in skillsDelta)
		{
			//Debug.Log("delta " + s);
			percentage += s * pointPercentage;
		}

		//Debug.Log("win percentage = " + (percentage + bias));

		return (int)(percentage + bias);
	}
}
