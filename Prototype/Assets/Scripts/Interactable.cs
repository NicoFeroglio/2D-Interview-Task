using System;

public interface Interactable
{
    public void OnStartInteraction(Action endCallback);
    public void OnEndInteraction();
    public void OnStartContact();
    public void OnEndContact();
}

