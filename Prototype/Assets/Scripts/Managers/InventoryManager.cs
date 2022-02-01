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
    private readonly int _maxRowsElement = 4;
    
    public List<EquipedElement> equipedElements = new List<EquipedElement>();
    
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public void OpenInventory(Inventory inventory, Equipment defaultEquipment, Equipment currentEquipment)
    {
        coins.text = GameManager.Instance.myPlayer.coins.ToString();
        SetInventory(inventory);
        currentEquipment.VerifyEquipmentIntegrity(defaultEquipment, inventory);
        Window.SetActive(!Window.activeSelf);
    }

    private void SetInventory(Inventory currentInventory)
    {
        float rowsCount = (currentInventory.elements.Count / (float)_maxRowsElement);

        GameObject rowPrefab = elementsContainer.GetChild(0).gameObject;
        GameObject elementPrefab = rowPrefab.transform.GetChild(0).gameObject;
        
        int count = currentInventory.elements.Count - 2;
        
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
            for (int j = 1; j < _maxRowsElement && count >= 0; j++)
            {
                var elementInstance = Instantiate(elementPrefab).transform;
                elementInstance.SetParent(row);
                elementInstance.localScale = Vector3.one;
                
                count--;
            }

            for (int i = 0; i < row.childCount; i++)
                row.GetChild(i).GetComponent<InventoryElement>().SetInventoryElement(currentInventory.elements[elementCount++]);
        }
    }

    public void TryEquipElement(Element element)
    {
        for (int i = 0; i < equipedElements.Count; i++)
        {
            if (equipedElements[i].VerifyMatchElementType(element.type))
            {
                equipedElements[i].SetEquipedElement(element);
                break;
            }
        }
    }
}