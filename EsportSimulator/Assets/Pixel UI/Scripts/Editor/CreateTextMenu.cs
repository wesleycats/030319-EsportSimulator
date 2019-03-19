/******************************************************************************************
 * Name: CreateTextMenu.cs
 * Created by: Jeremy Voss
 * Created on: 01/15/2019
 * Last Modified: 02/04/2019
 * Description:
 * Creates a menu for adding text prefabs to the scene.
 ******************************************************************************************/
using UnityEngine;
using UnityEditor;
using System;

namespace PixelsoftGames.PixelUI
{
    [ExecuteInEditMode]
    public class CreateTextMenu : MonoBehaviour
    {
        #region Fields & Properties

        const string skinName = "Text";
        const string skinPath = "Prefabs/Text/";
        const string path = "Prefabs/";

        #endregion

        #if UNITY_EDITOR
            #region Private Static Methods

            [MenuItem("Pixel UI/Create/" + skinName + "/PixelArt Text (External)")]
            static void CreatePixelArtTextExternal()
            {
                InstantiateObj(skinPath + "PixelArt Text (External)");
            }

            [MenuItem("Pixel UI/Create/" + skinName + "/PixelArt Text (Internal)")]
            static void CreatePixelArtTextInternal()
            {
                InstantiateObj(skinPath + "PixelArt Text (Internal)");
            }

            [MenuItem("Pixel UI/Create/" + skinName + "/Visitor Text (External)")]
            static void CreateVisitorTextExternal()
            {
                InstantiateObj(skinPath + "Visitor Text (External)");
            }

            [MenuItem("Pixel UI/Create/" + skinName + "/Visitor Text (Internal)")]
            static void CreateVisitorTextInternal()
            {
                InstantiateObj(skinPath + "Visitor Text (Internal)");
            }

            /// <summary>
            /// Retrieves prefabs from resources and instantiates on a canvas.
            /// </summary>
            static void InstantiateObj(string fullPath)
            {
                var prefab = Resources.Load(fullPath);

                UnityEngine.Object instance = null;
                if (Selection.activeObject != null)
                    instance = Instantiate(prefab, Selection.activeTransform, false);
                else
                {
                    Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                    if (!canvas)
                        canvas = CreateBaseMenu.InstantiateCanvas().gameObject.GetComponent<Canvas>();
                    instance = Instantiate(prefab, canvas.transform, false);
                }

                Selection.objects = new UnityEngine.Object[] { instance };
            }

        #endregion
#endif
    }
}