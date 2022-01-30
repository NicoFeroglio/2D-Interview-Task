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
    }

    public void ReleaseInteraction()
    {
        _currentInteractable.OnEndContact();
        _currentInteractable = null;
    }
}
