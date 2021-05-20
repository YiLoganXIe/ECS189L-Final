// Copyright © 2018 Procedural Worlds Pty Limited.  All Rights Reserved.
using UnityEngine;
using UnityEditor;
using PWCommon5;

namespace ProceduralWorlds.Flora
{
    /// <summary>
    /// Main Workflow Editor Window
    /// </summary>
    public class FloraMainWindow : EditorWindow, IPWEditor
    {
        private Vector2 m_scrollPosition = Vector2.zero;
        private EditorUtils m_editorUtils;
        private bool m_inScene;

        //private TabSet m_mainTabs;
		
        public bool PositionChecked { get; set; }
        
        #region Custom Menu Items
        /// <summary>
        /// 
        /// </summary>
        [MenuItem("Window/" + PWConst.COMMON_MENU + "/Flora/Flora...", false, 40)]
        public static void MenuDetailerMainWindow()
        {
            var window = EditorWindow.GetWindow<FloraMainWindow>(false, "Flora");
            window.minSize = new Vector2(450, 200);
            window.Show();
        }

        #endregion
        #region Constructors destructors and related delegates

        private void OnDestroy()
        {
            if (m_editorUtils != null)
            {
                m_editorUtils.Dispose();
            }
        }

        private void OnEnable()
        {
            if (m_editorUtils == null)
            {
                // Get editor utils for this
                m_editorUtils = FloraApp.GetEditorUtils(this);
            }

            m_inScene = FindObjectOfType<FloraTerrainTile>() != null;

            // Tab[] tabs = new Tab[]
            //     {
            //         new Tab("Tab 1", Tab1),
            //         new Tab("Tab 2", Tab2),
            //     };
            // m_mainTabs = new TabSet(m_editorUtils, tabs);
        }

        #endregion
        #region GUI main

        private void OnGUI()
        {
            m_editorUtils.Initialize(); // Do not remove this!
            m_editorUtils.GUIHeader();
            //Scroll
            m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition, false, false);

            m_editorUtils.Panel ("GlobalPanel", GlobalPanel, true);

            //End scroll
            GUILayout.EndScrollView();
            m_editorUtils.GUINewsFooter();
        }

        private void GlobalPanel(bool showHelp)
        {
            m_editorUtils.Heading("Introduction");

            if (m_editorUtils.Button("AddToScene"))
            {
                AddToScene();
            }

            if (!m_inScene)
            {
                GUI.enabled = false;
            }
            if (m_editorUtils.Button("BuildData"))
            {
                BuildData();
            }
            GUI.enabled = true;
        }

        private void AddToScene()
        {
            Terrain[] terrains = Terrain.activeTerrains;
            if (terrains.Length > 0)
            {
                foreach (Terrain terrain in terrains)
                {
                    if (terrain.GetComponent<FloraTerrainTile>() == null)
                    {
                        terrain.gameObject.AddComponent<FloraTerrainTile>();
                    }
                }
            }
            else
            {
                EditorUtility.DisplayDialog("No terrains found","Flora did not find any terrains in the scene. You would need to have at least one active terrain in the scene to use Flora for rendering.", "OK");
            }
            m_inScene = true;
        }

        private void BuildData()
        {
            FloraTerrainTile[] terrainTiles = GameObject.FindObjectsOfType<FloraTerrainTile>();
            if (terrainTiles.Length > 0)
            {
                foreach (FloraTerrainTile terrainTile in terrainTiles)
                {
                    terrainTile.BuildDatabaseFromTerrain(terrainTile.GetTerrain(), Shader.Find("PWS/Detailer/PW_Details_Foliage"));
                }
            }
            else
            {
                EditorUtility.DisplayDialog("No Flora terrains found", "Flora did not find any terrains that use the Flora Terrain Tile Component in the scene. Try to add the Flora Terrain Tile component to the terrains in yor scene with the 'Add Flora To Scene' Button first.", "OK");
            }
        }

        #endregion
    }
}