using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private float loadTime;

    public LerpColor switchOverlay;

    public void LoadGame(int fileIndex)
    {
        switchOverlay.LerpMaxAmount = 1;
        switchOverlay.Increasing = true;
        switchOverlay.Lerping = true;
        switchOverlay.LerpActivated = true;
        StartCoroutine(LoadChecker(loadTime, fileIndex));
    }

    private IEnumerator LoadChecker(float waitTime, int fileIndex)
    {
        yield return new WaitForSeconds(waitTime);

        if (!switchOverlay.Lerping && !switchOverlay.LerpPause && switchOverlay.LerpValue == 1)
            InitializeGameFile(fileIndex);
        else
            StartCoroutine(LoadChecker(waitTime, fileIndex));
    }

    public void InitializeGameFile(int fileIndex)
    {
        switch (fileIndex)
        {
            case 0:


                break;
            case 1:


                break;
            case 2:


                break;
            default:
                Debug.LogError("No acceptable file index has been given. Try 0, 1 or 2");

                break;

        }

        switchOverlay.LerpMaxAmount = 1;
        switchOverlay.Increasing = false;
        switchOverlay.Lerping = true;
        switchOverlay.LerpActivated = true;
    }
}
