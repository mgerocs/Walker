using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    [SerializeField]
    private InputTracker _inputTracker;

    public bool IsPaused { get; private set; } = false;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;

        EventManager.OnPauseGame += HandlePauseGame;
        EventManager.OnResumeGame += HandleResumeGame;
    }

    private void Start()
    {
        _inputTracker.SetGameplay();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;

        EventManager.OnPauseGame -= HandlePauseGame;
        EventManager.OnResumeGame -= HandleResumeGame;
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("SCENE LOADED");
    }

    private void HandlePauseGame()
    {
        if (!IsPaused)
        {
            IsPaused = true;
            Pause();
        }
    }

    private void HandleResumeGame()
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

        _inputTracker.SetUI();
    }

    private void Resume()
    {
        Time.timeScale = 1;

        _inputTracker.SetGameplay();
    }
}