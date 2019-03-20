using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private float loadTime;

    public ButtonManager buttonManager;
    public UIManager uiManager;
    public GameManager gameManager;
    public LerpColor switchOverlay;

    private void OnEnable()
    {
        if (gameObject.tag == "MainMenu")
            Time.timeScale = 1f;

        if (!buttonManager) return;

        buttonManager.EnableAllButtonsOf("Navigation");
    }

    public void StartNewGame()
    {
        gameManager.ResetGame();
        switchOverlay.LerpMaxAmount = 1;
        switchOverlay.Increasing = true;
        switchOverlay.Lerping = true;
        switchOverlay.LerpActivated = true;
        buttonManager.DisableAllButtons();
        StartCoroutine(LoadDelayer());
    }

    private IEnumerator LoadDelayer()
    {
        yield return new WaitForSeconds(loadTime);

        if (!switchOverlay.Lerping && switchOverlay.LerpValue == 1)
        {
            if (gameObject.tag == "MainMenu")
                gameObject.SetActive(false);

            switchOverlay.LerpMaxAmount = 1;
            switchOverlay.Increasing = false;
            switchOverlay.Lerping = true;
            switchOverlay.LerpActivated = true;
            buttonManager.EnableAllButtons();
        }
        else
        {
            StartCoroutine(LoadDelayer());
        }
    }

    public void PerformActionTag(GameObject button)
    {
        PerformActionTag(button.tag);
    }

    private void PerformActionTag(string action)
    {
        switch (action)
        {
            case "GoMainMenu":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case "GoQuit":
                Quit();
                break;

            default:
                Debug.LogError("No available action was given: " + action);
                break;
        }
    }

    private void Quit()
	{
		Application.Quit();
	}
}
