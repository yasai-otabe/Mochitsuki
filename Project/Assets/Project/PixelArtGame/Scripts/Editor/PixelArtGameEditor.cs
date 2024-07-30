using UnityEditor;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PixelArtGameEditor
{
    [MenuItem("PixelArtGameTool/初期設定")]
    private static void InitSettings()
    {
        QualitySettings.antiAliasing = 0;
        PlayModeWindow.SetCustomRenderingResolution(1920, 1080, "Full HD");
    }

    [MenuItem("GameObject/PixelArtGame/Camera", false, 0)]
    public static void CreatePixelArtGameCamera()
    {
        var obj = new GameObject("PixelArtGameCamera");
        obj.tag = "MainCamera";
        obj.transform.position = new(0, 0, -10);
        
        var camera = obj.AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.orthographic = true;
        camera.orthographicSize = PixelArtGame.SCREEN_HEIGHT / (PixelArtGame.PIXELS_PER_UNIT * 2);
        camera.depth = -1;
        camera.useOcclusionCulling = false;
        camera.allowMSAA = false;

        var pixelPerfect = obj.AddComponent<PixelPerfectCamera>();
        pixelPerfect.assetsPPU = PixelArtGame.PIXELS_PER_UNIT;
        pixelPerfect.refResolutionX = PixelArtGame.SCREEN_WIDTH;
        pixelPerfect.refResolutionY = PixelArtGame.SCREEN_HEIGHT;

        Selection.activeObject = obj;
        Undo.RegisterCreatedObjectUndo(obj, "PixelArtGameCamera");
    }

    [MenuItem("GameObject/PixelArtGame/Canvas", false, 0)]
    public static void CreatePixelArtGameCanvas()
    {
        var obj = new GameObject("PixelArtGameCanvas");
        obj.layer = 5; // UI

        var canvas = obj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = true;

        var scaler = obj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new(PixelArtGame.SCREEN_WIDTH, PixelArtGame.SCREEN_HEIGHT);
        scaler.referencePixelsPerUnit = PixelArtGame.PIXELS_PER_UNIT;

        var raycaster = obj.AddComponent<GraphicRaycaster>();

        Selection.activeObject = obj;
        Undo.RegisterCreatedObjectUndo(obj, "PixelArtGameCanvas");
    }

    [MenuItem("Assets/Create/PixelArtGame/Scene", false, 0)]
    private static void CreatePixelArtGameScene()
    {
        const string TEMPLATE_SCENE_PATH = "Assets/Project/PixelArtGame/SceneTemplates/PixelArtGameScene.scenetemplate"; // 変更必要

        var selectionPath = string.Empty;
        foreach (Object obj in Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.DeepAssets))
        {
            if (obj is DefaultAsset)
            {
                selectionPath = AssetDatabase.GetAssetPath(obj);
                break;
            }
        }
        var newScenePath = selectionPath + "/NewPixelArtGameScene.unity";
        
        Debug.Log($"Create PixelArtGameScene\nSceneTemplate Path : {TEMPLATE_SCENE_PATH}\nNewScene Path : {newScenePath}\n");
        
        var sceneTemplateAsset = AssetDatabase.LoadAssetAtPath<SceneTemplateAsset>(TEMPLATE_SCENE_PATH);
        SceneTemplateService.Instantiate(sceneTemplateAsset, false, newScenePath);

        var newSceneAsset = AssetDatabase.LoadAssetAtPath<Object>(newScenePath);
        Selection.activeObject = newSceneAsset;
    }
}
