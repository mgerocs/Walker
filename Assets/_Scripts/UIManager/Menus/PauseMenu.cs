using UnityEngine;

public class PauseMenu : MenuBase
{
    [SerializeField]
    private MenuTracker _menuTracker;

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }

    public void OpenSettings(SettingsMenu settingsMenu)
    {
        _menuTracker.PushMenu(settingsMenu);
    }

    public void MainTitle()
    {

    }
}
