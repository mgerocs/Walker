using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField]
    private float _overlapShpereRadius = 1f;

    [SerializeField]
    private LayerMask _interactableLayer;

    [SerializeField]
    private Color _gizmoColor = Color.red;

    private readonly Collider[] _colliders = new Collider[3];

    private bool _hasFound;

    private void Update()
    {
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        int numberOfItemsFound = Physics.OverlapSphereNonAlloc(
            transform.position,
            _overlapShpereRadius,
            _colliders,
            _interactableLayer
        );

        if (numberOfItemsFound > 0)
        {
            _hasFound = true;

            IInteractable interactable = _colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                EventManager.OnInteractableFound?.Invoke(interactable);
            }
        }
        else
        {
            if (_hasFound == true)
            {
                _hasFound = false;

                EventManager.OnInteractableLost?.Invoke();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawWireSphere(transform.position, _overlapShpereRadius);
    }
}