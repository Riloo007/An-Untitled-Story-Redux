using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [TextArea]
    public string UnityWarning = "These menus are set in a list to make it easy to add more, WHICH MEANS you cannot reorder them as defined here, otherwise scripts which use SetMenu(index) will need to be adjusted to show the correct menu.";
    public UIListController[] Menus;

    public void ShowMainMenu() {
        ShowMenu(0);
    }

    public void ShowMenu(int index) {

        foreach (var menu in Menus) menu.Disable();
        Menus[index].Enable();
    }

    public void HideAllMenus() {
        foreach (var menu in Menus) menu.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowMenu(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
