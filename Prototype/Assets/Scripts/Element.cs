using UnityEngine;

[CreateAssetMenu(fileName = "NewElement", menuName = "Create Element")]
public class Element : ScriptableObject
{
    public ElementType type;
    public Sprite sprite;
    [Min(1)] public int buyPrice, salePrice;
    public bool salable, buyable;
    public bool Equiped { get; private set; }
}

public enum ElementType
{
    Weapon,
    Ear,
    Hair,
    Top_Wear,
    Shoulder_Wear,
    Shoulder_Tattoo,
    Bottom_Wear,
    Arm_Wear,
    Leg_Wear,
    Foot_Wear
}