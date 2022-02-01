using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    
    private Element _currentElement;

    public void SetInventoryElement(Element element)
    {
        _currentElement = element;
        icon.sprite = element.sprite;
        button.interactable = !element.Equiped;
    }

    public void Equip()
    {
        button.interactable = false;
        //lo equipo en el personaje
    }
}
