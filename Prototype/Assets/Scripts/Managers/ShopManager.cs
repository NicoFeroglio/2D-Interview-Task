using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;

    public void SetCoins(float coins)
    {
        coinsText.text = coins.ToString();
    }
}
