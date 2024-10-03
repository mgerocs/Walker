using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour
{
    public static UIMaster Instance;

    [SerializeField]
    private MenuTracker _menuTracker;

    [SerializeField]
    private PauseMenu _pauseMenu;

    [SerializeField]
    private SettingsMenu _settingsMenu;

    [SerializeField]
    private LocalMapMenu _localMap;

    [SerializeField]
    private InventoryMenu _inventory;

    [SerializeField]
    private LoadingScreen _loadingScreen;

    [SerializeField]
    private TransitionScreen _transitionScreen;

    [SerializeField]
    private DialogScreen _dialogScreen;

    [SerializeField]
    private MenuBase _initalMenu;

    [SerializeField]
    private GameObject _firstFocusItem;

    private MenuBase[] _menus;
    private ScreenBase[] _screens;

    private bool _inDialog;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _menus = FindObjectsByType<MenuBase>(FindObjectsSortMode.None);
        _screens = FindObjectsByType<ScreenBase>(FindObjectsSortMode.None);

        if (_menuTracker == null)
        {
            Debug.LogError("Missing MenuTracker reference.");
        }

        if (_pauseMenu == null)
        {
            Debug.LogError("Missing PauseMenu reference.");
            return;
        }

        if (_localMap == null)
        {
            Debug.LogError("Missing LocalMapMenu reference.");
            return;
        }

        if (_inventory == null)
        {
            Debug.LogError("Missing InventoryMenu reference.");
            return;
        }

        if (_loadingScreen == null)
        {
            Debug.LogError("Missing LoadingScreen reference.");
            return;
        }

        if (_transitionScreen == null)
        {
            Debug.LogError("Missing TransitionScreen reference.");
            return;
        }

        if (_dialogScreen == null)
        {
            Debug.LogError("Missing DialogScreen reference.");
            return;
        }
    }

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;

        EventManager.OpenPauseMenu += HandleOpenPauseMenu;
        EventManager.CloseMenu += HandleCloseMenu;

        EventManager.ChangeMenu += HandleChangeMenu;

        EventManager.OpenMap += HandleOpenMap;
        EventManager.OpenInventory += HandleOpenInventory;

        EventManager.OnLoadingStart += HandleLoadingStart;
        EventManager.OnLoadingFinish += HandleLoadingFinish;

        EventManager.StartDialog += HandleStartDialog;
        EventManager.FinishDialog += HandleFinishDialog;
        EventManager.OnDialogStarted += HandleDialogStarted;
        EventManager.OnDialogFinished += HandleDialogFinished;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;

        EventManager.OpenPauseMenu -= HandleOpenPauseMenu;
        EventManager.CloseMenu -= HandleCloseMenu;

        EventManager.ChangeMenu -= HandleChangeMenu;

        EventManager.OpenMap -= HandleOpenMap;
        EventManager.OpenInventory -= HandleOpenInventory;

        EventManager.OnLoadingStart -= HandleLoadingStart;
        EventManager.OnLoadingFinish -= HandleLoadingFinish;

        EventManager.StartDialog -= HandleStartDialog;
        EventManager.FinishDialog -= HandleFinishDialog;
        EventManager.OnDialogStarted -= HandleDialogStarted;
        EventManager.OnDialogFinished -= HandleDialogFinished;
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Init();
    }

    public void Init()
    {
        foreach (MenuBase menu in _menus)
        {
            menu.gameObject.SetActive(false);
        }

        foreach (ScreenBase screen in _screens)
        {
            screen.gameObject.SetActive(false);
        }

        if (_menuTracker == null) return;

        _menuTracker.Reset();

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

        EventManager.OnMenuStackChanged?.Invoke(_menuTracker.StackCount());
    }

    private void HandleOpenPauseMenu()
    {
        if (_pauseMenu == null) return;

        OpenMenu(_pauseMenu);
    }

    private void HandleCloseMenu()
    {
        if (_inDialog) return;

        CloseMenu();
    }

    private void HandleChangeMenu(MenuBase menu)
    {
        OpenMenu(menu);
    }

    private void HandleOpenMap()
    {
        if (_localMap == null) return;

        OpenMenu(_localMap);
    }

    private void HandleOpenInventory()
    {
        if (_inventory == null) return;

        OpenMenu(_inventory);
    }

    private void HandleLoadingStart()
    {
        if (_loadingScreen == null) return;

        _loadingScreen.gameObject.SetActive(true);
    }

    private void HandleLoadingFinish()
    {
        if (_loadingScreen == null) return;

        _loadingScreen.gameObject.SetActive(false);
    }

    private void HandleStartDialog(NPCBase arg0)
    {
        _inDialog = true;
    }

    private void HandleDialogStarted()
    {
        if (_dialogScreen == null) return;

        _dialogScreen.gameObject.SetActive(true);
    }

    private void HandleFinishDialog()
    {
        if (_dialogScreen == null) return;

        _dialogScreen.gameObject.SetActive(false);
    }

    private void HandleDialogFinished()
    {
        _inDialog = false;
    }

    private void OpenMenu(MenuBase menu)
    {
        if (_menuTracker == null) return;

        _menuTracker.PushMenu(menu);

        EventManager.OnMenuStackChanged?.Invoke(_menuTracker.StackCount());
    }

    private void CloseMenu()
    {
        if (_menuTracker == null) return;

        MenuBase currentMenu = _menuTracker.GetMenuOnTopOfStack();

        if (!currentMenu.CanBeClosed) return;

        _menuTracker.PopMenu();

        EventManager.OnMenuStackChanged?.Invoke(_menuTracker.StackCount());
    }
}
