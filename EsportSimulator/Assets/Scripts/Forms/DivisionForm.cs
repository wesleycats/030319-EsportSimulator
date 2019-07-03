[System.Serializable]
public class DivisionForm
{
	public enum DivisionType { None, Bronze, Silver, Gold, Diamond, Champion, GrandChampion }

	public string divisionName;
    public DivisionType type;
    public Battle.Mode mode;
	public int minRank;
	public int maxRank;
    public int minGameKnowledge;
    public int maxGameKnowledge;
    public int minTeamPlay;
    public int maxTeamPlay;
    public int minMechanics;
    public int maxMechanics;
    public int fameReward;
    public int moneyReward;
}
