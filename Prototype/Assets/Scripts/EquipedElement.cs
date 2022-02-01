using UnityEngine;
using UnityEngine.UI;

public class EquipedElement : MonoBehaviour
{
    public ElementType type;
    public Image icon;
    private Element _currentElement;

    public void SetEquipedElement(Element element)
    {
        _currentElement = element;
    }

    public bool VerifyMatchElementType(ElementType type) => this.type == type;
}