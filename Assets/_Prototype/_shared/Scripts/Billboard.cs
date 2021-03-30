using System.Collections.Generic;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Vector3 _initScale;
    private Quaternion _originalRotation;

    [SerializeField] private bool _rotateToPlayer;
    [SerializeField] private bool _scaleToPlayer;

    [SerializeField] private float _scaleDistance;
    [SerializeField] private float _hideDistance;
    [SerializeField] private List <Behaviour> _hideComponents;
    [SerializeField] private SpriteRenderer _hideRenderer;
    
    private 

    void Start()
    {
        _initScale = transform.localScale;
        _originalRotation = transform.rotation;

        gameObject.layer = 18;
    }

    void Update()
    {
        if (_rotateToPlayer)
        {
            transform.rotation = Observer.PlayerHead.transform.rotation * _originalRotation;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }


        if (_scaleToPlayer)
        {
            float distance = Vector3.Distance(transform.position, Observer.Player.transform.position);

            if (_hideDistance > 0)
            {
                if (distance > _hideDistance)
                {
                    foreach (Behaviour component in _hideComponents)
                    {
                        component.enabled = false;
                        _hideRenderer.enabled = false;
                    } 
                }
                else
                {
                    foreach (Behaviour component in _hideComponents)
                    {
                        component.enabled = true;
                        _hideRenderer.enabled = true;
                    } 
                }
            }
            
            if (_scaleDistance > 0)
            {
                if (distance > _scaleDistance)
                {
                    return;
                }
            }
            



            
            transform.localScale = _initScale * distance;

            if (transform.localScale.x <= _initScale.x)
            {
                transform.localScale = _initScale;
            }
        }
    }
}