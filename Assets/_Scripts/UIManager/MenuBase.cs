using UnityEngine;

public class MenuBase : MonoBehaviour, IMenu
{
    [SerializeField]
    private bool _exitOnNewPagePush = true;

    [SerializeField]
    private bool _canBeClosed = true;

    public bool ExitOnNewPagePush => _exitOnNewPagePush;

    public bool CanBeClosed => _canBeClosed;

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public void LoadMenu(MenuBase menu)
    {
        EventManager.OnChangeMenu?.Invoke(menu);
    }
}
