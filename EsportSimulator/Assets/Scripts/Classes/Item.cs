[System.Serializable]
public class Item
{
    public enum Type { None, Mouse, Keyboard, Headset, Guide, Screen }
    public enum Quality { Default, Bad, Standard, Good, Excellent }

    public Type type;
    public Quality quality;
    public Skill[] skills;

    private int itemLevel;

    public Item(Type aType, Quality aQuality, Skill[] aSkills)
    {
        type = aType;
        quality = aQuality;
        skills = aSkills;
        itemLevel = (int)aQuality;
    }

    #region Getters & Setters

    public int ItemLevel { get { return itemLevel; } }

    #endregion
}
