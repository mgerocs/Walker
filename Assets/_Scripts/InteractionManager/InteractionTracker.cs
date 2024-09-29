using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Tracker", menuName = "Interaction Manager/Interaction Tracker")]
public class InteractionTracker : ScriptableObject
{
    public IInteractable Interactable { get; set; }

    public string InteractionPrompt => Interactable.InteractionPrompt;

    public void Interact()
    {
        Interactable.OnInteract();
        ResetData();
    }

    public bool IsSameInteractable(IInteractable newInteractable) =>
        Interactable == newInteractable;

    public bool IsEmpty() => Interactable == null;

    public void ResetData() => Interactable = null;
}
