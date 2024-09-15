public interface IInteractable
{
    public bool HoldInteract { get; }

    public float HoldDuration { get; }
    public bool MultipleUse { get; }

    public bool IsInteractable { get; }

    public string InteractionPrompt { get; }

    public void OnInteract();
}
