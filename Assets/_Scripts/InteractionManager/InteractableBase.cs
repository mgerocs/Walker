using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public bool multipleUse;

    public bool isInteractable;

    public string interactionPrompt;

    public Color _outlineColor = Color.white;

    public float _outlineWidth = 5.0f;

    public bool MultipleUse => multipleUse;

    public bool IsInteractable => isInteractable;

    public string InteractionPrompt => interactionPrompt;

    private Outline _outline;

    private bool _isBulkHighlighted = false;

    private void OnEnable()
    {
        EventManager.HighlightInteractables += HandleHighlightInteractables;
        EventManager.CancelHighlightInteractables += HandleHighlightInteractablesCanceled;

        EventManager.OnInteractableFound += HandleInteractableFound;
        EventManager.OnInteractableLost += HandleInteractableLost;
    }

    private void Start()
    {
        _outline = gameObject.AddComponent<Outline>();
        _outline.OutlineColor = _outlineColor;
        _outline.OutlineWidth = _outlineWidth;
        _outline.OutlineMode = Outline.Mode.OutlineVisible;
        _outline.enabled = false;
    }

    private void OnDisable()
    {
        EventManager.HighlightInteractables -= HandleHighlightInteractables;
        EventManager.CancelHighlightInteractables -= HandleHighlightInteractablesCanceled;

        EventManager.OnInteractableFound -= HandleInteractableFound;
        EventManager.OnInteractableLost -= HandleInteractableLost;
    }

    private void HandleHighlightInteractables()
    {
        _isBulkHighlighted = true;
        Highlight();
    }

    private void HandleHighlightInteractablesCanceled()
    {
        _isBulkHighlighted = false;
        Unhighlight();
    }

    private void HandleInteractableFound(InteractableBase interactable)
    {
        if (interactable == this)
        {
            Highlight();
        }
    }

    private void HandleInteractableLost()
    {
        Unhighlight();
    }

    public void Highlight()
    {
        _outline.enabled = true;
    }

    public void Unhighlight()
    {
        if (!_isBulkHighlighted)
        {
            _outline.enabled = false;
        }
    }

    public virtual void OnInteract()
    {
        Debug.Log("Interacted: " + gameObject.name);
    }
}
