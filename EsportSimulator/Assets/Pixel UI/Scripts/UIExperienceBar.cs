﻿/******************************************************************************************
 * Name: UIExperienceBar.cs
 * Created by: Jeremy Voss
 * Created on: 01/15/2019
 * Last Modified: 01/16/2019
 * Description:
 * This script serves 2 purposes.  The first is to manage the experience bar, while the
 * second manages the experience table.  Tweaking the settings from the inspector will
 * result in the creation of a unique exp table for your settings when the script executes
 * in runtime.
 ******************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace PixelsoftGames.PixelUI
{
    [RequireComponent(typeof(Slider))]
    public class UIExperienceBar : MonoBehaviour
    {
        #region Events

        public delegate void ExperienceBarEvent(UIExperienceBar expBar);
        /// <summary>
        /// This event gets called when enough experience points have
        /// been accumulated to level up the bar.
        /// </summary>
        public static event ExperienceBarEvent LevelUp;

        #endregion

        #region Fields & Properties

        [SerializeField]
        [Range(0, 4)]
        [Tooltip("The default level to begin with (i.e. - we start the game as a level 1 player)")]
        int DefaultLevel = 0;
        [SerializeField]
        [Range(0, 4)]
        [Tooltip("The maximum possible level that can be achieved.")]
        int MaximumLevel = 100;
        [SerializeField]
        [Tooltip("The base experience required to gain the first level")]
        int BaseExperience = 1000;
        [SerializeField]
        [Tooltip("How should the exp required to level for each level be staggered")]
        float TableStagger = 1.5f;

        [SerializeField] int[] expTable;
        int currentExperienceTowardsLevel;
        int currentLevel;
        Slider slider;

        /// <summary>
        /// Returns the amount of experience points earned towards the next level.
        /// </summary>
        public int GetExperienceTowardsLevel { get { return currentExperienceTowardsLevel; } }
        /// <summary>
        /// Returns the amount of experience points still required to level up.
        /// </summary>
        public int GetExperienceToNextLevel { get { return expTable[currentLevel] - currentExperienceTowardsLevel; } }
        /// <summary>
        /// Returns the current level.
        /// </summary>
        public int GetCurrentLevel { get { return currentLevel; } }

        #endregion

        #region Monobehaviour Callbacks

        // Use this for initialization
        void Start()
        {
			//Initialize();
        }

		#endregion

		#region Public Methods

		public void Initialize()
		{
			slider = GetComponent<Slider>();
			currentLevel = DefaultLevel;
			currentExperienceTowardsLevel = 0;
			CreateTable();
			UpdateValue();
		}

		/// <summary>
		/// This method will grant experience points in the given amount of fire a level up event if necessary.
		/// </summary>
		/// <param name="amount"></param>
		public void GiveExperiencePoints(int amount)
        {
            currentExperienceTowardsLevel += amount;

			while (currentExperienceTowardsLevel >= expTable[currentLevel])
            {
				currentExperienceTowardsLevel -= expTable[currentLevel];
                currentLevel++;

                if (LevelUp != null)
                    LevelUp(this);
            }

			FindObjectOfType<GameManager>().WorkLevel = currentLevel;

			UpdateValue();
        }

        /// <summary>
        /// This method will return the total amount of experience points earned.
        /// </summary>
        /// <returns></returns>
        public int GetTotalExperiencePoints()
        {
            int total = 0;

            for (int i = 0; i < currentLevel; i++)
                total += expTable[i];
            total += currentExperienceTowardsLevel;

            return total;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method will create an experience points table from level 1 to the maximum level.
        /// </summary>
        void CreateTable()
        {
            expTable = new int[MaximumLevel];
            for (int i = 0; i < expTable.Length; i++)
                expTable[i] = Mathf.FloorToInt(BaseExperience * Mathf.Pow(i+1, TableStagger));
        }

        /// <summary>
        /// This will update the slider experience point value.
        /// </summary>
        void UpdateValue()
        {
			slider.value = (float)currentExperienceTowardsLevel / expTable[currentLevel];
        }

        #endregion
    }
}