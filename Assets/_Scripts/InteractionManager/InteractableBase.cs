using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    [SerializeField]
    private bool _multipleUse;
    [SerializeField]
    private bool _isInteractable;
    [SerializeField]
    private string _interactionPrompt;
    [SerializeField]
    private Color _outlineColor = Color.white;
    [SerializeField]
    private float _outlineWidth = 5.0f;

    public virtual bool MultipleUse { get { return _multipleUse; } }

    public bool IsInteractable { get { return _isInteractable; } }

    public virtual string InteractionPrompt { get { return _interactionPrompt; } set { _interactionPrompt = value; } }

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

    private void HandleInteractableFound(IInteractable interactable)
    {
        if (interactable as Object == this)
        {
            Highlight();
        }
    }

    private void HandleInteractableLost()
    {
        Unhighlight();
    }

    public virtual void Highlight()
    {
        _outline.enabled = true;
    }

    public virtual void Unhighlight()
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
