using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "Create Element")]
public class Element : ScriptableObject
{
    public Sprite sprite;
    public uint buyPrice, salePrice;
    public bool salable, buyable;
}

