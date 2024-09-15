// https://www.youtube.com/watch?v=TwX2tppnVr4&ab_channel=VeryHotShark

using UnityEngine;

public class InteractionController : MonoBehaviour
{
    #region Variables
    [Header("Data")]
    /* public InteractionInputData interactionInputData; */

    public InteractionData interactionData;

    public Transform interactionPoint;

    [Header("Ray Settings")]
    public float overlapShpereRadius;

    public LayerMask interactableLayer;

    private Camera _cam;

    private readonly Collider[] _colliders = new Collider[3];
    #endregion

    #region Built in methods
    void Awake()
    {
        _cam = FindAnyObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractable();
        CheckForInteractableInput();
    }
    #endregion

    #region Custom methods
    void CheckForInteractable()
    {
        int numFound = Physics.OverlapSphereNonAlloc(
            interactionPoint.position,
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
                    Debug.Log("found interactable");
                    interactionData.Interactable = _interactable;
                }
                else
                {
                    if (!interactionData.IsSameInteractable(_interactable))
                    {
                        Debug.Log("found another interactable");
                        interactionData.Interactable = _interactable;
                    }
                }
            }
        }
        else
        {
            Debug.Log("no interactable found");
            interactionData.ResetData();
        }
    }

    void CheckForInteractableInput() { }
    #endregion
}
