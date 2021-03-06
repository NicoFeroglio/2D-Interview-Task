using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    private Interactable _currentInteractable;
    
    private void Awake()
    {
        Instance = this;
    }

    public void RequestInteraction(Interactable interactable)
    {
        _currentInteractable = interactable;
        _currentInteractable.OnStartContact();
        MessageManager.Instance.ActivateInteractionFeedback();
        GameManager.Instance.myPlayer.inputController.CanInteract = true;
    }

    public void ReleaseInteraction()
    {
        _currentInteractable.OnEndContact();
    }

    public void Interact(Action endInteractionCallback)
    {
        _currentInteractable.OnStartInteraction(endInteractionCallback);
    }
}
