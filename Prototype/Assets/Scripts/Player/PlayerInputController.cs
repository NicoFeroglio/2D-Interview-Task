using System;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private PlayerController _playerController;
    public bool CanMove {
        set {
            if (value)
                OnUpdate += CheckMovementInputs;
            else
                OnUpdate -= CheckMovementInputs;
        }
    }
    private Vector2 _input = Vector2.zero;
    private event Action OnUpdate = delegate {  };
    
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        CanMove = true;
    }

    private void CheckMovementInputs()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");
        _playerController.Move(_input);
    }
    
    private void Update()
    {
        OnUpdate();
    }
}