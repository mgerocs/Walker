using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected Color _outlineColor = Color.white;
    protected float _outlineWidth = 5.0f;

    private Outline _outline;

    private void Start()
    {
        _outline = gameObject.AddComponent<Outline>();
        _outline.OutlineColor = _outlineColor;
        _outline.OutlineWidth = _outlineWidth;
        _outline.OutlineMode = Outline.Mode.OutlineVisible;
        _outline.enabled = false;
    }

    public void HighlightObject()
    {
        _outline.enabled = true;
    }

    public void UnhighlightObject()
    {
        _outline.enabled = false;
    }
}
