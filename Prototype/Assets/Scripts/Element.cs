using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "Create Element")]
public class Element : ScriptableObject
{
    public ElementType type;
    public Sprite sprite, icon;
    [Min(1)] public int buyPrice, salePrice;
    public bool salable, buyable;
    [HideInInspector] public bool equiped;
}

public enum ElementType
{
    Weapon,
    Ear,
    Hair,
    TopWear,
    ShoulderWear,
    ShoulderTattoo,
    BottomWear,
    ArmWear,
    LegWear,
    FootWear
}