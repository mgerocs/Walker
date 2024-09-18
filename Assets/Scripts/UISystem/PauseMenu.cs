using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(CanvasGroup))]
[DisallowMultipleComponent]
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

    public void OpenSettings(MenuBase settingsMenu)
    {
        _menuTracker.PushMenu(settingsMenu);
    }

    public void MainTitle(MenuBase mainMenu)
    {
        _menuTracker.PushMenu(mainMenu);
    }
}
