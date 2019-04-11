using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTournamentContent : MonoBehaviour
{
	public Battle.Mode tournamentMode;
	public Button participateButton;
	public Text rewards;

	GameManager gameManager;

	private void OnEnable()
	{
		gameManager = FindObjectOfType<GameManager>();

		participateButton.interactable = true;

		foreach (Event e in gameManager.GetPlannedEvents)
		{
			if (tournamentMode == e.battleMode)
			{
				participateButton.interactable = false;
			}
		}
	}

	//TODO Change tournament content based on contest manager placement divisions
}
