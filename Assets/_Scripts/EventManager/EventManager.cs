


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

    // SCENE
    public static UnityAction<SceneData, string> OnExitScene;
    public static UnityAction<SceneData> FastTravel;

    public static UnityAction PauseGame;
    public static UnityAction ResumeGame;

    // UI
    public static UnityAction<MenuBase> ChangeMenu;
    public static UnityAction<int> OnMenuStackChanged;

    // PLAYER
    public static UnityAction OnPlayerSpawned;

    public static UnityAction<IInteractable> OnInteractableFound;
    public static UnityAction OnInteractableLost;

    public static UnityAction<ItemObject> OnItemPickedUp;


    public static UnityAction<NPCBase> StartDialog;
    public static UnityAction FinishDialog;
    public static UnityAction OnDialogStarted;
    public static UnityAction OnDialogFinished;

    public static UnityAction<Transform> TeleportPlayer;


    // SCENE
    public static UnityAction OnSceneLoaded;

    // SCENES
    public static UnityAction OnLoadingStart;
    public static UnityAction<float> OnLoadingProgress;
    public static UnityAction OnLoadingFinish;
}


