using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    private GameObject _shop;
    [SerializeField] private Transform elementsContainer;
    [SerializeField] private TextMeshProUGUI coinsText;
    private readonly byte _maxRowElements = 6;
    
    private Inventory _playerInventory, _shopkeeperInventory;
    
    
    private void Awake()
    {
        Instance = this;
        _shop = transform.GetChild(0).gameObject;
    }

    public void SetCoins(float coins)
    {
        coinsText.text = coins.ToString();
    }

    public void OpenShop(Inventory shopkeeperInventory)
    {
        coinsText.text = GameManager.Instance.myPlayer.coins.ToString();

        _playerInventory = GameManager.Instance.myPlayer.inventory;
        this._shopkeeperInventory = shopkeeperInventory;
        
        SetOffer(_shopkeeperInventory);
        
        _shop.SetActive(true);
    }
    
    public void CloseShop()
    {
        GameManager.Instance.myPlayer.inputController.CanMove = true;
        GameManager.Instance.myPlayer.inputController.CanInteract = true;
        _shop.SetActive(false);
    }

    private void SetOffer(Inventory currentOffer)
    {
        float temp = (currentOffer.elements.Count / 6f);
        int rowsCount = (int)(temp + (1 - (temp % 1)));
        Debug.Log(rowsCount);
        
        GameObject prefab = elementsContainer.GetChild(0).gameObject;

        for (int i = 1; i < rowsCount; i++)
        {
            Instantiate(prefab).transform.SetParent(elementsContainer);
        }
    }

    public void RequestBuyOffer() => SetOffer(_shopkeeperInventory);
    public void RequestSaleOffer() => SetOffer(_playerInventory);
}
