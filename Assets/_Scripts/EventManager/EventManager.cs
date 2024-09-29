


using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region Input events
    public static UnityAction<Vector2> Move;
    public static UnityAction<Vector2> Look;
    public static UnityAction<float> Zoom;
    public static UnityAction Jump;
    public static UnityAction CancelJump;
    public static UnityAction ToggleSprint;
    public static UnityAction Interact;
    public static UnityAction CancelInteract;
    public static UnityAction OpenPauseMenu;
    public static UnityAction HighlightInteractables;
    public static UnityAction CancelHighlightInteractables;
    public static UnityAction OpenMap;
    public static UnityAction OpenInventory;
    #endregion

    #region UI events
    public static UnityAction CloseMenu;
    #endregion

    public static UnityAction PauseGame;
    public static UnityAction ResumeGame;


    // UI
    public static UnityAction<MenuBase> ChangeMenu;

    // PLAYER
    public static UnityAction<GameObject> OnPlayerSpawned;
    public static UnityAction<InteractableBase> OnInteractableFound;
    public static UnityAction OnInteractableLost;

    // SCENE
    public static UnityAction OnSceneLoaded;

    // SCENES
    public static UnityAction OnLoadingStart;
    public static UnityAction<float> OnLoadingProgress;
    public static UnityAction OnLoadingFinish;
}


