using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopElement : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI priceText;
    private int _price;
    [SerializeField] private Button button;

    private Element _currentElement;
    private int _elementIndex;
    
    public void SetShopElement(Element element, int index, bool isSelling)
    {
        _currentElement = element;
        _elementIndex = index;
        
        icon.sprite = element.sprite;
        if (isSelling)
        {
            button.interactable = element.salable;
            _price = element.salePrice;
            button.onClick.AddListener(SoldOut);
        }
        else
        {
            button.interactable = element.buyable;
            _price = element.buyPrice;
            button.onClick.AddListener(Bought);
        }
        priceText.text = _price.ToString();
    }

    private void Bought()
    {
        ShopManager.Instance.ElementSold(_currentElement, _elementIndex, TransactionType.NPC_to_Player);
    }

    public void SoldOut()
    {
        ShopManager.Instance.ElementSold(_currentElement, _elementIndex, TransactionType.Player_to_NPC);
    }
}
