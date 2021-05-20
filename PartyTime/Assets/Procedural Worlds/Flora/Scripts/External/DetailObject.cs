using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralWorlds.Flora
{
    public class DetailObject : MonoBehaviour
    {
        public DetailObjectData m_data = new DetailObjectData();
        private FloraTerrainTile m_terrainTile;
        private void OnEnable()
        {
            FloraGlobals.onRefreshDetailObject += RefreshAll;
            if (!FloraGlobals.DetailData.Contains(m_data))
                FloraGlobals.DetailData.Add(m_data);
        }
        private void Start()
        {
            StartCoroutine(InitWait());
        }
        private void LateUpdate()
        {
            if (m_data.m_initSuccessful && m_terrainTile.m_data.PwTerrainData.IsVisible && !m_data.DetailScriptableObject.DisableDraw)
            {
                m_data.UpdateGPUCells();
                m_data.Draw();
            }
        }
        private void OnDisable()
        {
            m_data.m_initSuccessful = false;
            m_data.CleanReleaseData();
            FloraGlobals.onRefreshDetailObject -= RefreshAll;
            if (FloraGlobals.DetailData.Contains(m_data))
                FloraGlobals.DetailData.Remove(m_data);
        }
        private void OnDrawGizmos()
        {
            if (Application.isPlaying && Application.isEditor && m_data.m_initSuccessful && m_terrainTile.m_data.PwTerrainData.IsVisible && m_terrainTile.m_data.DrawDebugInfo)
            {
                CameraCellData cellData = m_terrainTile.m_data.CameraCellData;
                m_data.DrawCells(ref cellData.ActiveCellsDataCounter, ref cellData.ActiveCellsData, true);
                m_terrainTile.m_data.CameraCellData = cellData;
            }
        }
        
        public bool SupportsInstancing()
        {
            m_data.CleanReleaseData();
            if (SystemInfo.supportsInstancing == false)
                return false;
            if (m_data.TerrainTileData == null)
            {
                m_terrainTile = GetComponentInParent<FloraTerrainTile>() as FloraTerrainTile;
                m_data.TerrainTileData = m_terrainTile.m_data;
                if (m_data.TerrainTileData == null)
                    return false;
            }
            return true;
        }
        internal bool InitPool()
        {
            
            m_data.SetInitSuccessful(false);
            if (!SupportsInstancing())
                return false;
            PwTerrainData pwTerrainData = m_terrainTile.m_data.PwTerrainData;
            CameraCellData cameraCellData = m_terrainTile.m_data.CameraCellData;
            if (!cameraCellData.IsReady || !pwTerrainData.IsReady || m_data.DetailScriptableObject.Mat == null || m_data.DetailScriptableObject.Mesh == null)
            {
                m_data.SetInitSuccessful(false);
                return false;
            }
            if (m_data.DetailScriptableObject.SourceDataIndex > pwTerrainData.SplatPrototypesCount) m_data.DetailScriptableObject.SourceDataIndex = pwTerrainData.SplatPrototypesCount;
            return m_data.InitPool();
        }

        IEnumerator InitWait()
        {
            yield return null;
            yield return null;
            InitPool();
        }

        public void RefreshAll()
        {
            if (Application.isPlaying && m_terrainTile.m_data.CameraCellData.Count != 0) 
                InitPool();
        }
    }
}