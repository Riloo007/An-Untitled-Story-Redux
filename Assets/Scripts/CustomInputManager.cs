using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputManager : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    private void Start()
    {
        // Enable all input actions in the asset
        inputActionAsset.Enable();

        inputActionAsset.FindAction("Select").Enable();
        inputActionAsset.FindAction("Up").Enable();
        inputActionAsset.FindAction("Down").Enable();
    }

    // Example method to check if a key is pressed
    public bool GetActionPressed(string actionName)
    {
        var action = inputActionAsset.FindAction(actionName);
        return action.triggered && action.ReadValue<float>() > 0;
    }

    // Example method to check if a button is held down
    public bool GetAction(string actionName)
    {
        var action = inputActionAsset.FindAction(actionName);
        return action.ReadValue<float>() > 0;
    }
}
