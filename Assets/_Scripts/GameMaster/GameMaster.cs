using System;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    public bool IsPaused { get; private set; } = false;

    public SceneTransitionManager SceneTransitionManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }

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

        SceneTransitionManager = GetComponentInChildren<SceneTransitionManager>();
        PlayerManager = GetComponentInChildren<PlayerManager>();

        if (SceneTransitionManager == null)
        {
            throw new Exception("No SceneTransitionManager.");
        }

        if (PlayerManager == null)
        {
            throw new Exception("No PlayerManager.");
        }
    }

    private void OnEnable()
    {
        EventManager.PauseGame += HandlePauseGame;
        EventManager.ResumeGame += HandleResumeGame;
    }

    private void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        EventManager.PauseGame -= HandlePauseGame;
        EventManager.ResumeGame -= HandleResumeGame;
    }

    public void Init()
    {
        Time.timeScale = 1;

        _inputTracker.SetGameplay();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Resume()
    {
        Time.timeScale = 1;

        _inputTracker.SetGameplay();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}