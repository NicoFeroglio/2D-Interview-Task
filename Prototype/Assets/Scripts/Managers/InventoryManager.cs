using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class InventoryManager : BaseWindow
{
    public static InventoryManager Instance;
    [SerializeField] private Transform elementsContainer;
    [SerializeField] private TextMeshProUGUI coins;
    private readonly int _maxRowsElement = 4;
    
    public List<EquipedElement> equipedElements = new List<EquipedElement>();
    public List<InventoryElement> inventoryElements = new List<InventoryElement>();
    
    
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public override void Close()
    {
        base.Close();
        ResetInventory();
        GameManager.Instance.myPlayer.inputController.CanOpenInventory = true;
    }

    public void OpenInventory(Inventory inventory, Equipment defaultEquipment, Equipment currentEquipment)
    {
        inventoryElements.Clear();
        
        coins.text = GameManager.Instance.myPlayer.coins.ToString();
        SetInventory(inventory);
        currentEquipment.VerifyEquipmentIntegrity(defaultEquipment, inventory);
        RefreshEquipedElements(defaultEquipment);
        
        Window.SetActive(!Window.activeSelf);
        bg.SetActive(true);
    }

    /// <summary>
    /// Equip on the character panel.
    /// </summary>
    private void RefreshEquipedElements(Equipment defaultEquipment)
    {
        foreach (var iElement in inventoryElements)
        {
            if (iElement.currentElement.equiped)
            {
                foreach (var equipedSlot in equipedElements)
                {
                    if (equipedSlot.type == iElement.currentElement.type)
                    {
                        TryEquipElement(iElement);
                        break;
                    }
                }
            }
        }
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
            {
                InventoryElement current = row.GetChild(i).GetComponent<InventoryElement>();
                current.SetInventoryElement(currentInventory.elements[elementCount++]);
                inventoryElements.Add(current);
            }
        }
    }

    private void ResetInventory()
    {
        int iterations = elementsContainer.childCount;
        for (int i = 1; i < iterations; i++)
            DestroyImmediate(elementsContainer.GetChild(0).gameObject);

        Transform row = elementsContainer.GetChild(0);
        iterations = row.childCount;
        for (int i = 1; i < iterations; i++) 
            DestroyImmediate(row.GetChild(0).gameObject);
    }
    
    public void TryEquipElement(InventoryElement inventoryElement)
    {
        for (int i = 0; i < equipedElements.Count; i++)
        {
            if (equipedElements[i].VerifyMatchElementType(inventoryElement.currentElement.type))
            {
                equipedElements[i].EquipElement(inventoryElement);
                GameManager.Instance.myPlayer.UseCloth(inventoryElement.currentElement);
                break;
            }
        }
    }
}