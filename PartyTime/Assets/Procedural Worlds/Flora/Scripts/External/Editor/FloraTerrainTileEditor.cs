using System.Collections.Generic;
using System.IO;
using PWCommon5;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProceduralWorlds.Flora
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FloraTerrainTile))]
    public class FloraTerrainTileEditor : PWEditor
    {
        private FloraTerrainTile m_detailTerrainTile;
        private FloraTerrainTileData m_data;
        private List<bool> showPositions = new List<bool>();
        private EditorUtils m_editorUtils;

        private void OnEnable()
        {
            m_detailTerrainTile = target as FloraTerrainTile;
            if (m_editorUtils == null)
            {
                m_editorUtils = FloraApp.GetEditorUtils(this);
            }

            m_data = m_detailTerrainTile.m_data;

            showPositions = new List<bool>();
            var list = m_detailTerrainTile.m_detailObjectList;
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                showPositions.Add(false);
            }
        }
        private void OnDestroy()
        {
            if (m_editorUtils != null)
            {
                m_editorUtils.Dispose();
            }
        }
        private DetailOverrideData DrawDetailObjectData(DetailOverrideData data)
        {
            data.SourceDataType = (FloraCommonData.SourceDataType) EditorGUILayout.EnumPopup("Source Data Type", data.SourceDataType);
            data.SourceDataIndex = EditorGUILayout.IntField("Source Data Index", data.SourceDataIndex);
            data.DebugColor = EditorGUILayout.ColorField("Debug Color", data.DebugColor);
            return data;
        }
        public override void OnInspectorGUI()
        {
            m_editorUtils.Initialize();
            m_editorUtils.Panel("GlobalSettings", GlobalPanel, true);
        }

        private void GlobalPanel(bool helpEnabled)
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            {
                m_data.DetailCamera = (Camera)m_editorUtils.ObjectField("Camera", m_data.DetailCamera, typeof(Camera), true, helpEnabled);
                //m_data.TerrainType = (FloraTerrainType)m_editorUtils.EnumPopup("TerrainType", m_data.TerrainType, helpEnabled);
                m_data.UnityTerrain = (Terrain)m_editorUtils.ObjectField("Terrain", m_data.UnityTerrain, typeof(Terrain), true, helpEnabled);
                m_data.BaselineCellDensity = (FloraCommonData.CellDensity)m_editorUtils.EnumPopup("BaselineCellDensity", m_data.BaselineCellDensity, helpEnabled);
                m_data.MaximumDrawDistance = m_editorUtils.FloatField("MaximumDrawDistance", m_data.MaximumDrawDistance, helpEnabled);
                m_data.DrawDebugInfo = m_editorUtils.Toggle("DrawDebugInfo", m_data.DrawDebugInfo, helpEnabled);

                var detailObjectsListProperty = serializedObject.FindProperty("m_detailObjectList");
                EditorGUI.indentLevel++;
                m_editorUtils.PropertyField("DetailObjectList", detailObjectsListProperty, helpEnabled);
                EditorGUI.indentLevel--;

            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(m_detailTerrainTile);
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }

            if (m_editorUtils.Button("BuildDatabaseFromTerrain"))
            {
                if (EditorUtility.DisplayDialog("Build Renderers From Terrain", "Build detail renderer configuration from terrain. This option will extract existing details from your terrain and convert them to Flora detail renderers. ONLY do this if you DO NOT already have this set up on your terrain as this will override existing renderer settings.", "OK",
                    "Cancel"))
                {
                    m_detailTerrainTile.BuildDatabaseFromTerrain(m_detailTerrainTile.GetTerrain(), Shader.Find("PWS/Details/PW_Details_Foliage_URP"));
                }
            }
        }
    }
}