using Echosystem.Resonance.Prototyping;
using TMPro;
using UnityEngine;

public class FocusedObject : MonoBehaviour
{
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Observer.PlayerHead.transform.position, Observer.PlayerHead.transform.forward, out hit, 5f, ~LayerMask.GetMask("SilenceSphere")))
        {
            Observer.FocusedGameObject = hit.collider.gameObject;
            
            if (hit.collider.CompareTag("Player") || hit.collider.gameObject.isStatic)
            {
                _text.text = null;
                return;
            }
            
            _text.text = hit.collider.name;
        }
        else
        {
            Observer.FocusedGameObject = null;
            _text.text = null;
        }
    }
}
