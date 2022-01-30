using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour, Interactable
{
    public List<Reaction> reactions;
    
    public void OnStartInteraction()
    {
        
    }

    public void OnEndInteraction()
    {
        
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