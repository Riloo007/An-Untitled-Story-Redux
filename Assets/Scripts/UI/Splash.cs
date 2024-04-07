using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Sorry this script looks so garbagelike 🙃
// It's only used once and I have no big plans to expand on it so here we are in an 'it works' state.

public class Splash : MonoBehaviour
{
    public UIController UIController;
    public CustomInputManager Input;
    public CanvasGroup blackPanel;
    public CanvasGroup splashImage;
    public GameObject splashScreen;
    private float timer;
    public float fadeDuration = .5f;
    public float holdDuration = 1f;

    private int state = 0;
    private float stateDuration;



    // Start is called before the first frame update
    void Start()
    {
        stateDuration = fadeDuration;
        splashScreen.SetActive(true);
        
        // InputAction select = inputActionsAsset.FindAction("Select");
        // select.Enable();
        // select.performed += Skip;

        UIController.HideAllMenus();
    }

    void Skip() {
        timer = 0;
        stateDuration = 0.1f;
        state = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetActionPressed("Select")) Skip();
        

        timer += Time.deltaTime;
        if(timer > stateDuration) {
            timer = 0;
            state += 1;
        }

        if(state == 0) { // Fade in image
            splashImage.alpha = Mathf.SmoothStep(0, 1, timer/fadeDuration);
        }

        if(state == 1) { // Hold image on screen
            splashImage.alpha = 1f;
            stateDuration = holdDuration;
        }

        if(state == 2) { // Fade out image
            splashImage.alpha = Mathf.SmoothStep(1, 0, timer/fadeDuration);
            blackPanel.alpha = Mathf.SmoothStep(1, 0, timer/fadeDuration);
            stateDuration = fadeDuration;
        }

        if(state == 3) { // Remove used groups and stuff
            UIController.ShowMenu(UIController.MainMenu);
            Destroy(splashScreen);
            Destroy(GetComponent<Splash>());
        }
    }
}
