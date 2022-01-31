using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "Create Element")]
public class Element : ScriptableObject
{
    public Sprite sprite;
    public uint purchasePrice, salePrice;
    public bool salable, buyable;
}