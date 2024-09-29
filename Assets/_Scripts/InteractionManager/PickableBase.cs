using UnityEngine;

public abstract class PickableBase : InteractableBase
{
    [SerializeField]
    private ItemObject _itemObject;

    public override bool MultipleUse => false;

    public override string InteractionPrompt { get => base.InteractionPrompt + $" ({_itemObject.Name})"; }

    public override void OnInteract()
    {
        Debug.Log("Picked up: " + _itemObject.Name);
        EventManager.OnItemPickedUp?.Invoke(_itemObject);

        Destroy(gameObject);
    }
}
