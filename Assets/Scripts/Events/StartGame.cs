using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public UIController UIController;
    public Transform skyBGTransform;
    public Transform hillsBGTransform;
    public Transform openingSceneTransform;
    public Transform cameraTransform;
    public PlayerController playerController;
    public SceneController sceneController;
    public float transitionLength = 5f;
    // public UIController UIController;


    private bool initiated = false;
    private float timer = 0f;

    void Start()
    {
        playerController.active = false;
    }

    public void Initiate() {
        initiated = true;

        UIController.HideAllMenus();
        
        sceneController.PanToLocation(openingSceneTransform, transitionLength);
        skyBGTransform.parent = cameraTransform;
        skyBGTransform.position = new Vector3(skyBGTransform.position.x, skyBGTransform.position.y, 1f);

        Debug.Log("STARTED GAME");
    }

    void Update()
    {
        if(!initiated) return;

        timer += Time.deltaTime;
        

        if(timer > transitionLength) {
            playerController.active = true;
            return;
        };
    }

    void FixedUpdate() {
        hillsBGTransform.position = new Vector3(
            0f,
            Mathf.SmoothStep(-1.25f, -2.5f, timer/transitionLength),
            0f);
    }
}
