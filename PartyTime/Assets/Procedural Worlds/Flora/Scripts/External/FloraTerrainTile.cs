using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ProceduralWorlds.Flora
{
    /// <summary>
    /// Information about details
    /// </summary>
    [Serializable]
    public struct DetailOverrideData
    {
        public DetailScriptableObject DetailScriptableObject;
        public FloraCommonData.SourceDataType SourceDataType;
        public int SourceDataIndex;
        public Color DebugColor;
    }

    public class FloraTerrainTile : MonoBehaviour
    {
        public FloraTerrainTileData m_data = new FloraTerrainTileData();
        public List<DetailOverrideData> m_detailObjectList = new List<DetailOverrideData>();
        private FloraCameraCells m_detailCameraCells;
        public FloraCameraCellsData DetailCameraCellsData => m_detailCameraCells.m_data;

        private void DrawDebug()
        {
            var cameraCellData = m_data.CameraCellData;
            var pwTerrainData = m_data.PwTerrainData;
            DetailCameraCellsData.DrawDebug(ref cameraCellData, ref pwTerrainData);
            m_data.CameraCellData = cameraCellData;
            m_data.PwTerrainData = pwTerrainData;
        }

        private void OnEnable()
        {
            if (!FloraGlobals.DetailData.Contains(m_data))
            {
                FloraGlobals.DetailData.Add(m_data);
            }

            Refresh();
            FloraGlobals.onRefreshDetailTerrainTile += Refresh;
        }

        private void OnDisable()
        {
            if (FloraGlobals.DetailData.Contains(m_data))
            {
                FloraGlobals.DetailData.Remove(m_data);
            }

            FloraGlobals.onRefreshDetailTerrainTile += Refresh;
            CleanUpCameraCells();
        }
        
        public enum FLORARP
        {
            Builtin,
            URP,
            HDRP
        }

        private FLORARP m_floaraRP;
        private FloraSettings m_floraSettings;
        
        private FLORARP GetRenderPipline()
        {
            FLORARP rp;

            if (GraphicsSettings.currentRenderPipeline == null)
            {
                rp = FLORARP.Builtin;
            }
            else if (GraphicsSettings.currentRenderPipeline.GetType().ToString().Contains("HighDefinition"))
            {
                rp = FLORARP.HDRP;
            }
            else if (GraphicsSettings.currentRenderPipeline.GetType().ToString().Contains("Universal"))
            {
                rp = FLORARP.URP;
            }
            else
            {
                rp = FLORARP.Builtin;
            }
            return rp;
        }

        private FloraSettings GetFloraSettings()
        {
#if UNITY_EDITOR
            FloraSettings fs;
            var assetPath = AssetDatabase.FindAssets("t:FloraSettings", new string[] {"Assets/Procedural Worlds/Flora/Content Resources/Settings"});
            if (assetPath.Length > 0)
            {
                fs = (FloraSettings) AssetDatabase.LoadAssetAtPath<FloraSettings>(AssetDatabase.GUIDToAssetPath(assetPath[0]));
                return fs;
            }
            else
            {
                return null;
            }
#else
            return null;
#endif
        }

        private Shader setShader(Shader currentshader, FloraSettings.ShaderProfiles shaderProfiles )
        {
            switch (m_floaraRP)
            {
                case FLORARP.Builtin:
                    return shaderProfiles.Builtin as Shader;
                case FLORARP.URP:
                    return shaderProfiles.URP as Shader;
                case FLORARP.HDRP:
                    return shaderProfiles.HDRP as Shader;
            }
            return currentshader;
        }

        private string GetGuid(Object obj)
        {
            string guid = "";
#if UNITY_EDITOR
            long localId;
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out guid, out localId);
#endif
            return guid;
        }
        

        private Shader GetShaderForPipeline( Material mat , FloraSettings fs)
        {
            var currentShader = mat.shader;
            if (fs != null)
            {
                var currentShaderGuid = GetGuid((Object)currentShader);
                foreach (var shadergroup in fs.shaderProfiles)
                {
                    if (shadergroup.BuiltinGUID == currentShaderGuid || shadergroup.UrpGUID == currentShaderGuid || shadergroup.HdrpGUID == currentShaderGuid)
                    {
                        return setShader(currentShader, shadergroup);
                    }
                }
            }
            return currentShader;
        }
        

        private void Start()
        {
            var pwTerrainData = m_data.PwTerrainData;
            var detailObjectsList = m_detailObjectList;

            m_floaraRP = GetRenderPipline();
            m_floraSettings = GetFloraSettings();
            
            
            if (pwTerrainData.IsReady && m_data.CameraCellData.IsReady)
            {
                for (int i = 0; i < m_detailObjectList.Count; i++)
                {
                    var detailObjectData = m_detailObjectList[i];
                    detailObjectData.DetailScriptableObject.m_data.Mat.shader = GetShaderForPipeline(detailObjectData.DetailScriptableObject.m_data.Mat, m_floraSettings);
                    float[,,] alphaMap = m_data.GetAlpha(ref pwTerrainData, ref detailObjectData.SourceDataType);
                    if (alphaMap.GetLength(2) >= detailObjectData.SourceDataIndex)
                    {
                        var obj = new GameObject(detailObjectData.DetailScriptableObject.m_data.Name);
                        obj.transform.parent = this.transform;
                        var detailObj = obj.AddComponent<DetailObject>();
                        detailObj.m_data.DetailScriptableObject = detailObjectData.DetailScriptableObject.m_data;
                        var detailScriptableObject = detailObj.m_data.DetailScriptableObject;
                        detailScriptableObject.DebugColor = detailObjectData.DebugColor;
                        detailScriptableObject.DebugCellSize =
                            0.6f / (float) detailObjectsList.Count * (float) i + 0.2f;
                        detailScriptableObject.SourceDataType = detailObjectData.SourceDataType;
                        detailScriptableObject.SourceDataIndex = detailObjectData.SourceDataIndex;
                        var detailScriptableDebugColor = detailScriptableObject.DebugColor;
                        if (detailScriptableDebugColor.r == 0 && detailScriptableDebugColor.g == 0 &&
                            detailScriptableDebugColor.b == 0)
                        {
                            var col = new Vector3(Random.value, Random.value, Random.value).normalized;
                            detailScriptableObject.DebugColor = new Color(col.x, col.y, col.z, 1f);
                            var detailObjectDataDebugColor = detailObjectData.DebugColor;
                            detailObjectDataDebugColor.r = col.x;
                            detailObjectData.DebugColor = detailObjectDataDebugColor;
                        }

                        detailScriptableObject.DebugColor = detailScriptableDebugColor;
                    }
                    else
                    {
                        Debug.LogWarning("Requested index of " + detailObjectData.SourceDataIndex.ToString() +
                                         " of the type " + detailObjectData.ToString() +
                                         " is not in range.  Detail Instance not created on Terrain tile " + this.name);
                    }
                }
            }

            m_data.PwTerrainData = pwTerrainData;
        }

        private Plane[] cFPlanes = new Plane[6];

        private void Update()
        {
            var pwTerrainData = m_data.PwTerrainData;
            var cameraCellData = m_data.CameraCellData;
            var detailCamera = m_data.DetailCamera;
            if (pwTerrainData.IsReady && cameraCellData.IsReady)
            {
                m_data.CalculateFrustumPlanes(cFPlanes, detailCamera); // Generating GC in Editor
                bool boundsTest = m_data.BoundsTest(detailCamera, cFPlanes);
                bool distanceTest = m_data.DistanceTest(pwTerrainData.MAXDrawDistMinusExtentsSqr,
                    detailCamera.transform.position, pwTerrainData.Bounds.center);
                if (boundsTest && distanceTest)
                    pwTerrainData.IsVisible = true;
                else
                    pwTerrainData.IsVisible = false;
                if (pwTerrainData.IsVisible)
                    DetailCameraCellsData.CameraCellUpdate(detailCamera, cFPlanes, ref cameraCellData,
                        ref pwTerrainData);
            }

            m_data.PwTerrainData = pwTerrainData;
            m_data.CameraCellData = cameraCellData;
        }

        internal void Refresh()
        {
            // Get Init Terrain
            InitTerrain();
            //Init CameraCells
            InitCameraCells();
            FloraGlobals.SetShaderGlobals();
        }

        private void OnDrawGizmos()
        {
            var drawDebugInfo = m_data.DrawDebugInfo;
            var detailCamera = m_data.DetailCamera;
            if (Application.isPlaying && Application.isEditor && m_data.PwTerrainData.IsReady &&
                m_data.CameraCellData.IsReady && drawDebugInfo)
            {
                if (m_data.PwTerrainData.IsVisible)
                {
                    DrawDebug();
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(m_data.PwTerrainData.Bounds.center, m_data.PwTerrainData.Bounds.size);
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(m_data.PwTerrainData.Bounds.center, m_data.PwTerrainData.Bounds.size * 0.25f);
                    Gizmos.DrawLine(m_data.PwTerrainData.Bounds.center, detailCamera.transform.position);
                }
                else
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawWireCube(m_data.PwTerrainData.Bounds.center, m_data.PwTerrainData.Bounds.size);
                }

                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(detailCamera.transform.position, Vector3.one * 30f);
            }
        }

        internal bool InitCameraCells()
        {
            CleanUpCameraCells();
            var cameraCellData = m_data.CameraCellData;
            cameraCellData.IsReady = false;
            if (m_data.FindCamera())
            {
                var pwTerrainData = m_data.PwTerrainData;
                var baselineCellDensity = m_data.BaselineCellDensity;
                m_detailCameraCells = gameObject.AddComponent<FloraCameraCells>();

                int cellDensity = (int) baselineCellDensity;
                if (FloraGlobals.Settings.CameraCellGlobalSubdivisionModifier > 0)
                {
                    cellDensity =
                        Math.Min(
                            (int) baselineCellDensity << FloraGlobals.Settings.CameraCellGlobalSubdivisionModifier,
                            64);
                }
                else if (FloraGlobals.Settings.CameraCellGlobalSubdivisionModifier < 0)
                {
                    cellDensity =
                        Math.Max(
                            (int) baselineCellDensity >>
                            Math.Abs(FloraGlobals.Settings.CameraCellGlobalSubdivisionModifier), 2);
                }

                DetailCameraCellsData.InitCells(ref cameraCellData, ref pwTerrainData, cellDensity);
                DetailCameraCellsData.BuildCameraCells(ref cameraCellData, ref pwTerrainData);
                cameraCellData.IsReady = true;
                m_data.PwTerrainData = pwTerrainData;
            }
            else
            {
                Debug.LogWarning(" Procedural Worlds Details Camera init failed to find camera");
            }

            m_data.CameraCellData = cameraCellData;
            return m_data.CameraCellData.IsReady;
        }

        internal bool InitTerrain()
        {
            var pwTerrainData = m_data.PwTerrainData;
            pwTerrainData.IsReady = false;
            switch (m_data.TerrainType)
            {
                case FloraTerrainType.Unity:
                    var terrain = m_data.UnityTerrain;
                    FindUnityTerrain(ref terrain);
                    m_data.CaptureUnityTerrain(ref terrain, ref pwTerrainData);
                    m_data.UnityTerrain = terrain;
                    if (m_data.UnityTerrain != null)
                        pwTerrainData.IsReady = true;
                    break;
            }

            m_data.PwTerrainData = pwTerrainData;
            return m_data.PwTerrainData.IsReady;
        }

        private void FindUnityTerrain(ref Terrain terrain)
        {
            if (terrain == null)
                terrain = GetComponentInParent<Terrain>();
            if (terrain == null)
                terrain = GetComponentInChildren<Terrain>();
            if (terrain == null)
                terrain = FindObjectOfType<Terrain>();
            if (terrain == null)
                Debug.LogWarning(" Procedural Worlds Detail Manager failed to find suitable terrain");
        }

        public void CleanUpCameraCells()
        {
            if (m_detailCameraCells != null)
                Destroy(m_detailCameraCells);
        }

        /// <summary>
        /// Gets the terrain that the system uses + assigns the terrain it if it's null
        /// </summary>
        /// <returns></returns>
        public Terrain GetTerrain()
        {
            if (m_data.UnityTerrain == null)
            {
                m_data.UnityTerrain = GetComponent<Terrain>();
            }

            return m_data.UnityTerrain;
        }

#region Data Building

#if UNITY_EDITOR
        /// <summary>
        /// Builds a database from a terrain taking all the terrain details and building the material and scriptable object data
        /// </summary>
        /// <param name="terrain"></param>
        /// <param name="shader"></param>
        /// <param name="saveGeneratedAssets"></param>
        public void BuildDatabaseFromTerrain(Terrain terrain, Shader shader, bool saveGeneratedAssets = true)
        {
            if (terrain != null && shader != null)
            {
                DetailPrototype[] detailPrototypes = terrain.terrainData.detailPrototypes;
                if (detailPrototypes.Length > 0)
                {
                    m_detailObjectList.Clear();
                    for (int i = 0; i < detailPrototypes.Length; i++)
                    {
                        DetailPrototype currentPrototype = detailPrototypes[i];
                        if (currentPrototype.prototypeTexture != null)
                        {
                            Material generalMaterial = new Material(shader);
                            generalMaterial.SetTexture("_BaseColorMap", currentPrototype.prototypeTexture);
                            generalMaterial.SetTexture("_NormalMap", UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(GetAssetPath("PW_Billboard_N.png")));
                            generalMaterial.SetTexture("_MaskMap", UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(GetAssetPath("PW_Billboard_M.png")));
                            generalMaterial.SetColor("_BaseColor", Color.white);
                            generalMaterial.SetFloat("_PW_SF_WIND", 1f);

                            Mesh mesh = UnityEditor.AssetDatabase.LoadAssetAtPath<Mesh>(GetAssetPath("PW_DetailQuad.fbx"));
                            DetailScriptableObject scriptableObject = ScriptableObject.CreateInstance<DetailScriptableObject>();
                            string name = currentPrototype.prototypeTexture.name + " PW Grass Data";
                            Color debugColor = GetDebugColor(i + 1);

                            Color colorA = currentPrototype.healthyColor;
                            colorA.a = 0.05f;
                            Color colorB = currentPrototype.dryColor;
                            colorB.a = 0.05f;
                            scriptableObject.name = name;
                            scriptableObject.m_data = new DetailData
                            {
                                Name = name,
                                Mesh = mesh,
                                Mat = generalMaterial,
                                SourceDataType = FloraCommonData.SourceDataType.Detail,
                                SourceDataIndex = i,
                                ColorA = colorA,
                                ColorB = colorB,
                                StartFadeDistance = 25f,
                                EndFadeDistance = terrain.detailObjectDistance,
                                ShadowMode = ShadowCastingMode.TwoSided,
                                Density = Mathf.Clamp(Mathf.RoundToInt(terrain.terrainData.size.x * 4f * terrain.detailObjectDensity), 1, 256),
                                MaxJitter = 1f,
                                ScaleRangeMin = currentPrototype.minWidth,
                                ScaleRangeMax = currentPrototype.maxWidth,
                                DebugColor = debugColor, 
                                UseNoise = true
                            };
                            m_detailObjectList.Add(new DetailOverrideData
                            {
                                SourceDataIndex = i, 
                                SourceDataType = FloraCommonData.SourceDataType.Detail,
                                DetailScriptableObject = scriptableObject, 
                                DebugColor = debugColor
                            });
                        }
                    }

                    UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(terrain.gameObject.scene);
                    EditorUtility.SetDirty(this);

                    if (saveGeneratedAssets)
                    {
                        SaveGeneratedAssets();
                    }
                }
            }
        }
        /// <summary>
        /// Saves all grass data in the list to assets
        /// </summary>
        private void SaveGeneratedAssets()
        {
            string savePath = EditorUtility.SaveFolderPanel("Saved Detailer Files", Application.dataPath, "");
            if (!string.IsNullOrEmpty(savePath))
            {
                savePath = savePath.Replace(Application.dataPath, "Assets");
                if (savePath.EndsWith("Assets"))
                {
                    savePath += "/";
                }
                else if (!savePath.EndsWith("/"))
                {
                    savePath += "/";
                }
            }
            else
            {
                savePath = "Assets/";
            }

            if (m_detailObjectList.Count > 0)
            {
                for (int i = 0; i < m_detailObjectList.Count; i++)
                {
                    DetailOverrideData overrideData = m_detailObjectList[i];
                    if (overrideData.DetailScriptableObject != null)
                    {
                        string scriptableObjectPath = savePath + overrideData.DetailScriptableObject.name + ".asset";
                        string materialPath = savePath + overrideData.DetailScriptableObject.m_data.Name + ".mat";
                        AssetDatabase.CreateAsset(overrideData.DetailScriptableObject, scriptableObjectPath);
                        if (overrideData.DetailScriptableObject.m_data.Mat != null)
                        {
                            AssetDatabase.CreateAsset(overrideData.DetailScriptableObject.m_data.Mat, materialPath);
                        }

                        overrideData.DetailScriptableObject = AssetDatabase.LoadAssetAtPath<DetailScriptableObject>(scriptableObjectPath);
                        overrideData.DetailScriptableObject.m_data.Mat = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
                        m_detailObjectList[i] = overrideData;
                    }
                }
            }
        }
        /// <summary>
        /// Gets a debug Color
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        private Color GetDebugColor(int idx)
        {
            Gradient gradient = new Gradient
            {
                mode = GradientMode.Blend
            };
            GradientColorKey[] keys = new GradientColorKey[5];
            keys[0].time = 0f;
            keys[0].color = Color.white;

            keys[1].time = 0.25f;
            keys[1].color = Color.blue;

            keys[2].time = 0.5f;
            keys[2].color = Color.magenta;

            keys[3].time = 0.75f;
            keys[3].color = Color.red;

            keys[4].time = 1f;
            keys[4].color = Color.yellow;

            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0].alpha = 1f;
            alphaKeys[0].time = 0f;
            gradient.SetKeys(keys, alphaKeys);

            return gradient.Evaluate((float) 1f / idx);
        }
        /// <summary>
        /// Get the asset path of the first thing that matches the name
        /// </summary>
        /// <param name="fileName">File name to search for</param>
        /// <returns></returns>
        public static string GetAssetPath(string fileName)
        {
            string fName = Path.GetFileNameWithoutExtension(fileName);
            string[] assets = AssetDatabase.FindAssets(fName, null);
            for (int idx = 0; idx < assets.Length; idx++)
            {
                string path = AssetDatabase.GUIDToAssetPath(assets[idx]);
                if (Path.GetFileName(path) == fileName)
                {
                    return path;
                }
            }
            return "";
        }
#endif

#endregion
    }
}