// https://www.youtube.com/watch?v=9MIwIaRUUhc&ab_channel=LlamAcademy

using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Canvas))]
[DisallowMultipleComponent]
public class MenuController : MonoBehaviour
{
    [SerializeField]
    private MenuTracker _menuTracker;

    [SerializeField]
    private MenuBase _pauseMenu;

    [SerializeField]
    private MenuBase _settingsMenu;

    [SerializeField]
    private MenuBase _initialMenu;

    [SerializeField]
    private GameObject _firstFocusItem;

    private void Awake()
    {
        MenuBase[] menus = GetComponentsInChildren<MenuBase>();

        foreach (MenuBase menu in menus)
        {
            menu.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventManager.OnPause += HandlePause;
        EventManager.OnCloseMenu += HandleCloseMenu;

        EventManager.OnMenuStackChange += HandleMenuStackChange;
    }

    private void Start()
    {
        _menuTracker.Reset();

        if (_initialMenu != null)
        {
            _menuTracker.PushMenu(_initialMenu);
        }

        if (_firstFocusItem != null)
        {
            EventSystem.current.SetSelectedGameObject(_firstFocusItem);
        }
    }

    private void OnDisable()
    {
        EventManager.OnPause -= HandlePause;
        EventManager.OnCloseMenu -= HandleCloseMenu;

        EventManager.OnMenuStackChange -= HandleMenuStackChange;
    }

    private void HandlePause()
    {
        _menuTracker.PushMenu(_pauseMenu);
    }

    private void HandleCloseMenu()
    {

        MenuBase currentMenu = _menuTracker.GetMenuOnTopOfStack();

        if (!currentMenu.CanBeClosed) return;

        _menuTracker.PopMenu();
    }

    private void HandleMenuStackChange(MenuBase prevMenu, MenuBase nextMenu)
    {

        //   Debug.Log("PREV: " + prevMenu?.gameObject.name + " NEXT: " + nextMenu?.gameObject.name);

        if (prevMenu != null)
        {
            prevMenu.gameObject.SetActive(false);
        }

        if (nextMenu != null)
        {
            nextMenu.gameObject.SetActive(true);
            EventManager.OnPauseGame.Invoke(this);
        }
        else
        {
            EventManager.OnResumeGame.Invoke(this);
        }
    }

}