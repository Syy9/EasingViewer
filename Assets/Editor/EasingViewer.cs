using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Syy.Easing
{
    public class EasingViewer : EditorWindow
    {
        [MenuItem("Tools/EasingViewer")]
        public static void Open()
        {
            GetWindow<EasingViewer>("EasingViewer");
        }

        List<EaseNode> _nodeList = new List<EaseNode>();
        void OnEnable()
        {
            _nodeList.Add(new EaseInOut());
            _nodeList.Add(new Linear());

            var nodeWidth = 150;
            var nodeHeight = 190;
            var margin = 20;
            var baseY = 20;
            var count = 0;
            foreach (var node in _nodeList)
            {
                var rect = new Rect(count * (nodeWidth + margin) + margin, baseY, nodeWidth, nodeHeight);
                node.Init(count, this, rect);
                count++;
            }
        }

        void OnGUI()
        {
            BeginWindows();
            foreach (var node in _nodeList)
            {
                node.OnGUI();
            }
            EndWindows();
        }

        void DoWindow(int unusedWindowId)
        {
            GUILayout.Button("button");
            GUI.DragWindow();
        }
    }

    public abstract class EaseNode
    {
        protected EditorWindow _window;
        protected Rect _rect;
        int _id;

        public void Init(int id, EditorWindow window, Rect rect)
        {
            _id = id;
            _window = window;
            _rect = rect;
        }

        public void OnGUI()
        {
            _rect = GUILayout.Window(_id, _rect, WindowFunction, Title);
        }

        void WindowFunction(int id)
        {
            OnContentGUI();
            GUI.DragWindow(_window.position);
        }

        protected abstract AnimationCurve Curve { get; }

        protected virtual string Title { get { return this.GetType().Name; } }
        protected virtual void OnContentGUI()
        {
            EditorGUILayout.CurveField(Curve, GUILayout.Width(_rect.width - 10), GUILayout.Height(_rect.width));
        }
        
    }

    public class EaseInOut : EaseNode
    {
        protected override AnimationCurve Curve { get { return AnimationCurve.EaseInOut(0, 0, 1, 1); } }
    }

    public class Linear : EaseNode
    {
        protected override AnimationCurve Curve { get { return AnimationCurve.Linear(0, 0, 1, 1); } }
    }
}
