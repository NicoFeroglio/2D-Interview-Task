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
    public bool CanInteract {
        set
        {
            if (value)
                OnUpdate += CheckInteractionInput;
            else
                OnUpdate -= CheckInteractionInput;
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

    private void CheckInteractionInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            InteractionManager.Instance.Interact(()=> CanMove = true);
            CanMove = CanInteract = false;
        }
    }
    
    private void Update()
    {
        OnUpdate();
    }
}