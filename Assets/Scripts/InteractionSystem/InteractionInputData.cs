using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "Scriptable Objects/InteractionInputData")]
public class InteractionInputData : ScriptableObject
{
    private bool m_interactedClicked;
    private bool m_interactedReleased;

    public bool InteractedClicked
    {
        get => m_interactedClicked;
        set => m_interactedClicked = value;
    }

    public bool InteractedReleased
    {
        get => m_interactedReleased;
        set => m_interactedReleased = value;
    }

    public void ResetInput()
    {
        m_interactedClicked = false;
        m_interactedReleased = false;
    }
}
