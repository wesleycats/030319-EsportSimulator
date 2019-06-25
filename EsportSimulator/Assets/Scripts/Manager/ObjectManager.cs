using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public void EnableAllObjects(string[] exceptionTags)
	{
		foreach (GameObject o in GameObject.FindObjectsOfType<GameObject>())
		{
			for (int i = 0; i < exceptionTags.Length; i++)
			{
				if (o.tag == exceptionTags[i]) continue;

				o.gameObject.SetActive(true);
			}
		}
	}

    public void EnableAllObjects()
	{
		foreach (GameObject o in GameObject.FindObjectsOfType<GameObject>())
		{
			o.gameObject.SetActive(true);
		}
	}

    public void EnableAllObjectsOf(string tag)
	{
		foreach (GameObject o in GameObject.FindGameObjectsWithTag(tag))
		{
			o.gameObject.SetActive(true);
		}
	}

    public void DisableAllObjects(string[] exceptionTags)
	{
		foreach (GameObject o in GameObject.FindObjectsOfType<GameObject>())
		{
			for (int i = 0; i < exceptionTags.Length; i++)
            {
                if (o.tag == exceptionTags[i]) continue;

                o.gameObject.SetActive(false);
            }
        }
    }

    public void DisableAllObjects()
	{
		foreach (GameObject o in GameObject.FindObjectsOfType<GameObject>())
		{
			o.gameObject.SetActive(false);
		}
	}

    public void DisableAllObjectsOf(string tag)
	{
        foreach (GameObject o in GameObject.FindGameObjectsWithTag(tag))
        {
            o.gameObject.SetActive(false);
        }
    }
}
