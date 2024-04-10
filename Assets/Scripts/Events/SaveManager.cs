using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public PlayerController player;
    public SceneController sceneController;
    public Menu SlotMenu;
    public Menu NewSlotMenu;
    public Menu ConfirmOverwriteMenu;
    public UIController UIController;
    public StartGame StartGame;
    public TMP_InputField textinput;
    private int activeGameSlot;
    private string saveName;


    public TextMeshProUGUI[] slotNames1;
    public TextMeshProUGUI[] slotNames2;
    private SlotData[] saveData;
    

    void Start() {
        LoadData();
    }

    void LoadData() {
        saveData = new SlotData[3];

        // Grab all three slots
        saveData[0] = SaveFile.LoadData(0);
        saveData[1] = SaveFile.LoadData(1);
        saveData[2] = SaveFile.LoadData(2);

        // Set the save names in the UI
        for (int i = 0; i < 3; i++)
        {
            if(saveData[i] != null) {
                slotNames1[i].text = saveData[i].saveName;
                slotNames2[i].text = saveData[i].saveName;
            }
        }
    }
    
    public void NewGameInSlot(int index = -1) {
        // If there is a game in this slot, show confirmation menu
        if(index != -1) activeGameSlot = index;
        if(saveData[activeGameSlot] != null) {
            UIController.ShowMenu(ConfirmOverwriteMenu);
            return;
        }

        // Otherwise, write to the empty slot
        UIController.ShowMenu(NewSlotMenu);
    }

    public void ConfirmOverwrite() {
        UIController.ShowMenu(NewSlotMenu);
    }

    public void CancelOverwrite() {
        UIController.ShowMenu(SlotMenu);
    }

    public void NameChanged() {
        // newSaveName = n;
        saveName = textinput.text;
    }

    public void OverwriteAndStartGame() {
        // Do funny file stuff
        Debug.Log("New game started in slot " + activeGameSlot + " with player name " + saveName);
        
        // Start the game
        StartGame.Initiate();
    }

    public void LoadGameSlot(int index) {
        activeGameSlot = index;

        saveName = saveData[activeGameSlot].saveName;

        player.maxJumpCount = saveData[activeGameSlot].maxJumpCount;
        player.initialJumpVelocity = saveData[activeGameSlot].initialJumpVelocity;
        player.risingJumpDuration = saveData[activeGameSlot].risingJumpDuration;
        player.longJumpMultiplier = saveData[activeGameSlot].longJumpMultiplier;
        player.transform.position = new Vector3(saveData[activeGameSlot].position[0], saveData[activeGameSlot].position[1], saveData[activeGameSlot].position[2]);

        UIController.HideAllMenus();
        sceneController.overrideTransformEnabled = false;
        player.active = true;
    }

    public void SaveGame() {
        // Todo save game to file using activeGameSlot as an index
        SlotData data = new(player) {
            saveName = saveName
        };
        SaveFile.SaveData(data, activeGameSlot);
    }
}
