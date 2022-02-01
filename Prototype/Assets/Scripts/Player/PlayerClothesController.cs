using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothesController : MonoBehaviour
{
    public List<ClothSection> sections;

    public void UseCloth(Element cloth)
    {
        for (int i = 0; i < sections.Count; i++)
        {
            if (sections[i].type == cloth.type)
            {
                sections[i].container.sprite = cloth.sprite;
            }
        }
    }
}

[System.Serializable]
public struct ClothSection
{
    public SpriteRenderer container;
    public ElementType type;
}