using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : BaseWindow
{
    public static InventoryManager Instance;
    [SerializeField] private Transform elementsContainer;
    [SerializeField] private TextMeshProUGUI coins;
    
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public void OpenInventory(Inventory inventory)
    {
        coins.text = GameManager.Instance.myPlayer.coins.ToString();
        
        
        Window.SetActive(!Window.activeSelf);
    }
}
