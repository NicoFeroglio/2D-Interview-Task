using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : BaseWindow
{
    public static ShopManager Instance;

    [SerializeField] private Transform elementsContainer;
    [SerializeField] private TextMeshProUGUI coinsText;
    private readonly byte _maxRowElements = 6;

    [SerializeField] private Button shopkeeperButton, playerButton;
    
    private Inventory _playerInventory, _shopkeeperInventory;
    private Inventory _currentInventory;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public override void Close()
    {
        base.Close();
        GameManager.Instance.myPlayer.inputController.CanOpenInventory = true;
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

        shopkeeperButton.onClick.Invoke();

        Window.SetActive(true);
        bg.SetActive(true);
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
        
        float rowsCount = (_currentInventory.elements.Count / (float)_maxRowElements);

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
                element.GetComponent<ShopElement>().SetShopElement(_currentInventory.elements[elementCount], elementCount, _currentInventory == _playerInventory);
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

    public void ElementSold(Element element, int index, TransactionType transactionType)
    {
        if (transactionType == TransactionType.NPC_to_Player)
        {
            if (GameManager.Instance.myPlayer.CanBuy(element.buyPrice))
            {
                GameManager.Instance.myPlayer.RefreshCoins(-element.buyPrice);
                _playerInventory.elements.Add(element);
            }
        }
        else
        {
            GameManager.Instance.myPlayer.RefreshCoins(element.salePrice);
            _playerInventory.elements.RemoveAt(index);
            _currentInventory = null;
            RequestSaleOffer();
            _shopkeeperInventory.elements.Add(element);
            GameManager.Instance.myPlayer.currentEquipment.VerifyEquipmentIntegrity(
                GameManager.Instance.myPlayer.defaultEquipment,
                GameManager.Instance.myPlayer.inventory);
        }
        
        SetCoins(GameManager.Instance.myPlayer.coins);
    }
}

public enum TransactionType
{
    Player_to_NPC,
    NPC_to_Player
}