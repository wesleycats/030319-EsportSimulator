[System.Serializable]
public class Battle
{
	public enum Mode { None, OneVersusOne, ThreeVersusThree, FiveVersusFive }

	public Mode mode;

	public Battle(Mode aMode, int aPlannedMonth)
	{
		mode = aMode;
	}
}
