using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{

    public static UnityAction<Component> OnPause;
    public static UnityAction<Component> OnResume;

    public static UnityAction<MenuBase, MenuBase> OnMenuStackChange;

}


