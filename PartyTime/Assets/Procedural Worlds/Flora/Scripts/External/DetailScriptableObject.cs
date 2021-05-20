using UnityEngine;

namespace ProceduralWorlds.Flora
{
    [CreateAssetMenu(menuName = "Procedural Worlds/Flora/Detail Object")]
    public class DetailScriptableObject : ScriptableObject
    {
        public DetailData m_data;
    }
}