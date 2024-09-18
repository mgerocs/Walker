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

        OnMenuStackChange(prevMenu, nextMenu);
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

            OnMenuStackChange(prevMenu, nextMenu);
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

        OnMenuStackChange(null, null);
    }

    public void Reset()
    {
        for (int i = 0; i < _menuStack.Count; i++)
        {
            _menuStack.Pop();
        }

        OnMenuStackChange(null, null);
    }

    private readonly Stack<MenuBase> _menuStack = new();

    private void OnMenuStackChange(MenuBase prevMenu, MenuBase nextMenu)
    {
        EventManager.OnMenuStackChange.Invoke(prevMenu, nextMenu);
    }
}
