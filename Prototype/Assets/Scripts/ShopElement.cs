using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopElement : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI priceText;
    private uint _price;
    [SerializeField] private Button button;
    
    
    public void SetShopElement(Element element, bool isSelling)
    {
        icon.sprite = element.sprite;
        if (isSelling)
        {
            _price = element.salePrice;
            button.onClick.AddListener(SoldOut);
        }
        else
        {
            _price = element.buyPrice;
            button.onClick.AddListener(Bought);
        }
        priceText.text = _price.ToString();
    }

    private void Bought()
    {
        
    }

    public void SoldOut()
    {
        
    }
}
