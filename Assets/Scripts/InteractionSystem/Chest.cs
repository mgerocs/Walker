using System.Buffers.Text;
using UnityEngine;

public class Chest : InteractableBase
{
    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log("I am a Chest");
    }
}
