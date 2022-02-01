using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Create Equipment")]
public class Equipment : ScriptableObject
{
    [System.Serializable]
    public struct EquipmentSpace
    {
        public Element element;
        public ElementType type;
    }

    public EquipmentSpace weapon;
    public EquipmentSpace ear, hair;
    public EquipmentSpace topWear, shoulderWear, shoulderTattoo, bottomWear, armWear, legWear, footWear;
}