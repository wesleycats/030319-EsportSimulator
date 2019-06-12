using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
	public enum LoadType { None, NewGame, LoadGame }
	
    [SerializeField] private float loadTime;

	[Header("References")]
    public ButtonManager buttonManager;
    public UIManager uiManager;
    public GameManager gameManager;
    public GameLoader gameLoader;
	public LerpColor switchOverlay;
	public Object tutorialScene;
	public GameData gameData;

    private void OnEnable()
    {
        if (gameObject.tag == "MainMenu")
		{
            Time.timeScale = 1f;
			
			if (gameData.tutorialDone)
			{
				gameManager.ResetGame();
				gameObject.SetActive(false);
			}
		}

        if (!buttonManager) return;

        buttonManager.EnableAllButtonsOf("Navigation");
    }

    public void StartNewGame()
    {
		gameData.tutorialDone = false;
		switchOverlay.Lerp(1);
		StartCoroutine(LoadDelayer(0, LoadType.NewGame));
    }

	public void LoadGame(int slot)
	{
		switchOverlay.Lerp(1);
		StartCoroutine(LoadDelayer(slot, LoadType.LoadGame));
	}

	public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator LoadDelayer(int slot, LoadType loadType)
    {
        yield return new WaitForSeconds(loadTime);

		if (!switchOverlay.LerpActivated)
        {
			switch (loadType)
			{
				case LoadType.NewGame:
					SceneManager.LoadScene(tutorialScene.name);
					break;

				case LoadType.LoadGame:
					gameLoader.LoadGame(slot);
					break;

				default:
					Debug.Log("No load type was given.");
					break;
			}
        }
        else
            StartCoroutine(LoadDelayer(slot, loadType));
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
                RestartGame();
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
