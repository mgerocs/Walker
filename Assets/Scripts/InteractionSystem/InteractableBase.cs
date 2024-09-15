using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    #region Variables
    [Header("Interactable Settings")]
    public bool holdInteract;

    public float holdDuration;

    public bool multipleUse;

    public bool isInteractable;

    public string interactionPrompt;
    #endregion

    #region Properties
    public bool HoldInteract => holdInteract;

    public float HoldDuration => holdDuration;

    public bool MultipleUse => multipleUse;

    public bool IsInteractable => isInteractable;

    public string InteractionPrompt => interactionPrompt;
    #endregion

    #region Methods
    public void OnInteract()
    {
        Debug.Log("Interacted: " + gameObject.name);
    }
    #endregion
}
