/******************************************************************************************
 * Name: UITabbedWindow.cs
 * Created by: Jeremy Voss
 * Created on: 01/15/2019
 * Last Modified: 01/16/2019
 * Description:
 * This script controls the hiding and showing of content tabs for each tab.  If you need
 * to add more tabs, in the hiearchy duplicate one of the existing tabs and change its
 * ID to be one higher than the current highest.  Then duplicate a content tab as well
 * and extend the Tabs count in the inspector and attach the new content tab
 * to the last slot.
 ******************************************************************************************/

 // Script has been customized for the use of this project
 // The word pane or panes has been changed to tab or tabs

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PixelsoftGames.PixelUI
{
    public class UITabbedWindow : MonoBehaviour
    {
        #region Fields & Properties

        [SerializeField]
        [Tooltip("List of content tabs for this tabbed window")]
        List<GameObject> Tabs;
		[SerializeField]
		[Tooltip("List of content in the tabs")]
		List<GameObject> contentContainers;
		[SerializeField]
        [Tooltip("The default tab to display on instantiation")]
        GameObject DefaultTab;

        // Customized
        [SerializeField]
        [Tooltip("Color the deactivted tabs will have")]
        Color deactivatedColor;
        [SerializeField]
        [Tooltip("The default color the activated tab will have")]
        Color defaultColor;

        // The currently active tab
        GameObject activeTab;

		#endregion

		#region Monobehaviour Callbacks

		private void OnEnable()
		{
			SetupContent();
		}

		private void OnDisable()
		{
			DisableContent();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Customized
		/// </summary>
		public void DisableContent()
		{
			foreach (GameObject o in contentContainers)
			{
				o.SetActive(false);
			}
		}

		/// <summary>
		/// This changes the active content tab and deactivates the previously active one.
		/// </summary>
		/// <param name="index"></param>
		public void ActivateContentTab(int index)
        {
            if (Tabs == null || index >= Tabs.Count)
            {
                Debug.LogError("Could not switch to the requested content tab because the requested tab index is out of bounds or the content tabs list is null.", gameObject);
                return;
            }

			// Customized
			if (!activeTab.GetComponent<Image>())
			{
				if (!activeTab.GetComponentInChildren<Image>()) return;

				//activeTab.transform.GetChild(0).GetComponent<Image>().color = deactivatedColor;
				//activeTab = Tabs[index];
				//activeTab.transform.GetChild(0).GetComponent<Image>().color = defaultColor;

				activeTab.transform.GetChild(0).GetComponent<Image>().color = deactivatedColor;
				activeTab.transform.GetChild(1).gameObject.SetActive(false);
				activeTab = Tabs[index];
				activeTab.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
				activeTab.transform.GetChild(1).gameObject.SetActive(true);
			}
			else
			{
				activeTab.GetComponent<Image>().color = deactivatedColor;
				activeTab = Tabs[index];
				activeTab.GetComponent<Image>().color = defaultColor;
			}

		}

        #endregion

        #region Private Methods

        /// <summary>
        /// This method is called on startup and validates the tabbed window, deactivates inactive windows and activates the default. 
        /// </summary>
        void SetupContent()
        {
            if (Tabs == null || Tabs.Count == 0)
            {
                Debug.LogError("Could not set up content tabs because the content tabs list is null or empty.", gameObject);
                return;
            }

            if (DefaultTab == null)
            {
                Debug.LogWarning("No default tab for tabbed window has been set up, choosing the first tab in the list by default", gameObject);
                DefaultTab = Tabs[0];
            }

			activeTab = DefaultTab;

			// Customized
			for (int i = 0; i < activeTab.transform.childCount; i++)
			{
				activeTab.transform.GetChild(i).gameObject.SetActive(true);
			}

			// Customized
			foreach (GameObject g in Tabs)
            {
				if (!g.GetComponent<Image>())
				{
					if (!g.GetComponentInChildren<Image>()) continue;

					if (g == activeTab)
						g.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
					else
						g.transform.GetChild(0).GetComponent<Image>().color = deactivatedColor;

					//g.transform.GetChild(0).GetComponent<Image>().color = deactivatedColor;
					//g.transform.GetChild(1).gameObject.SetActive(false);
					//g.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
					//g.transform.GetChild(1).gameObject.SetActive(true);
				}
				else
				{
					if (g == activeTab)
						g.GetComponent<Image>().color = defaultColor;
					else
						g.GetComponent<Image>().color = deactivatedColor;
				}


			}
        }

        #endregion
    }
}
