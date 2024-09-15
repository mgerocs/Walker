using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private IInteractable _interactable;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();



            if (_interactable != null)
            {

                // cast to parent and call HighlightObject()
                Interactable interactable = _interactable as Interactable;
                interactable.HighlightObject();

                if (!_interactionPromptUI.isDisplayed)
                {
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                }

                /* if (Keyboard.current.fKey.wasPressedThisFrame)
                {
                    _interactable.OnInteract(this);
                } */
            }
        }
        else
        {
            if (_interactable != null)
            {
                // cast to parent and call HighlightObject()
                Interactable interactable = _interactable as Interactable;
                interactable.UnhighlightObject();

                _interactable = null;
            }

            if (_interactionPromptUI.isDisplayed)
            {
                _interactionPromptUI.Close();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

}
