using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class MainMenu : MonoBehaviour
    {
        /*
        public Animator transition;
        public float transitionTime;
    
        public void LoadLevel (int sceneNumber)
        {
            Debug.Log("sceneBuildIndex to load:" + sceneNumber);
            StartCoroutine(LoadLevelTransition(sceneNumber));
        }
    
        IEnumerator LoadLevelTransition(int sceneNumber)
        {
            transition.SetTrigger("Start");
    
            yield return new WaitForSeconds(transitionTime);
    
            SceneManager.LoadScene(sceneNumber);
        }
        */

        public void QuitGame()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }
    }
}