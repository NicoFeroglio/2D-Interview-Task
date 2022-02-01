using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWindow : MonoBehaviour
{
    [SerializeField] protected GameObject bg;
    protected GameObject Window;

    protected virtual void Awake()
    {
        Window = transform.GetChild(0).gameObject;
    }

    public virtual void Close()
    {
        GameManager.Instance.myPlayer.inputController.CanMove = true;
        GameManager.Instance.myPlayer.inputController.CanInteract = true;
        Window.SetActive(false);
        bg.SetActive(false);
    }
}
