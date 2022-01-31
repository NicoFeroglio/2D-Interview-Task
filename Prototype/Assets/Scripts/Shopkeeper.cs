using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shopkeeper : MonoBehaviour, Interactable
{
    public List<Reaction> reactions;
    private event Action OnFinishInteraction = delegate {  };
    
    
    public void OnStartInteraction(Action endCallback)
    {
        OnFinishInteraction = endCallback;
    }

    public void OnEndInteraction()
    {
        OnFinishInteraction();
    }

    public void OnStartContact()
    {
        MessageManager.Instance.OpenMessage(transform, GetRandomFeedback().firstContactFeedback);
    }

    public void OnEndContact()
    {
        MessageManager.Instance.CloseInTime(GetRandomFeedback().endContactFeedback);
    }

    private Reaction GetRandomFeedback() => reactions[Random.Range(0, reactions.Count)];
}