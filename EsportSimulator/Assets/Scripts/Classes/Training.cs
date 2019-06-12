using System.Collections.Generic;

[System.Serializable]
public class Training
{
	public enum Type { None, Watching, Course, CoursePlus }

    public List<Skill> skills = new List<Skill>();
	public Type type;

	public Training(List<Skill> aSkills, Type aType)
    {
		skills = aSkills;
        type = aType;
    }

	public Training(Skill skill, Type aType)
	{
		skills.Add(skill);
		type = aType;
	}
}
