using System.Collections.Generic;

[System.Serializable]
public class Training
{
	public enum Type { None, Watching, Course, CoursePlus }

	public Type type;
	public int energyCost;
    public List<Skill> skills = new List<Skill>();

	public Training(Type aType, int aEnergyCost, List<Skill> aSkills)
    {
		type = aType;
		energyCost = aEnergyCost;

		foreach (Skill s in aSkills)
			skills.Add(s);
    }

	public Training(Type aType, int aEnergyCost, Skill skill)
	{
		type = aType;
		energyCost = aEnergyCost;
		skills.Add(skill);
	}
}
