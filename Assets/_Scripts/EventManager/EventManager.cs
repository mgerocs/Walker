


using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region Input events
    public static UnityAction<Vector2> OnMove;
    public static UnityAction<Vector2> OnLook;
    public static UnityAction<float> OnZoom;
    public static UnityAction OnJump;
    public static UnityAction OnCancelJump;
    public static UnityAction OnToggleSprint;
    public static UnityAction OnInteract;
    public static UnityAction OnCancelInteract;
    public static UnityAction OnOpenPauseMenu;
    public static UnityAction OnHighlightInteractables;
    public static UnityAction OnCancelHighlightInteractables;
    public static UnityAction OnMap;
    #endregion

    #region UI events
    public static UnityAction OnCloseMenu;
    #endregion

    public static UnityAction OnPauseGame;
    public static UnityAction OnResumeGame;

    public static UnityAction<GameObject> OnSpawnPlayer;
    public static UnityAction<SceneField, string> OnChangeScene;
    public static UnityAction<MenuBase> OnChangeMenu;

    // SCENES
    public static UnityAction OnLoadingStart;
    public static UnityAction<float> OnLoadingProgress;
    public static UnityAction OnLoadingFinish;
}


