using PWCommon5;
using UnityEditor;
namespace ProceduralWorlds.Flora
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(DetailScriptableObject))]
    public class DetailScriptableObjectEditor : Editor
    {
        private DetailScriptableObject m_detailScriptableObject;
        public void OnEnable()
        {
            m_detailScriptableObject = target as DetailScriptableObject;
        }
        public override void OnInspectorGUI()
        {
            FloraEditorUtility.EditorUtils.Initialize();
            FloraEditorUtility.EditorUtils.Panel("DetailerEditor", DetailerEditor, true);
        }

        private void DetailerEditor(bool helpEnabled)
        {
            var data = m_detailScriptableObject.m_data;
            EditorGUI.BeginChangeCheck();
            {
                FloraEditorUtility.HelpEnabled = helpEnabled;
                FloraEditorUtility.DetailerEditor(data);
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(m_detailScriptableObject);
            }
        }
    }
}