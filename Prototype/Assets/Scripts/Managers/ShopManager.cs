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
    private Inventory _currentInventory;

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

    private bool ResetShop(Inventory nextInventory)
    {
        if(_currentInventory == nextInventory) return false;

        int iterations = elementsContainer.childCount;
        for (int i = 1; i < iterations; i++)
            DestroyImmediate(elementsContainer.GetChild(0).gameObject);

        Transform row = elementsContainer.GetChild(0);
        iterations = row.childCount;
        for (int i = 1; i < iterations; i++) 
            DestroyImmediate(row.GetChild(0).gameObject);

        return true;
    }

    private void SetOffer(Inventory currentOffer)
    {
        _currentInventory = currentOffer;
        
        float rowsCount = (_currentInventory.elements.Count / 6f);

        GameObject rowPrefab = elementsContainer.GetChild(0).gameObject;
        GameObject elementPrefab = rowPrefab.transform.GetChild(0).gameObject;

        int count = _currentInventory.elements.Count - 2;
        
        for (int i = 1; i < rowsCount; i++)
        {
            var rowInstance = Instantiate(rowPrefab).transform;
            rowInstance.SetParent(elementsContainer);
            rowInstance.localScale = Vector3.one;

            count--;
        }

        int elementCount = 0;
        
        foreach (Transform row in elementsContainer.transform)
        {
            for (int j = 1; j < _maxRowElements && count >= 0; j++)
            {
                var elementInstance = Instantiate(elementPrefab).transform;
                elementInstance.SetParent(row);
                elementInstance.localScale = Vector3.one;
                
                count--;
            }

            foreach (Transform element in row.transform)
            {
                element.GetComponent<ShopElement>().SetShopElement(_currentInventory.elements[elementCount], _currentInventory == _playerInventory); //falta bloquear el boton
                elementCount++;
            }
        }
    }

    public void RequestBuyOffer()
    {
        if(ResetShop(_shopkeeperInventory))
            SetOffer(_shopkeeperInventory);
    }

    public void RequestSaleOffer()
    {
        if(ResetShop(_playerInventory))
            SetOffer(_playerInventory);
    }


}
