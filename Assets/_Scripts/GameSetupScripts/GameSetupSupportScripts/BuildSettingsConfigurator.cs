using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class BuildSettingsConfigurator
{
    static BuildSettingsConfigurator()
    {
        ConfigureBuildSettings();
    }

    private static void ConfigureBuildSettings()
    {
        // Define the paths to your scenes
        string[] scenePaths = {
            "Assets/Scenes/StartSceneA2.unity",
            "Assets/Scenes/GameSceneA2.unity",
            "Assets/Scenes/EndSceneA2.unity",

        };

        // Create a list to store the build settings scenes
        List<EditorBuildSettingsScene> buildScenes = new List<EditorBuildSettingsScene>();

        foreach (string scenePath in scenePaths)
        {
            // Check if the scene file exists
            if (System.IO.File.Exists(scenePath))
            {
                buildScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            }
            else
            {
                Debug.LogWarning($"Scene not found at path: {scenePath}");
            }
        }

        // Apply the updated list to the Build Settings
        EditorBuildSettings.scenes = buildScenes.ToArray();

        Debug.Log("Build Settings configured with predefined scenes.");
    }
}