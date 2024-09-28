using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuTracker", menuName = "Scriptable Objects/MenuTracker")]
public class MenuTracker : ScriptableObject
{
    private Stack<MenuBase> _menuStack = new();

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

        if (_menuStack.Count > 0)
        {
            MenuBase prevMenu = _menuStack.Peek();

            if (prevMenu.ExitOnNewPagePush)
            {
                prevMenu.Exit();
            }
        }

        _menuStack.Push(nextMenu);
    }

    public void PopMenu()
    {
        if (_menuStack.Count > 0)
        {
            MenuBase prevMenu = _menuStack.Pop();

            prevMenu.Exit();

            if (_menuStack.Count > 0)
            {
                MenuBase nextMenu = _menuStack.Peek();

                if (nextMenu.ExitOnNewPagePush)
                {
                    nextMenu.Enter();
                }
            }
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
            MenuBase menu = _menuStack.Peek();
            menu.Exit();
        }

        _menuStack = new();
    }
}
