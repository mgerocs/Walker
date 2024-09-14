using UnityEngine;

public class Highlight : MonoBehaviour
{

    [SerializeField] public Color _outlineColor;
    [SerializeField] public float _outlineWidth;


    public void HighlightObject(GameObject gameObject)
    {
        Outline outline = gameObject.AddComponent<Outline>();
        outline.OutlineColor = _outlineColor;
        outline.OutlineWidth = _outlineWidth;
    }
}
