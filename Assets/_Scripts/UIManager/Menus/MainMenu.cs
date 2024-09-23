using UnityEngine;

public class MainMenu : MenuBase
{
    [SerializeField]
    private MenuTracker _menuTracker;

    [SerializeField]
    private string _startingScene;

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void StartGame()
    {

    }

    public void LoadGame()
    {

    }

    public void OpenSettings(MenuBase settingsMenu)
    {
        _menuTracker.PushMenu(settingsMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
