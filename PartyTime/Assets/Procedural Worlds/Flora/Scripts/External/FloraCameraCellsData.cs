using System;
using UnityEngine;

namespace ProceduralWorlds.Flora
{
    public class FloraCameraCells : MonoBehaviour
    {
        public FloraCameraCellsData m_data = new FloraCameraCellsData();
        private void OnEnable()
        {
            if (!FloraGlobals.DetailData.Contains(m_data))
            {
                FloraGlobals.DetailData.Add(m_data);
            }
        }
        private void OnDisable()
        {
            if (FloraGlobals.DetailData.Contains(m_data))
            {
                FloraGlobals.DetailData.Remove(m_data);
            }
        }
    }
}
