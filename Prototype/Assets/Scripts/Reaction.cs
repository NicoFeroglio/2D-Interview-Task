using UnityEngine;

[CreateAssetMenu(fileName = "NewReaction", menuName = "Create Reaction")]
public class Reaction : ScriptableObject
{
    public string firstContactFeedback;
    public string endContactFeedback;
}