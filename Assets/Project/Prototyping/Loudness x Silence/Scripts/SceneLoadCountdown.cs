using UnityEngine;
 
 namespace Echosystem.Resonance.Game
 {
     public class SceneLoadCountdown : MonoBehaviour
     {
         [SerializeField] private SceneLoader.Scene _scene;
         [SerializeField] private SceneLoader.Scene _loadingScene;
 
         [SerializeField] private int _timer;
 
         void Start()
         {
             Invoke("LoadScene", _timer);
         }
 
         private void LoadScene()
         {
             SceneLoader.Instance.LoadScene(_scene, _loadingScene, 10f);
         }
     }
 }