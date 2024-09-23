


using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{

    #region Input events
    public static UnityAction<Vector2> OnMove;
    public static UnityAction OnJump;
    public static UnityAction OnCancelJump;
    public static UnityAction OnToggleSprint;
    public static UnityAction<Vector2> OnRotateCamera;
    public static UnityAction OnInteract;
    public static UnityAction OnCancelInteract;
    public static UnityAction OnOpenMenu;
    public static UnityAction OnHighlightInteractables;
    public static UnityAction OnCancelHighlightInteractables;
    #endregion

    #region UI events
    public static UnityAction OnCloseMenu;
    #endregion




    public static UnityAction OnPauseGame;
    public static UnityAction OnResumeGame;

    public static UnityAction<MenuBase, MenuBase> OnMenuStackChange;

    public static UnityAction<GameObject> OnPlayerSpawn;

    public static UnityAction<SceneName, string> OnSceneChange;

    // SCENES
    public static UnityAction OnLoadingStart;
    public static UnityAction<float> OnLoadingProgress;
    public static UnityAction OnLoadingFinish;
    public static UnityAction OnActivateScene;
}


