using UnityEngine;

[CreateAssetMenu(fileName = "InteractionData", menuName = "Scriptable Objects/InteractionData")]
public class InteractionData : ScriptableObject
{
    private InteractableBase _interactable;

    public InteractableBase Interactable
    {
        get => _interactable;
        set => _interactable = value;
    }

    public void Interact()
    {
        _interactable.OnInteract();
        ResetData();
    }

    public bool IsSameInteractable(InteractableBase newInteractable) =>
        _interactable == newInteractable;

    public bool IsEmpty() => _interactable == null;

    public void ResetData() => _interactable = null;
}
