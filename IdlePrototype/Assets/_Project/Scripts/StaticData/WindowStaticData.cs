using IdleArcade.StaticData.Windows;
using System.Collections.Generic;
using UnityEngine;

namespace IdleArcade.StaticData.Windows
{
    [CreateAssetMenu(menuName = "Static Data/Window static data", fileName = "WindowStaticData")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}