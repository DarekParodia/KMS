using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesMixer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private MultiInputSystem _multiInputSystem;
    private int _lastDirection = 0;
    private bool _isMoving = false;
    
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private AnimationClip[] _animations;
    private Animator _animator;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _multiInputSystem = GetComponent<MultiInputSystem>();
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (_multiInputSystem.LastDirection != _lastDirection)
        {
            _lastDirection = _multiInputSystem.LastDirection;
            if (_isMoving)
            {
                _animator.Play(_animations[_lastDirection].name);
            }
            else
            {
                _spriteRenderer.sprite = _sprites[_lastDirection];
            }
        }
    }
}