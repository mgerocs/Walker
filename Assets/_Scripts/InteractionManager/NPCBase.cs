using Unity.Cinemachine;
using UnityEngine;

public abstract class NPCBase : InteractableBase
{
    [SerializeField]
    private NPCObject _npcObject;

    private CinemachineCamera _dialogCamera;

    private void Awake()
    {
        _dialogCamera = gameObject.GetComponentInChildren<CinemachineCamera>();
    }

    private void Start()
    {
        if (_dialogCamera != null)
        {
            _dialogCamera.Priority = 0;
        }
    }

    public override string InteractionPrompt { get => base.InteractionPrompt + $" {_npcObject.Name}"; }

    public override void Highlight()
    {
        return;
    }

    public override void Unhighlight()
    {
        return;
    }

    public override void OnInteract()
    {
        Debug.Log("Talked to: " + _npcObject.Name);
        EventManager.StartDialog?.Invoke(this);
    }
}
