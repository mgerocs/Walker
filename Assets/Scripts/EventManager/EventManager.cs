using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{

    public static UnityAction<Component> OnPause;
    public static UnityAction<Component> OnResume;

    public static UnityAction<MenuBase, MenuBase> OnMenuStackChange;

    public static UnityAction<GameObject> OnPlayerSpawn;

    public static UnityAction OnSceneChange;

}


