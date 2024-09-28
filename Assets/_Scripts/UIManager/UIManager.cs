using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private MenuTracker _menuTracker;

    [SerializeField]
    private PauseMenu _pauseMenu;

    [SerializeField]
    private SettingsMenu _settingsMenu;

    [SerializeField]
    private LocalMap _localMap;

    [SerializeField]
    private ScreenBase _loadingScreen;

    [SerializeField]
    private ScreenBase _transitionScreen;

    [SerializeField]
    private MenuBase _initalMenu;

    [SerializeField]
    private GameObject _firstFocusItem;

    private MenuBase[] _menus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist GameManager and its components
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager objects
        }

        _menus = FindObjectsByType<MenuBase>(FindObjectsSortMode.None);
    }

    private void OnEnable()
    {
        EventManager.OnOpenPauseMenu += HandleOpenPauseMenu;
        EventManager.OnCloseMenu += HandleCloseMenu;

        EventManager.OnChangeMenu += HandleChangeMenu;

        EventManager.OnMap += HandleMap;

        EventManager.OnLoadingStart += HandleLoadingStart;
        EventManager.OnLoadingFinish += HandleLoadingFinish;
    }

    private void OnDisable()
    {
        EventManager.OnOpenPauseMenu -= HandleOpenPauseMenu;
        EventManager.OnCloseMenu -= HandleCloseMenu;

        EventManager.OnChangeMenu -= HandleChangeMenu;

        EventManager.OnMap -= HandleMap;

        EventManager.OnLoadingStart -= HandleLoadingStart;
        EventManager.OnLoadingFinish -= HandleLoadingFinish;
    }

    public void Init()
    {
        _menuTracker.PopAllMenus();

        UpdateMenuStatus();

        if (_transitionScreen != null)
        {
            _transitionScreen.gameObject.SetActive(true);
        }

        if (_loadingScreen != null)
        {
            _loadingScreen.gameObject.SetActive(false);
        }

        if (_initalMenu != null)
        {
            _menuTracker.PushMenu(_initalMenu);
        }

        if (_firstFocusItem != null)
        {
            EventSystem.current.SetSelectedGameObject(_firstFocusItem);
        }
    }

    private void HandleOpenPauseMenu()
    {
        _menuTracker.PushMenu(_pauseMenu);

        UpdateMenuStatus();
    }

    private void HandleCloseMenu()
    {
        MenuBase currentMenu = _menuTracker.GetMenuOnTopOfStack();

        if (!currentMenu.CanBeClosed) return;

        _menuTracker.PopMenu();

        UpdateMenuStatus();
    }

    private void HandleChangeMenu(MenuBase menu)
    {
        _menuTracker.PushMenu(menu);

        UpdateMenuStatus();
    }

    private void HandleMap()
    {
        _menuTracker.PushMenu(_localMap);

        UpdateMenuStatus();
    }

    private void HandleLoadingStart()
    {
        _loadingScreen.gameObject.SetActive(true);
    }

    private void HandleLoadingFinish()
    {
        _loadingScreen.gameObject.SetActive(false);
    }

    private void UpdateMenuStatus()
    {
        foreach (MenuBase menu in _menus)
        {
            bool isActive = _menuTracker.IsMenuOnTopOfStack(menu);

            menu.gameObject.SetActive(isActive);
        }

        if (_menuTracker.StackCount() > 0)
        {
            EventManager.OnPauseGame?.Invoke();
        }
        else
        {
            EventManager.OnResumeGame?.Invoke();
        }
    }
}
