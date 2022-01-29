using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputController _inputController;
    private PlayerAnimatorController _animatorController;
    
    public uint _coins;
    public byte _speed;
    private bool _inAction;
    private List<Element> _inventory = new List<Element>();

    private bool _onWalk;
    public bool OnWalk {
        get => _onWalk;
        set
        {
            if (value != _onWalk)
            {
                _onWalk = value;
                _animatorController.Walk(_onWalk);
            }
        }
    }
    
    private void Awake()
    {
        _inputController = GetComponent<PlayerInputController>();
        _animatorController = new PlayerAnimatorController(GetComponentInChildren<Animator>());
    }

    public void Move(Vector3 dir)
    {
        OnWalk = (dir != Vector3.zero);
        if (_onWalk)
        {
            transform.position += dir.normalized * (Time.deltaTime * _speed);
            SetGraphicDirection(dir.x);
        }
    }

    private void SetGraphicDirection(float x)
    {
        if(x != 0)
            transform.localScale = new Vector3(-x, transform.localScale.y, transform.localScale.z);
    }
}