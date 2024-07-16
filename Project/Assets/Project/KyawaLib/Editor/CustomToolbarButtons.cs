using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;

using BennyKok.ToolbarButtons;

namespace KyawaLib
{
    public class CustomToolbarButtons
    {
        private static AdvancedDropdownState scenesState = new AdvancedDropdownState();

        /// <summary>
        /// 特定シーンからゲームを開始するボタン
        /// </summary>
        [ToolbarButton("UnityEditor.GameView", "Play Game", order = 0)]
        [Shortcut("Play Game", KeyCode.Alpha5)]
        public static void ShowPlayingScenes()
        {
            if (EditorApplication.isPlaying)
                return;

            var sceneList = AssetDatabase.GetAllAssetPaths().Where(s => s.EndsWith(".unity")).ToList();
            sceneList.Sort();

            var a = new GenericAdvancedDropdown("Play Game from...", scenesState);
            foreach (var p in sceneList)
            {
                string label = ReplaceLast(p, ".unity", "");
                label = ReplaceFirst(label, "Assets/", "");
                //------------------------------------
                if (!label.StartsWith("Project/Scenes/01"))
                    continue;
                var strings = label.Split('/');
                label = strings[strings.Length - 1];
                //------------------------------------
                a.AddItem(label, () =>
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(p);
                        EditorSceneManager.playModeStartScene = sceneAsset;
                        EditorApplication.isPlaying = true;
                    }
                });
            }
            a.ShowAsContext(10);
        }

        /// <summary>
        /// 指定したシーンを開くボタン
        /// </summary>
        [ToolbarButton("SceneAsset Icon", "Load Scenes", order = 1)]
        [Shortcut("Load Scenes", KeyCode.Alpha3)]
        public static void ShowPRojectScenes()
        {
            if (EditorApplication.isPlaying)
                return;

            var sceneList = AssetDatabase.GetAllAssetPaths().Where(s => s.EndsWith(".unity")).ToList();
            sceneList.Sort();

            //const string prefKey = "ToolbarScenesState";
            //var jsonState = EditorPrefs.GetString(prefKey);
            //if (!string.IsNullOrEmpty(jsonState))
            //{
            //    EditorJsonUtility.FromJsonOverwrite(jsonState, scenesState);
            //}
            var a = new GenericAdvancedDropdown("Load Scene", scenesState);
            foreach (var p in sceneList)
            {
                string label = ReplaceLast(p, ".unity", "");
                label = ReplaceFirst(label, "Assets/", "");
                //------------------------------------
                if (!label.StartsWith("Project"))
                    continue;
                var strings = label.Split('/');
                label = strings[strings.Length - 1];
                //------------------------------------
                a.AddItem(label, () =>
                {
                    //EditorPrefs.SetString(prefKey, EditorJsonUtility.ToJson(scenesState));
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(p, OpenSceneMode.Single);
                        if (p == "bootstrap")
                        {
                            Selection.activeGameObject = GameObject.FindGameObjectWithTag("Player");
                            SceneView.FrameLastActiveSceneView();
                        }
                    }
                });
            }
            a.ShowAsContext(10);
        }

        /// <summary>
        /// パッケージマネージャーを開くボタン
        /// </summary>
        [ToolbarButton(iconName = "Package Manager", tooltip = "Package Manager", order = 2)]
        public static void ShowPackageManager()
        {
            UnityEditor.PackageManager.UI.Window.Open("");
        }

        /// <summary>
        /// 各種設定ウィンドウを選択して開くボタン
        /// </summary>
        [ToolbarButton("Settings", "Show Settings", order = 3)]
        public static void ShowSettings()
        {
            var a = new GenericMenu();
            a.AddItem(new GUIContent("Project Settings"), false, () => EditorApplication.ExecuteMenuItem("Edit/Project Settings..."));
            a.AddItem(new GUIContent("Build Settings"), false, () => EditorApplication.ExecuteMenuItem("File/Build Settings..."));
            a.ShowAsContext();
        }

        public static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static string ReplaceLast(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }
    }
}
