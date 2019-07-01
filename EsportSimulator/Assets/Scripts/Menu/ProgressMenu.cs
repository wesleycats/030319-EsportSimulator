using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMenu : MonoBehaviour
{
	[Header("References")]
	public Text salaryText;
	public Text rentText;
	public Text totalIncomeText;
	public Text totalExpensesText;
	public GameManager gameManager;

	private int totalIncome;
	private int totalExpenses;

	private void OnEnable()
	{
		salaryText.text = "Salary: $" + gameManager.EarnedSalary;
		rentText.text = "Rent: $" + gameManager.CurrentAccommodation.rent;
		totalIncomeText.text = "Total: $" + GetTotalIncome().ToString();
		totalExpensesText.text = "Total: $" + GetTotalExpenses().ToString();
	}

	private int GetTotalIncome()
	{
		totalIncome = 0;

		totalIncome += gameManager.EarnedSalary;

		return totalIncome;
	}

	private int GetTotalExpenses()
	{
		totalExpenses = 0;

		totalExpenses += gameManager.CurrentAccommodation.rent;

		return totalExpenses;
	}
}
