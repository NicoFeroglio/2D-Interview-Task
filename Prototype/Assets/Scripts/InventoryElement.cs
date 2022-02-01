using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    
    [HideInInspector] public Element currentElement;
    
    public void SetInventoryElement(Element element)
    {
        currentElement = element;
        icon.sprite = element.icon;
        button.interactable = !element.equiped;
    }

    public void Equip()
    {
        button.interactable = false;
        currentElement.equiped = true;
        InventoryManager.Instance.TryEquipElement(this);
    }

    public void Unequip()
    {
        button.interactable = true;
    }
}
