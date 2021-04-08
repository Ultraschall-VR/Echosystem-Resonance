using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public static class AutomatedBuildPipeline
{
    [MenuItem ("Pipeline/Build/VR Windows")]
    public static void BuildVR()
    {
        // Set SceneSettings
        SetSettingsVR();

        // Set BuildPath
        var path = Path.GetDirectoryName(Path.GetDirectoryName(Application.dataPath));
        var buildPath = Path.Combine(path, "Build");
        buildPath = Path.Combine(buildPath, PlayerSettings.bundleVersion);
        buildPath = Path.Combine(buildPath, "VR");
        buildPath = Path.Combine(buildPath, "Windows")  + Path.DirectorySeparatorChar + PlayerSettings.productName + " (" +PlayerSettings.bundleVersion + "-VR"+ ")" + ".exe";
        
        // Set BuildSettings
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;

        // Build with former Settings
        Build(buildPlayerOptions, buildPath);
    }
    
    [MenuItem ("Pipeline/Build/Non-VR Windows")]
    public static void BuildWindowsNonVR()
    {
        // Set SceneSettings
        SetSettingsNonVR();
        
        // Set BuildPath
        var path = Path.GetDirectoryName(Path.GetDirectoryName(Application.dataPath));
        var buildPath = Path.Combine(path, "Build");
        buildPath = Path.Combine(buildPath, PlayerSettings.bundleVersion);
        buildPath = Path.Combine(buildPath, "Non-VR");
        buildPath = Path.Combine(buildPath, "Windows")  + Path.DirectorySeparatorChar + PlayerSettings.productName + " (" +PlayerSettings.bundleVersion + ")" + ".exe";

        // Set BuildSettings
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;

        // Build with former Settings
        Build(buildPlayerOptions, buildPath);
    }
    
    [MenuItem ("Pipeline/Build/Non-VR Mac OS")]
    public static void BuildMacOsNonVR()
    {
        // Set SceneSettings
        SetSettingsNonVR();
        
        // Set BuildPath
        var path = Path.GetDirectoryName(Path.GetDirectoryName(Application.dataPath));
        var buildPath = Path.Combine(path, "Build");
        buildPath = Path.Combine(buildPath, PlayerSettings.bundleVersion);
        buildPath = Path.Combine(buildPath, "Non-VR");
        buildPath = Path.Combine(buildPath, "Mac OS")  + Path.DirectorySeparatorChar + PlayerSettings.productName + " (" +PlayerSettings.bundleVersion + ")" + ".app";

        // Set BuildSettings
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.target = BuildTarget.StandaloneOSX;

        // Build with former Settings
        Build(buildPlayerOptions, buildPath);
    }
    
    [MenuItem ("Pipeline/Set Scene Settings/VR")]
    private static void SetSettingsVR()
    {
        // Deactivate Auto Start of SteamVR
        PlayerSettings.virtualRealitySupported = true;

        // Save currently opened Scene and store path
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        var currentScene = SceneManager.GetActiveScene().path;

        // Iterate through all Scenes in BuildSettings and Set SceneSettings
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var scene = EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(i));

            if (scene.isLoaded)
            {
                SceneSettings SceneSettings = GameObject.Find("SceneSettings").GetComponent<SceneSettings>();
                SceneSettings.VREnabled = true;
                EditorUtility.SetDirty(SceneSettings.gameObject);
                EditorSceneManager.SaveScene(scene);
            }
        }
        
        // Load formerly opened Scene
        EditorSceneManager.OpenScene(currentScene);
    }

    [MenuItem ("Pipeline/Set Scene Settings/Non-VR")]
    private static void SetSettingsNonVR()
    {
        // Deactivate Auto Start of SteamVR
        PlayerSettings.virtualRealitySupported = false;
        
        // Save currently opened Scene and store path
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        var currentScene = SceneManager.GetActiveScene().path;
        
        // Iterate through all Scenes in BuildSettings and Set SceneSettings
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var scene = EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(i));

            if (scene.isLoaded)
            {
                SceneSettings SceneSettings = GameObject.Find("SceneSettings").GetComponent<SceneSettings>();
                SceneSettings.VREnabled = false;
                EditorUtility.SetDirty(SceneSettings.gameObject);
                EditorSceneManager.SaveScene(scene);
            }
        }
        
        // Load formerly opened Scene
        EditorSceneManager.OpenScene(currentScene);
    }
    
    public static void Build(BuildPlayerOptions buildPlayerOptions, string buildPath)
    {
        // Get all scenes from BuildSettings and add to list
        List<string> scenes = new List<string>();
        
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var scene = SceneUtility.GetScenePathByBuildIndex(i);
            scenes.Add(scene);
        }
        
        // Set all buildSettings
        buildPlayerOptions.scenes = scenes.ToArray();
        buildPlayerOptions.options = BuildOptions.None;
        buildPlayerOptions.locationPathName = buildPath;
        
        // Build with report
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }
        
        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }

        // Output buildpath
        Debug.Log(buildPlayerOptions.target.ToString() + " Build saved to: " + buildPath);
        
        // Open Buildpath in Explorer/Finder
        EditorUtility.RevealInFinder(buildPath);
    }
}
