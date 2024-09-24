using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private MenuTracker _menuTracker;

    [SerializeField]
    private MenuBase _pauseMenu;

    [SerializeField]
    private SettingsMenu _settingsMenu;

    [SerializeField]
    private ScreenBase _loadingScreen;

    [SerializeField]
    private ScreenBase _transitionScreen;

    [SerializeField]
    private MenuBase _initalMenu;

    [SerializeField]
    private GameObject _firstFocusItem;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;

        EventManager.OnOpenMenu += HandleOpenMenu;
        EventManager.OnCloseMenu += HandleCloseMenu;

        EventManager.OnLoadingStart += HandleLoadingStart;
        EventManager.OnLoadingFinish += HandleLoadingFinish;
    }

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
    }

    private void Start()
    {
        ResetUI();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;

        EventManager.OnOpenMenu -= HandleOpenMenu;
        EventManager.OnCloseMenu -= HandleCloseMenu;

        EventManager.OnLoadingStart -= HandleLoadingStart;
        EventManager.OnLoadingFinish -= HandleLoadingFinish;
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        ResetUI();
    }

    private void ResetUI()
    {
        MenuBase[] menusInScene = FindObjectsByType<MenuBase>(FindObjectsSortMode.None);

        foreach (MenuBase menu in menusInScene)
        {
            menu.gameObject.SetActive(false);
        }

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
    }

    private void HandleOpenMenu()
    {
        _menuTracker.PushMenu(_pauseMenu);
    }

    private void HandleCloseMenu()
    {
        MenuBase currentMenu = _menuTracker.GetMenuOnTopOfStack();

        if (!currentMenu.CanBeClosed) return;

        _menuTracker.PopMenu();
    }

    private void HandleLoadingStart()
    {
        _loadingScreen.gameObject.SetActive(true);
    }

    private void HandleLoadingFinish()
    {
        _loadingScreen.gameObject.SetActive(false);
    }
}
