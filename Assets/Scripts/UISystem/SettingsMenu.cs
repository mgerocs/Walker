using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class SettingsMenu : MenuBase
{
    [SerializeField]
    private MenuTracker _menuTracker;

}
