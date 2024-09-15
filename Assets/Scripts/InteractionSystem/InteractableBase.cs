using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    public InputReader inputReader;

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
        inputReader.HighlightInteractablesEvent += HandleHighlightInteractables;
        inputReader.HighlightInteractablesCanceledEvent += HandleHighlightInteractablesCanceled;
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
        inputReader.HighlightInteractablesEvent -= HandleHighlightInteractables;
        inputReader.HighlightInteractablesCanceledEvent -= HandleHighlightInteractablesCanceled;
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

    public void Highlight()
    {
        _outline.enabled = true;
    }

    public void Unhighlight()
    {
        if (!_isBulkHighlighted) _outline.enabled = false;
    }

    public virtual void OnInteract()
    {
        Debug.Log("Interacted: " + gameObject.name);
    }
}
