using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
	public enum Type { None, Tournament }

	public Type type;
	public Battle.Mode battleMode;
	public int month;

	public Event(Type aType, Battle.Mode aBattleMode, int aMonth)
	{
		type = aType;
		battleMode = aBattleMode;
		month = aMonth;
	}
}
