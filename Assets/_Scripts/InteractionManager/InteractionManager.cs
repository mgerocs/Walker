// https://www.youtube.com/watch?v=TwX2tppnVr4&ab_channel=VeryHotShark

using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public InteractionData interactionData;

    public InteractionInputData interactioninputData;

    public Transform interactionTrigger;

    public InteractionPromptUI interactionPromptUI;

    public float overlapShpereRadius = 0.5f;

    public LayerMask interactableLayer;

    private readonly Collider[] _colliders = new Collider[3];

    private void OnEnable()
    {
        EventManager.OnInteract += HandleInteract;
    }

    private void OnDisable()
    {
        EventManager.OnInteract -= HandleInteract;
    }

    private void Update()
    {
        CheckForInteractable();
    }

    private void HandleInteract()
    {
        if (interactionData.IsEmpty()) return;

        if (!interactionData.Interactable.IsInteractable) return;

        interactionData.Interact();
    }

    private void CheckForInteractable()
    {
        int numFound = Physics.OverlapSphereNonAlloc(
            interactionTrigger.position,
            overlapShpereRadius,
            _colliders,
            interactableLayer
        );

        if (numFound > 0)
        {
            InteractableBase _interactable = _colliders[0].GetComponent<InteractableBase>();

            if (_interactable != null)
            {
                if (interactionData.IsEmpty())
                {
                    interactionData.Interactable = _interactable;
                }
                else
                {
                    if (!interactionData.IsSameInteractable(_interactable))
                    {
                        interactionData.Interactable = _interactable;
                    }
                }

                interactionData.Highlight();
            }
        }
        else
        {
            if (!interactionData.IsEmpty())
            {
                interactionData.Unhighlight();
                interactionData.ResetData();
            }
        }
    }
}
