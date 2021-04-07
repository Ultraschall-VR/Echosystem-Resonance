using UnityEngine;

public class CinemaModeDisabler : MonoBehaviour
{
    void Start()
    {
        if(SceneSettings.Instance.CinemaMode)
            gameObject.SetActive(false);
        else 
            gameObject.SetActive(true);
    }     
}
