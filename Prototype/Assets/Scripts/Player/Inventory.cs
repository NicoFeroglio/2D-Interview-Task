using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Create Inventory")]
public class Inventory : ScriptableObject
{
    //public uint size = 20;
    public List<Element> elements = new List<Element>();
}