using UnityEngine;

[System.Serializable]
public class Item
{
    public enum Type { None, Mouse, Keyboard, Headset, Guide, Screen }
    public enum Quality { Default, Bad, Standard, Good, Excellent }

    public Type type;
    public Quality quality;
	public int cost;
    public Skill[] skills;
	public Sprite sprite;

	public Item(Type aType, Quality aQuality, int aCost, Skill[] aSkills, Sprite aSprite)
	{
		type = aType;
		quality = aQuality;
		cost = aCost;
		skills = aSkills;
		sprite = aSprite;
	}
}
