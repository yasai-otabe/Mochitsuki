using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PixelArtGameFontPreset : ScriptableObject
{
    [SerializeField]
    TMP_FontAsset _asset = null;

    [SerializeField]
    int _fontSize = 0;

    public TMP_FontAsset asset => _asset;
    public int fontSize => _fontSize;

    public void SetFontAsset(TMP_FontAsset asset)
        => _asset = asset;
}

#if UNITY_EDITOR
public static class PixelArtGameFontPresetEditor
{
    [UnityEditor.MenuItem("Assets/Create/PixelArtGame/FontPreset", false, 0)]
    private static void CreatePixelArtGameScene()
    {
        if (UnityEditor.Selection.activeObject != null)
        {
            if (UnityEditor.Selection.activeObject is TMP_FontAsset fontAsset)
            {
                fontAsset.atlasTexture.filterMode = FilterMode.Point;

                var fontData = ScriptableObject.CreateInstance<PixelArtGameFontPreset>();
                fontData.SetFontAsset(fontAsset);

                var assetPath = UnityEditor.AssetDatabase.GetAssetPath(fontAsset);
                var assetName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
                assetPath = assetPath.Replace(assetName, assetName + " Preset");
                UnityEditor.AssetDatabase.CreateAsset(fontData, assetPath);

                var newAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                UnityEditor.Selection.activeObject = newAsset;
                return;
            }
        }
        Debug.LogError("Please Select a TMP_Font Asset");
    }
}
#endif
