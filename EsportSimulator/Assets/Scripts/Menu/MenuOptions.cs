using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
	public GameLoader.LoadType loadType;

    [SerializeField] private float loadTime;
	private int slotToLoad;

	[Header("References")]
	public int tutorialSceneIndex;
    public ButtonManager buttonManager;
    public UIManager uiManager;
    public GameManager gameManager;
    public GameLoader gameLoader;
	public LerpColor switchOverlay;
	public GameData gameData;

	private AudioManager audioManager;

	private void Awake()
	{
		audioManager = FindObjectOfType<AudioManager>();
	}

	private void Start()
	{
		switchOverlay.LerpStopped += LoadInGame;

		if (gameObject.tag == "MainMenu")
		{
			Time.timeScale = 1f;
			audioManager.CurrentPlaylist = audioManager.mainMenuClips;

			if (gameData.tutorialDone)
			{
				gameManager.ResetGame();
				audioManager.CurrentPlaylist = audioManager.mainClips;
				gameObject.SetActive(false);
			}
		}
	}

	private void OnEnable()
    {
        if (!buttonManager) return;

        buttonManager.EnableAllButtonsOf("Navigation");
    }

	public void StartNewGame()
	{
		loadType = GameLoader.LoadType.NewGame;
		audioManager.FadeOut();
		switchOverlay.Lerp(1);
	}

	public void LoadGame(int slot)
	{
		loadType = GameLoader.LoadType.LoadGame;
		slotToLoad = slot;
		audioManager.FadeOut();
		switchOverlay.Lerp(1);
	}

	public void LoadInGame(bool b)
	{
		if (!b || loadType == GameLoader.LoadType.None) return;

		switch (loadType)
		{
			case GameLoader.LoadType.NewGame:
				SceneManager.LoadScene(tutorialSceneIndex);
				break;

			case GameLoader.LoadType.LoadGame:
				gameLoader.LoadGame(slotToLoad);
				break;

			default:
				Debug.LogError("Given load type is unavailable");
				break;
		}
	}

	public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
