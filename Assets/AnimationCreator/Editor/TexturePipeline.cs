using UnityEngine;
using UnityEditor;

public class TexturePipeline : AssetPostprocessor {

    private void OnPreprocessTexture() {
        Debug.LogFormat("OnPrepTexture, The path is {0}", assetPath);
        if (assetPath.StartsWith("Assets/AnimationRes/BuildAnimation")) {
            PreprocessBg();
        }
    }

    //private void OnPostprocessTexture(Texture2D texture) {
    //    Debug.LogFormat("OnPostTexture, The path is {0}", assetPath);
    //}

    private void PreprocessBg() {
        TextureImporter importer = assetImporter as TextureImporter;
        if (importer == null) {
            return;
        }
        importer.maxTextureSize = 2048;
        importer.textureType = TextureImporterType.Sprite;
        importer.spritePixelsPerUnit = 100;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        var textureSettings = new TextureImporterSettings();
        importer.ReadTextureSettings(textureSettings);
        textureSettings.spriteAlignment = (int)SpriteAlignment.Center;
        textureSettings.mipmapEnabled = false;
        textureSettings.filterMode = FilterMode.Bilinear;
        importer.SetTextureSettings(textureSettings);
    }

}
