using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{

    [SerializeField] private List<Collider> _colliders;
    [SerializeField] private Animator _anim;

    [SerializeField] private AnimationClip _hideAnimationClip;
    [SerializeField] private AnimationClip _showAnimationClip;

    private bool _hidden;
    
    
    public void Hide()
    {
        if (!_hidden)
        {
            _anim.SetBool("Hide", true);
            _anim.SetBool("Show", false);
            StartCoroutine(HandleColliders(_hideAnimationClip.length));
            
            _hidden = true;
        }
    }

    public void Show()
    {
        if (_hidden)
        {
            _anim.SetBool("Hide", false);
            _anim.SetBool("Show", true);
            StartCoroutine(HandleColliders(_showAnimationClip.length));
            
            _hidden = false;
        }
    }

    private IEnumerator HandleColliders(float timer)
    {
        yield return new WaitForSeconds(timer);

        foreach (var collider in _colliders)
        {
            if (_hidden)
            {
                collider.enabled = false;
            }
            else
            {
                collider.enabled = true;
            }
        }
    }
}
