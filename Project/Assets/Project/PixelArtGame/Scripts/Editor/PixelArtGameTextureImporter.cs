using UnityEditor;
using UnityEngine;

public class PixelArtGameTextureImporter : AssetPostprocessor
{
    private void OnPreprocessTexture()
    {
        var importer = (TextureImporter)assetImporter;

        if (!importer.assetPath.StartsWith("Assets/Project/"))
            return;

        // Texture Type を Sprite (2D and UI)
        importer.textureType = TextureImporterType.Sprite;
        // Advanced > Alpha Is Transparency を有効
        importer.alphaIsTransparency = true;
        // Filter Mode を Point (no filter)
        importer.filterMode = FilterMode.Point;
        // Compression を None
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        // Pixcel Per Unit を設定
        importer.spritePixelsPerUnit = PixelArtGame.PIXELS_PER_UNIT;
    }
}