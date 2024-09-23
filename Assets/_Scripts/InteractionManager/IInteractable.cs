public interface IInteractable
{
    public bool MultipleUse { get; }

    public bool IsInteractable { get; }

    public string InteractionPrompt { get; }

    public void OnInteract();
}
