using PWCommon5;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

namespace ProceduralWorlds.Flora
{
    public class FloraEditorUtility : PWEditor
    {
        public static EditorUtils EditorUtils
        {
            get
            {
                if (m_editorUtils == null)
                {
                    m_editorUtils = FloraApp.GetEditorUtils(Editor.CreateInstance<FloraEditorUtility>());
                }

                return m_editorUtils;
            }
            set
            {
                m_editorUtils = value;
            }
        }

        [SerializeField] private static EditorUtils m_editorUtils;

        public static bool HelpEnabled;

        public static void DetailerEditor(DetailData data)
        {
            EditorUtils.Initialize();

            EditorUtils.LabelField("ObjectName", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            data.Name = EditorUtils.TextField("Name", data.Name, HelpEnabled);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorUtils.LabelField("SourceData", EditorStyles.boldLabel); 
            EditorGUI.indentLevel++;
            data.Mesh = (Mesh) EditorUtils.ObjectField("Mesh", data.Mesh, typeof(Mesh), false, HelpEnabled);
            data.Mat = (Material) EditorUtils.ObjectField("Material", data.Mat, typeof(Material), false, HelpEnabled);
            data.SourceDataType =
                (FloraCommonData.SourceDataType) EditorUtils.EnumPopup("Source Data Type", data.SourceDataType,
                    HelpEnabled);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorUtils.LabelField("Color", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            data.ColorA = EditorUtils.ColorField("Color A", data.ColorA, HelpEnabled);
            data.ColorB = EditorUtils.ColorField("Color B", data.ColorB, HelpEnabled);
            data.ColorTransition = EditorUtils.CurveField("ColorTransition", data.ColorTransition, HelpEnabled);
            data.ColorRandomization =
                EditorUtils.Slider("ColorRandomization", data.ColorRandomization, 0f, 1f, HelpEnabled);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorUtils.LabelField("Draw", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            data.DisableDraw = EditorUtils.Toggle("Disable Draw", data.DisableDraw, HelpEnabled);
            if (!data.DisableDraw)
            {
                data.DrawBias = EditorUtils.IntField("Draw Bias", data.DrawBias, HelpEnabled);
                data.StartFadeDistance = EditorUtils.FloatField("Start Fade Distance", data.StartFadeDistance, HelpEnabled);
                data.EndFadeDistance = EditorUtils.FloatField("End Fade Distance", data.EndFadeDistance, HelpEnabled);
                data.ShadowMode = (ShadowCastingMode) EditorUtils.EnumPopup("Shadow Mode", data.ShadowMode, HelpEnabled);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorUtils.LabelField("Position", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            data.RandomSeed = EditorUtils.IntField("Random Seed", data.RandomSeed, HelpEnabled);
            data.SourceDataIndex = EditorUtils.IntSlider("Source Data Index", data.SourceDataIndex, 0, 32, HelpEnabled);
            data.Density = EditorUtils.IntSlider("Density", data.Density, 2, 1023, HelpEnabled);
            data.MaxJitter = EditorUtils.Slider("Max Jitter", data.MaxJitter, 0f, 1f, HelpEnabled);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorUtils.LabelField("Scale", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            data.ScaleRangeMin = EditorUtils.FloatField("Scale Range Min", data.ScaleRangeMin, HelpEnabled);
            data.ScaleRangeMax = EditorUtils.FloatField("Scale Range Max", data.ScaleRangeMax, HelpEnabled);
            data.ScaleByAlpha = EditorUtils.CurveField("Scale By Alpha", data.ScaleByAlpha, HelpEnabled);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorUtils.LabelField("RotationsBasic", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            data.ForwardAngleBias = EditorUtils.Slider("Forward Angle Bias", data.ForwardAngleBias, 0, 360, HelpEnabled);
            data.RandomRotationRange =
                EditorUtils.Slider("Random Rotation Range", data.RandomRotationRange, 0, 1, HelpEnabled);
            data.UseAdvancedRotations =
                EditorUtils.Toggle("Use Advanced Rotations", data.UseAdvancedRotations, HelpEnabled);
            if (data.UseAdvancedRotations)
            {
                EditorGUI.indentLevel++;
                data.AlignToUp = EditorUtils.Slider("Align To Up", data.AlignToUp, 0, 1, HelpEnabled);
                data.SlopeType =
                    (DetailObjectData.SlopeType)EditorUtils.EnumPopup("Slope Type", data.SlopeType, HelpEnabled);
                data.AlignForwardToSlope =
                    EditorUtils.Slider("Align Forward To Slope", data.AlignForwardToSlope, 0, 1, HelpEnabled);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorUtils.LabelField("MaskSettings", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            data.AlphaThreshold = EditorUtils.Slider("Alpha Threshold", data.AlphaThreshold, 0, 1, HelpEnabled);

            data.UseNoise = EditorUtils.Toggle("Use Noise", data.UseNoise, HelpEnabled);
            if (data.UseNoise)
            {
                EditorGUI.indentLevel++;
                data.NoiseScale = EditorUtils.Slider("Noise Scale", data.NoiseScale, 0.001f, 1, HelpEnabled);
                data.NoiseMaskContrast =
                    EditorUtils.Slider("Noise Mask Contrast", data.NoiseMaskContrast, 0.001f, 8, HelpEnabled);
                data.NoiseMaskOffset = EditorUtils.Slider("Noise Mask Offset", data.NoiseMaskOffset, -4, 4, HelpEnabled);
                data.InvertNoise = EditorUtils.Toggle("Invert Noise", data.InvertNoise, HelpEnabled);
                EditorGUI.indentLevel--;
            }

            data.UseHeight = EditorUtils.Toggle("Use Height", data.UseHeight, HelpEnabled);
            if (data.UseHeight)
            {
                EditorGUI.indentLevel++;
                data.ScaleByHeight = EditorUtils.CurveField("Scale By Height", data.ScaleByHeight, HelpEnabled);
                EditorGUI.indentLevel--;
            }

            data.UseSlope = EditorUtils.Toggle("Use Slope", data.UseSlope, HelpEnabled);
            if (data.UseSlope)
            {
                EditorGUI.indentLevel++;
                data.ScaleBySlope = EditorUtils.CurveField("Scale By Slope", data.ScaleBySlope, HelpEnabled);
                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();


            EditorUtils.LabelField("Debug", EditorStyles.boldLabel); 
            EditorGUI.indentLevel++;
            data.DrawCPUSpawnLocations =
                EditorUtils.Toggle("Draw CPU Spawn Locations", data.DrawCPUSpawnLocations, HelpEnabled);
            if (data.DrawCPUSpawnLocations)
            {
                EditorGUI.indentLevel++;
                data.DebugColor = EditorUtils.ColorField("Debug Color", data.DebugColor, HelpEnabled);
                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }
    }
}