using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputReader inputReader;

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
        inputReader.OpenMenuEvent += HandleOpenMenu;
        inputReader.CloseMenuEvent += HandleCloseMenu;
    }

    private void Start()
    {

    }

    private void OnDisable()
    {
        inputReader.OpenMenuEvent -= HandleOpenMenu;
        inputReader.CloseMenuEvent -= HandleCloseMenu;
    }

    private void HandleOpenMenu()
    {
        Pause();
    }

    private void HandleCloseMenu()
    {
        Resume();
    }

    private void Pause()
    {
        Time.timeScale = 0;

        inputReader.SetUI();
    }

    private void Resume()
    {
        Time.timeScale = 1;

        inputReader.SetGameplay();
    }
}
