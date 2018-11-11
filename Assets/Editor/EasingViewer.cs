using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Syy.Easing
{
    public class EasingViewer : EditorWindow
    {
        [MenuItem("Tools/EasingViewer")]
        public static void Open()
        {
            GetWindow<EasingViewer>();
        }
    }
}
