using UnityEngine;
using UnityEngine.UI;

public class EquipedElement : MonoBehaviour
{
    public ElementType type;
    public Image icon;
    private InventoryElement _currentInventoryElement;

    public void EquipElement(InventoryElement inventoryElement)
    {
        if(_currentInventoryElement != null)
            _currentInventoryElement.Unequip();
        
        _currentInventoryElement = inventoryElement;
        icon.sprite = inventoryElement.currentElement.icon;
        GameManager.Instance.myPlayer.currentEquipment.EquipElement(_currentInventoryElement.currentElement);
    }

    public bool VerifyMatchElementType(ElementType type) => this.type == type;
}