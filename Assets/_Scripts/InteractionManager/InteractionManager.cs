// https://www.youtube.com/watch?v=TwX2tppnVr4&ab_channel=VeryHotShark

using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    private InteractionTracker _interactionTracker;

    private void Awake()
    {
        if (_interactionTracker == null)
        {
            Debug.LogError("Missing InteractionTracker reference.");
        }
    }

    private void OnEnable()
    {
        EventManager.OnInteractableFound += HandleInteractableFound;
        EventManager.OnInteractableLost += HandleInteractableLost;

        EventManager.Interact += HandleInteract;
    }

    private void Start()
    {
        if (_interactionTracker == null) return;

        _interactionTracker.ResetData();
    }

    private void OnDisable()
    {
        EventManager.OnInteractableFound -= HandleInteractableFound;
        EventManager.OnInteractableLost -= HandleInteractableLost;

        EventManager.Interact -= HandleInteract;
    }

    private void HandleInteractableFound(IInteractable interactable)
    {
        if (_interactionTracker == null) return;

        if (_interactionTracker.IsEmpty())
        {
            _interactionTracker.Interactable = interactable;
        }
        else
        {
            if (!_interactionTracker.IsSameInteractable(interactable))
            {
                _interactionTracker.Interactable = interactable;
            }
        }
    }

    private void HandleInteractableLost()
    {
        if (_interactionTracker == null) return;

        if (!_interactionTracker.IsEmpty())
        {
            _interactionTracker.ResetData();
        }
    }

    private void HandleInteract()
    {
        if (_interactionTracker == null) return;

        if (_interactionTracker.IsEmpty()) return;

        if (!_interactionTracker.Interactable.IsInteractable) return;

        _interactionTracker.Interact();
    }
}
