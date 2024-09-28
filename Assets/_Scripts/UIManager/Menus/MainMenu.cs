using UnityEngine;

public class MainMenu : MenuBase
{
    public void StartGame()
    {

    }

    public void LoadGame()
    {

    }

    public void OpenSettings(MenuBase settingsMenu)
    {

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
