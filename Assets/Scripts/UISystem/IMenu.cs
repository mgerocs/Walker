public interface IMenu
{
    public bool ExitOnNewPagePush { get; }

    public void Enter();

    public void Exit();
}
