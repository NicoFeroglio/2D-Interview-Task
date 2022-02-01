using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Create Equipment")]
public class Equipment : ScriptableObject
{
    public EquipmentSpace[] equipments = new EquipmentSpace[10];

    public void EquipElement(Element currentElement)
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            if (equipments[i].type == currentElement.type)
            {
                equipments[i].element = currentElement;
                break;
            } 
        }
    }

    public void VerifyEquipmentIntegrity(Equipment defaultEquipment, Inventory inventory)
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < inventory.elements.Count; j++)
            {
                if (equipments[i].element == inventory.elements[j])
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                equipments[i].element = defaultEquipment.equipments[i].element;
                //se tiene que actualizar el sprite del personaje
            }
        }
    }
}

[System.Serializable]
public struct EquipmentSpace
{
    public Element element;
    public ElementType type;
}