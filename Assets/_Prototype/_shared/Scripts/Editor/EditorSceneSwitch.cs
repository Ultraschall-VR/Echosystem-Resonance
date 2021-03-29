using UnityEditor;
using UnityEditor.SceneManagement;

public static class EditorSceneSwitch
{
    [MenuItem ("Scenes/LevelMenu")]
    public static void OpenLevelMenu()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Prototype/Scenes/LevelMenu.unity");
    }
    
    [MenuItem ("Scenes/Level0")]
    public static void OpenLevel0()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Prototype/Scenes/Level0.unity");
    }
    
    [MenuItem ("Scenes/Level1")]
    public static void OpenLevel1()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Prototype/Scenes/Level1.unity");
    }
    
    [MenuItem ("Scenes/Level2")]
    public static void OpenLevel2()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Prototype/Scenes/Level2.unity");
    }
    
    [MenuItem ("Scenes/Level3")]
    public static void OpenLevel3()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Prototype/Scenes/Level3.unity");
    }
}