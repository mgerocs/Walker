using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : ScreenBase, IScreen
{
    [SerializeField]
    private Image _progressBar;

    private void OnEnable()
    {
        EventManager.OnLoadingProgress += HandleLoadingProgress;
    }

    private void OnDisable()
    {
        EventManager.OnLoadingProgress -= HandleLoadingProgress;
    }

    private void HandleLoadingProgress(float progress)
    {
        _progressBar.fillAmount = progress;
    }
}