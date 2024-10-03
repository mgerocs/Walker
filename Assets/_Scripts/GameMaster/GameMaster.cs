using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    public bool IsPaused { get; private set; } = false;

    public SceneTransitionManager SceneTransitionManager { get; private set; }

    [SerializeField]
    private InputTracker _inputTracker;


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
    }

    private void OnEnable()
    {
        EventManager.PauseGame += HandlePauseGame;
        EventManager.ResumeGame += HandleResumeGame;

        EventManager.OnMenuStackChanged += HandleMenuStackChanged;

        EventManager.StartDialog += HandleStartDialog;
        EventManager.OnDialogFinished += HandleDialogFinished;
    }

    private void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        EventManager.PauseGame -= HandlePauseGame;
        EventManager.ResumeGame -= HandleResumeGame;

        EventManager.OnMenuStackChanged -= HandleMenuStackChanged;

        EventManager.StartDialog -= HandleStartDialog;
        EventManager.OnDialogFinished -= HandleDialogFinished;
    }

    private void HandleMenuStackChanged(int menuCount)
    {
        if (menuCount > 0)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Init()
    {
        IsPaused = false;

        Time.timeScale = 1;

        _inputTracker.SetGameplay();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandlePauseGame()
    {
        Pause();
    }

    private void HandleResumeGame()
    {
        Resume();
    }

    private void HandleStartDialog(NPCBase arg0)
    {
        _inputTracker.SetUI();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HandleDialogFinished()
    {
        _inputTracker.SetGameplay();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Pause()
    {
        if (IsPaused) return;

        IsPaused = true;

        Time.timeScale = 0;

        _inputTracker.SetUI();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Resume()
    {
        if (!IsPaused) return;

        IsPaused = false;

        Time.timeScale = 1;

        _inputTracker.SetGameplay();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}