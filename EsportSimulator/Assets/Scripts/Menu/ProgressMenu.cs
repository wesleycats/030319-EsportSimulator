using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMenu : MonoBehaviour
{
	[Header("References")]
	public Text salaryText;
	public Text streamingText;
	public Text eventsText;
	public Text rentText;
	public Text upgradesText;
	public Text totalIncomeText;
	public Text totalExpensesText;
	public GameManager gameManager;

	private void OnEnable()
	{
		salaryText.text = "Salary: $" + gameManager.EarnedSalary;
		streamingText.text = "Streaming: $" + gameManager.EarnedStreamDonations;
		eventsText.text = "Events: $" + gameManager.EarnedEventRewards;

		rentText.text = "Rent: $" + gameManager.CurrentAccommodation.rent;
		upgradesText.text = "Upgrades: $" + gameManager.UpgradeExpenses;

		totalIncomeText.text = "Total: $" + GetTotalIncome().ToString();
		totalExpensesText.text = "Total: $" + GetTotalExpenses().ToString();
	}

	private int GetTotalIncome()
	{
		int totalIncome = 0;

		totalIncome += gameManager.EarnedSalary;
		totalIncome += gameManager.EarnedStreamDonations;
		totalIncome += gameManager.EarnedEventRewards;

		return totalIncome;
	}

	private int GetTotalExpenses()
	{
		int totalExpenses = 0;

		totalExpenses += gameManager.CurrentAccommodation.rent;
		totalExpenses += gameManager.UpgradeExpenses;

		return totalExpenses;
	}
}
