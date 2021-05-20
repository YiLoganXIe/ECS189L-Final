using PWCommon5;
using UnityEditor;
using UnityEngine;

namespace ProceduralWorlds.Flora
{
    [CustomEditor(typeof(DetailObject))]
    public class DetailObjectEditor : PWEditor
    {
        private DetailObject m_detailObject;
        private DetailObjectData m_data;

        // We need to use and to call an instance of the default MaterialEditor
        private MaterialEditor m_materialEditor;
        private void OnEnable()
        {
            m_detailObject = (DetailObject) target;
            m_data = m_detailObject.m_data;
            if (m_data.DetailScriptableObject.Mat != null)
            {
                // Create an instance of the default MaterialEditor
                m_materialEditor = (MaterialEditor) CreateEditor(m_data.DetailScriptableObject.Mat);
            }
        }
        public override void OnInspectorGUI()
        {
            FloraEditorUtility.EditorUtils.Initialize();
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();

            FloraEditorUtility.EditorUtils.Panel("DetailerEditor", DetailerEditor, true);

            // Draw the material field of GrassTerrainObject
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                if (m_materialEditor != null)
                {
                    // Free the memory used by the previous MaterialEditor
                    DestroyImmediate(m_materialEditor);
                }
                if (m_data.DetailScriptableObject.Mat != null)
                {
                    // Create a new instance of the default MaterialEditor
                    m_materialEditor = (MaterialEditor) CreateEditor(m_data.DetailScriptableObject.Mat);
                }
                m_detailObject.RefreshAll();
            }
            if (m_materialEditor != null)
            {
                // Draw the material's foldout and the material shader field
                // Required to call _materialEditor.OnInspectorGUI ();
                m_materialEditor.DrawHeader();

                //  We need to prevent the user to edit Unity default materials
                bool isDefaultMaterial = !AssetDatabase.GetAssetPath(m_data.DetailScriptableObject.Mat).StartsWith("Assets");
                using (new EditorGUI.DisabledGroupScope(isDefaultMaterial))
                {
                    // Draw the material properties
                    // Works only if the foldout of _materialEditor.DrawHeader () is open
                    EditorGUI.BeginChangeCheck();
                    m_materialEditor.OnInspectorGUI();
                    if (EditorGUI.EndChangeCheck())
                    {
                        m_data.RefreshMaterials();
                    }
                }
            }
        }
        private void OnDisable()
        {
            if (m_materialEditor != null)
            {
                // Free the memory used by default MaterialEditor
                DestroyImmediate(m_materialEditor);
            }
        }

        private void DetailerEditor(bool helpEnabled)
        {
            FloraEditorUtility.HelpEnabled = helpEnabled;
            FloraEditorUtility.DetailerEditor(m_data.DetailScriptableObject);
        }
    }
}