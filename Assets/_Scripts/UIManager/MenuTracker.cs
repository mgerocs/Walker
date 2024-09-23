using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuTracker", menuName = "Scriptable Objects/MenuTracker")]
public class MenuTracker : ScriptableObject
{
    public bool HasMenuInStack()
    {
        return _menuStack.Count > 0;
    }

    public int StackCount()
    {
        return _menuStack.Count;
    }

    public MenuBase GetMenuOnTopOfStack()
    {
        if (_menuStack.Count > 0)
        {
            return _menuStack.Peek();
        }
        else
        {
            return null;
        }
    }

    public bool IsMenuInStack(MenuBase menu)
    {
        return _menuStack.Contains(menu);
    }

    public bool IsMenuOnTopOfStack(MenuBase menu)
    {
        return _menuStack.Count > 0 && menu == _menuStack.Peek();
    }

    public void PushMenu(MenuBase nextMenu)
    {
        nextMenu.Enter();

        MenuBase prevMenu = null;

        if (_menuStack.Count > 0)
        {
            prevMenu = _menuStack.Peek();

            if (prevMenu.ExitOnNewPagePush)
            {
                prevMenu.Exit();
            }
        }

        _menuStack.Push(nextMenu);

        UpdateMenuStatus(prevMenu, nextMenu);
    }

    public void PopMenu()
    {
        if (_menuStack.Count > 0)
        {
            MenuBase prevMenu = _menuStack.Pop();

            prevMenu.Exit();

            MenuBase nextMenu = null;

            if (_menuStack.Count > 0)
            {
                nextMenu = _menuStack.Peek();

                if (nextMenu.ExitOnNewPagePush)
                {
                    nextMenu.Enter();
                }
            }

            UpdateMenuStatus(prevMenu, nextMenu);
        }
        else
        {
            Debug.LogWarning("No menu in stack.");
        }
    }

    public void PopAllMenus()
    {
        for (int i = 0; i < _menuStack.Count; i++)
        {
            PopMenu();
        }

        UpdateMenuStatus(null, null);
    }

    public void Reset()
    {
        _menuStack = new();
    }

    private Stack<MenuBase> _menuStack = new();

    private void UpdateMenuStatus(MenuBase prevMenu, MenuBase nextMenu)
    {
        // Debug.Log("PREV: " + prevMenu?.gameObject.name + " NEXT: " + nextMenu?.gameObject.name);

        if (prevMenu != null)
        {
            prevMenu.gameObject.SetActive(false);
        }

        if (nextMenu != null)
        {
            nextMenu.gameObject.SetActive(true);
            EventManager.OnPauseGame?.Invoke();
        }
        else
        {
            EventManager.OnResumeGame?.Invoke();
        }
    }
}
