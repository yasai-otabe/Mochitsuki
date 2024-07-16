using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace KyawaLib
{
    public class CustomMenuItems
    {
        /// <summary>
        /// ゲーム画面のスクリーンショットを保存する
        /// </summary>
        [MenuItem("KyawaLib/Capture Screenshot &S")]
        static void Capture()
        {
            var folder = String.Concat(Application.dataPath.Replace("Assets", "../"), "ScreenShots/");
            var file = DateTime.Now.ToString("yyMMdd-HHmmss");
            var path = $"{folder}{file}.png";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            ScreenCapture.CaptureScreenshot(path);
            Debug.Log($"Captured Screenshot. : {path}");
        }

        /*
        #region インスペクターロック
        private static EditorWindow mouseOverWindow;

        /// <summary>
        /// インスペクターのロックを切り替える
        /// https://baba-s.hatenablog.com/entry/2017/12/28/145700
        /// </summary>
        [MenuItem("KyawaLib/Toggle Inspector Lock &L")]
        private static void Toggle()
        {
            if (mouseOverWindow == null)
            {
                if (!EditorPrefs.HasKey("LockableInspectorIndex"))
                    EditorPrefs.SetInt("LockableInspectorIndex", 0);
                int i = EditorPrefs.GetInt("LockableInspectorIndex");

                var type = Assembly
                    .GetAssembly(typeof(Editor))
                    .GetType("UnityEditor.InspectorWindow");

                var list = Resources.FindObjectsOfTypeAll(type);
                mouseOverWindow = list.ElementAtOrDefault(i) as EditorWindow;
            }

            if (mouseOverWindow != null && mouseOverWindow.GetType().Name == "InspectorWindow")
            {
                var type = Assembly
                    .GetAssembly(typeof(Editor))
                    .GetType("UnityEditor.InspectorWindow");

                var propertyInfo = type.GetProperty("isLocked");
                var value = (bool)propertyInfo.GetValue(mouseOverWindow, null);
                propertyInfo.SetValue(mouseOverWindow, !value, null);
                mouseOverWindow.Repaint();
            }
        }
        #endregion
        */
    }
}