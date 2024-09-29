using UnityEngine;

public abstract class MenuBase : MonoBehaviour, IMenu
{
    [SerializeField]
    private bool _exitOnNewPagePush = true;

    [SerializeField]
    private bool _canBeClosed = true;

    public bool ExitOnNewPagePush => _exitOnNewPagePush;

    public bool CanBeClosed => _canBeClosed;

    public bool IsActive { get; private set; }

    public virtual void Enter()
    {
        IsActive = true;
    }

    public virtual void Exit()
    {
        IsActive = false;
    }

    public void LoadMenu(MenuBase menu)
    {
        EventManager.ChangeMenu?.Invoke(menu);
    }
}
