using UnityEngine;

public class Door : InteractableBase
{
    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log("I am a Door");
    }
}
