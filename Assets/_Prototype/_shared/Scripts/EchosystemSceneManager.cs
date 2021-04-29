using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class EchosystemSceneManager : MonoBehaviour
{
    public static int SceneToLoad;
    public static LevelLoader LevelLoader;
    public static PauseMenu PauseMenu;

    private void Awake()
    {
        LevelLoader = null;
        PauseMenu = null;
    }
}
