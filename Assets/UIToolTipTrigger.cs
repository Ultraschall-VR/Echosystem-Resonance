using UnityEngine;

public class UIToolTipTrigger : MonoBehaviour
{
    private ToolTipps _toolTipps;

    [SerializeField] private ToolTipps.Tooltip _tooltip;

    private bool _triggered = false;

    private void Start()
    {
        Invoke("Initialize", 0.1f);
    }

    private void Initialize()
    {
        _toolTipps = FindObjectOfType<ToolTipps>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_triggered)
            return;
        
        if (other.CompareTag("Player"))
        {
            _triggered = true;
            _toolTipps.LoadToolTip(_tooltip);
            
            Invoke("HideToolTip", 3f);
        }
    }

    private void HideToolTip()
    {
        _toolTipps.UnloadToolTip();
    }
}
