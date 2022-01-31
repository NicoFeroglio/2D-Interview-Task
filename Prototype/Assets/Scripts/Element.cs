using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "Create Element")]
public class Element : ScriptableObject
{
    public Sprite sprite;
    [Min(1)] public int buyPrice, salePrice;
    public bool salable, buyable;
}

