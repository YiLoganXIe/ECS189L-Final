using System.Collections.Generic;
using UnityEngine;
namespace ProceduralWorlds.Flora
{
    [CreateAssetMenu(fileName = "FloraSettings", menuName = "Procedural Worlds/Flora/Flora Settings", order = 0)]
    public class FloraSettings : ScriptableObject
    {
        [System.Serializable]
        public class ShaderProfiles
        {
            public Object Builtin;
            public Object URP;
            public Object HDRP;
            
           [HideInInspector]public string BuiltinGUID;
           [HideInInspector]public string UrpGUID;
           [HideInInspector]public string HdrpGUID;
        }
        
        [SerializeField]
        public List<ShaderProfiles> shaderProfiles;
    }
    
    
}