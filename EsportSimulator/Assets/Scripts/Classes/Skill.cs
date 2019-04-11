[System.Serializable]
public class Skill
{
    public enum Type { None, GameKnowledge, TeamPlay, Mechanics }

    public Type type;
    public int amount;

    public Skill(Type aType, int aAmount)
    {
        type = aType;
        amount = aAmount;
    }
}
