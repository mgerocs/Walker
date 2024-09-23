using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    public InteractionData interactionData;

    public TextMeshProUGUI _textMesh;

    private Camera _mainCam;

    private bool _isDisplayed = false;

    private string _text;

    private void Start()
    {
        _mainCam = Camera.main;
        _textMesh.text = _text;
    }

    private void Update()
    {
        if (!interactionData.IsEmpty())
        {
            _textMesh.text = interactionData.InteractionPrompt;
        }
        else
        {
            _textMesh.text = null;
        }
    }

    private void LateUpdate()
    {
        var rotation = _mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}
