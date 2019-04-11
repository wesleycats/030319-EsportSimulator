using UnityEngine;

[System.Serializable]
public class Tutorial
{
	public Type type;
	[Tooltip("Used for customizable order")]
	public int orderIndex;
	public TextAsset textFile;
	public bool done;

	public enum Type { None, Introduction, WinCondition, LoseCondition, Calender, Progress, Needs, Skills, Work, Stream, Battle, Contest, Items, Accommodations }
}
