using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    // General
    public bool gamePaused = false;

    // UI
    public UIController UIController;
    public Menu PauseMenu;

    // Rendering
    public Material BlurVMaterial;
    public Material BlurHMaterial;
    public RawImage BlurVImage;
    public RawImage BlurHImage;


    void Start()
    {
        UIController.ShowMenu(UIController.MainMenu);
        // UIController.ShowMenu(0);
    }

    public void PauseGame() {
        gamePaused = true;
        BlurHImage.material = BlurHMaterial;
        BlurVImage.material = BlurVMaterial;
        // UIController.ShowMenu(3);
        UIController.ShowMenu(PauseMenu);
    }
    public void UnPauseGame() {
        gamePaused = false;
        BlurHImage.material = null;
        BlurVImage.material = null;
        UIController.HideAllMenus();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(gamePaused) UnPauseGame(); else PauseGame();
        };
    }
}
