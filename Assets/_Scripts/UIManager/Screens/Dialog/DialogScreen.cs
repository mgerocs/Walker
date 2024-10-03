
using UnityEngine;

public class DialogScreen : ScreenBase, IScreen
{
    public void FinishDialog()
    {
        EventManager.FinishDialog?.Invoke();
    }
}