using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AutomatedBuildPipeline
{
    [MenuItem ("Pipeline/Build/VR Windows")]
    public static void BuildVR()
    {
        SetSettingsVR();
        
        Debug.Log("Building for Windows + VR Enabled");
        
        var path = Path.GetDirectoryName(Path.GetDirectoryName(Application.dataPath));
        var buildPath = Path.Combine(path, "Build");
        buildPath = Path.Combine(buildPath, "VR") + Path.DirectorySeparatorChar + "Echosystem Resonance.exe";

        Debug.Log("Build saved to: " + buildPath);
        
        List<string> scenes = new List<string>();
        
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var scene = SceneUtility.GetScenePathByBuildIndex(i);
            scenes.Add(scene);
        }
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes.ToArray();
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;
        
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
    }
    
    [MenuItem ("Pipeline/Build/Non-VR Mac OS")]
    public static void BuildMacOsNonVR()
    {
        SetSettingsNonVR();
        
        Debug.Log("Building for Mac OS");
        
        var path = Path.GetDirectoryName(Path.GetDirectoryName(Application.dataPath));
        var buildPath = Path.Combine(path, "Build");
        buildPath = Path.Combine(buildPath, "Non-VR");
        buildPath = Path.Combine(buildPath, "Mac OS")  + Path.DirectorySeparatorChar + "Echosystem Resonance.app";

        Debug.Log("Build saved to: " + buildPath);
        
        List<string> scenes = new List<string>();
        
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var scene = SceneUtility.GetScenePathByBuildIndex(i);
            scenes.Add(scene);
        }
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes.ToArray();
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.target = BuildTarget.StandaloneOSX;
        buildPlayerOptions.options = BuildOptions.None;
        
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
    }
    
    [MenuItem ("Pipeline/Build/Non-VR Windows")]
    public static void BuildWindowsNonVR()
    {
        SetSettingsNonVR();
        
        Debug.Log("Building for Windows");
        
        var path = Path.GetDirectoryName(Path.GetDirectoryName(Application.dataPath));
        var buildPath = Path.Combine(path, "Build");
        buildPath = Path.Combine(buildPath, "Non-VR");
        buildPath = Path.Combine(buildPath, "Windows")  + Path.DirectorySeparatorChar + "Echosystem Resonance.exe";

        Debug.Log("Build saved to: " + buildPath);
        
        List<string> scenes = new List<string>();
        
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var scene = SceneUtility.GetScenePathByBuildIndex(i);
            scenes.Add(scene);
        }
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes.ToArray();
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;
        
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
    }

    [MenuItem ("Pipeline/Set Scene Settings/VR")]
    private static void SetSettingsVR()
    {
        Debug.Log("Setting SceneSettings to VREnabled");
        
        PlayerSettings.virtualRealitySupported = true;

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        var currentScene = SceneManager.GetActiveScene().path;

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
        
        EditorSceneManager.OpenScene(currentScene);
    }

    
    [MenuItem ("Pipeline/Set Scene Settings/Non-VR")]
    private static void SetSettingsNonVR()
    {
        Debug.Log("Setting SceneSettings to !VREnabled");
        
        PlayerSettings.virtualRealitySupported = false;
        
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        var currentScene = SceneManager.GetActiveScene().path;
        
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
        
        EditorSceneManager.OpenScene(currentScene);
    }
}
