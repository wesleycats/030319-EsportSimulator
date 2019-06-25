using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Tutorial
{
	public string tutorialName;
	[Tooltip("Used for customizable order")]
	public int orderIndex;
	public TextAsset textFile;
	public List<CustomTransform> customTransforms = new List<CustomTransform>();
	public List<FocusObject> focusObjects = new List<FocusObject>();
	public bool pause;
	public bool done;
}
