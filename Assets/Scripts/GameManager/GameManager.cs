using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private InputReader _inputReader;

    [SerializeField]
    private MenuTracker _menuTracker;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        EventManager.OnPause += HandlePause;
        EventManager.OnResume += HandleResume;
    }

    private void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        EventManager.OnPause -= HandlePause;
        EventManager.OnResume -= HandleResume;
    }

    public bool IsPaused { get; private set; } = false;

    private void HandlePause(Component component)
    {
        if (!IsPaused)
        {
            IsPaused = true;
            Pause();
        }
    }

    private void HandleResume(Component component)
    {
        if (IsPaused)
        {
            IsPaused = false;
            Resume();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;

        _inputReader.SetUI();
    }

    private void Resume()
    {
        Time.timeScale = 1;

        _inputReader.SetGameplay();
    }

    private void Init()
    {
        Time.timeScale = 1;

        _inputReader.SetGameplay();
    }
}
