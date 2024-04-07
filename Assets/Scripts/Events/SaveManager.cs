using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Menu NewSlotMenu;
    public UIController UIController;
    public StartGame StartGame;
    public TMP_InputField textinput;
    private int activeGameSlot;
    private string newSaveName;

    public void SelectSlot(int index) {
        UIController.ShowMenu(NewSlotMenu);
        activeGameSlot = index;
    }

    public void NameChanged() {
        // newSaveName = n;
        newSaveName = textinput.text;
    }

    public void OverwriteAndStartGame() {
        // Do funny file stuff
        Debug.Log("New game started in slot " + activeGameSlot + " with player name " + newSaveName);
        
        // Start the game
        StartGame.Initiate();
    }

    public void LoadGameSlot(int index) {
        activeGameSlot = index;
    }

    public void SaveGame() {
        // Todo save game to file using activeGameSlot as an index
    }
}
