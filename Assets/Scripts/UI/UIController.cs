using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.PackageManager;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public CustomInputManager Input;
    public Menu MainMenu;
    public Menu[] Menus;

    void OnValidate() {
        foreach (var menu in Menus)
        {
            if(menu != null) menu.Input = Input;
        }
    }

    void Start() {
        ShowMenu(MainMenu);
    }

    public void ShowMenu(Menu menu) {
        HideAllMenus();
        menu.Enable();
    }

    public void ShowMenuByIndex(int index) {
        HideAllMenus();
        Menus[index].Enable();
    }

    public void HideAllMenus() {
        foreach (var menu in Menus) menu.Disable();
    }
}
