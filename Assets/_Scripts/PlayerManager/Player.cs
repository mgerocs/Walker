using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraRoot;

    [SerializeField]
    private Transform _interactionTriger;

    public Transform CameraRoot
    {
        get
        {
            return _cameraRoot;
        }
    }

    public Transform InteractionTrigger
    {
        get
        {
            return _interactionTriger;
        }
    }
}