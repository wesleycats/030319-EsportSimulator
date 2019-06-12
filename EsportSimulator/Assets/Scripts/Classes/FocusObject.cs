using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FocusObject
{
	public GameObject gObject;
	public bool wasActive;
	public bool waitForInput;
	public bool interactable;
	public int focusTextIndex;
	public int originalOrder;
	public bool isCustomSortOrder;
	public int customSortOrder;
}
