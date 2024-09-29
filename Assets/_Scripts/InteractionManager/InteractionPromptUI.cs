using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField]
    private InteractionTracker _interactionTracker;
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    private Camera _mainCam;

    private string _text;

    private bool _isDisplayed;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void OnEnable()
    {
        EventManager.OnInteractableFound += HandleInteractableFound;
        EventManager.OnInteractableLost += HandleInteractableLost;
    }

    private void Start()
    {
        _textMesh.text = _text;
    }

    private void OnDisable()
    {
        EventManager.OnInteractableFound -= HandleInteractableFound;
        EventManager.OnInteractableLost -= HandleInteractableLost;
    }

    private void HandleInteractableFound(IInteractable arg0)
    {
        if (!_interactionTracker.IsEmpty())
        {
            _isDisplayed = true;

            _textMesh.text = _interactionTracker.InteractionPrompt;
        }
    }

    private void HandleInteractableLost()
    {
        _isDisplayed = false;

        _textMesh.text = null;
    }

    private void LateUpdate()
    {
        if (!_isDisplayed) return;

        Quaternion rotation = _mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}
