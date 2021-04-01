using UnityEngine;

public class StartInfoboxAnimation : MonoBehaviour
{
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("InfoboxAnim");
    }
    
    void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("InfoboxAnim");
    }

    private void OnDisable()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("InfoboxAnimReversed");
    }
}
