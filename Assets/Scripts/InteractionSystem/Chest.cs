using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null) return false;

        if (!inventory.hasKey) return false;

        Debug.Log("Opening Chest");

        return true;
    }
}
