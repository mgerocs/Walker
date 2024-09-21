


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
    public static UnityAction OnPause;
    public static UnityAction OnHighlightInteractables;
    public static UnityAction OnCancelHighlightInteractables;
    #endregion

    #region UI events
    public static UnityAction OnCloseMenu;
    #endregion




    public static UnityAction<Component> OnPauseGame;
    public static UnityAction<Component> OnResumeGame;

    public static UnityAction<MenuBase, MenuBase> OnMenuStackChange;

    public static UnityAction<GameObject, SpawnPoint> OnPlayerSpawn;

    public static UnityAction OnSceneChange;

}


