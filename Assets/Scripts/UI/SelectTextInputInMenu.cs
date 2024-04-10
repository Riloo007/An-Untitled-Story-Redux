using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectTextInputInMenu : MonoBehaviour
{
    public CustomInputManager Input;
    public TMP_InputField input;
    public Menu menu;
    private int previousIndex;
    private bool previouslyDisabled;

    void Update()
    {
        if(
            (previouslyDisabled && !menu.uiDisabled) ||
            (menu.SelectedIndex == 0 && previousIndex == 1)
        ) {
            input.Select();
        } else {
            if(menu.SelectedIndex == 0) input.ActivateInputField();
            else input.DeactivateInputField();
        }

        if(input.IsActive() && Input.GetActionPressed("Submit")) {
            menu.SelectNext();
            menu.SelectNext();
        }

        previousIndex = menu.SelectedIndex;
        previouslyDisabled = menu.uiDisabled;
    }
}
