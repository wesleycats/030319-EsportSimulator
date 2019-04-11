using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] allObjects;

    private void Awake()
    {
        allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
    }

    public void EnableAllObjects(string[] exceptionTags)
    {
        foreach (GameObject b in allObjects)
        {
            for (int i = 0; i < exceptionTags.Length; i++)
            {
                if (b.tag == exceptionTags[i]) continue;
                
                b.gameObject.SetActive(true);
            }
        }
    }

    public void EnableAllObjects()
    {
        foreach (GameObject b in allObjects)
        {
            b.gameObject.SetActive(true);
        }
    }

    public void EnableAllObjectsOf(string tag)
    {
        foreach (GameObject b in allObjects)
        {
            if (b.tag != tag) continue;

            b.gameObject.SetActive(true);
        }
    }

    public void DisableAllObjects(string[] exceptionTags)
    {
        foreach (GameObject b in allObjects)
        {
            for (int i = 0; i < exceptionTags.Length; i++)
            {
                if (b.tag == exceptionTags[i]) continue;

                b.gameObject.SetActive(false);
            }
        }
    }

    public void DisableAllObjects()
    {
        foreach (GameObject b in allObjects)
        {
            b.gameObject.SetActive(true);
        }
    }

    public void DisableAllObjectsOf(string tag)
    {
		Debug.Log(tag);
        foreach (GameObject b in allObjects)
        {
            if (b.tag != tag) continue;

            b.gameObject.SetActive(false);
        }
    }
}
