using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuTracker", menuName = "Scriptable Objects/MenuTracker")]
public class MenuTracker : ScriptableObject
{
    public List<MenuBase> MenuList { get; set; }

    public Stack<MenuBase> MenuStack { get; private set; } = new();

    public int StackCount()
    {
        return MenuStack.Count;
    }

    public MenuBase GetMenuOnTopOfStack()
    {
        if (MenuStack.Count > 0)
        {
            return MenuStack.Peek();
        }
        else
        {
            return null;
        }
    }

    public bool IsMenuInStack(MenuBase menu)
    {
        return MenuStack.Contains(menu);
    }

    public bool IsMenuOnTopOfStack(MenuBase menu)
    {
        return MenuStack.Count > 0 && menu == MenuStack.Peek();
    }

    public void PushMenu(MenuBase nextMenu)
    {
        nextMenu.Enter();

        if (MenuStack.Count > 0)
        {
            MenuBase prevMenu = MenuStack.Peek();

            if (prevMenu.ExitOnNewPagePush)
            {
                prevMenu.Exit();
            }
        }

        MenuStack.Push(nextMenu);
    }

    public void PopMenu()
    {
        if (MenuStack.Count > 0)
        {
            MenuBase prevMenu = MenuStack.Pop();

            prevMenu.Exit();

            if (MenuStack.Count > 0)
            {
                MenuBase nextMenu = MenuStack.Peek();

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
        MenuStack = new();
    }
}
