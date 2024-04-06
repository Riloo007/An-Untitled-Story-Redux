using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIListController : MonoBehaviour
{
    public CustomInputManager Input;
    public TextMeshProUGUI[] texts;
    // public UnityEvent[] menuActions;
    public bool uiDisabled;
    public int SelectedIndex = 0;

    private float[] oldTextAlphas;
    private float[] textTargetAlphas;



    private CanvasGroup canvasGroup;
    private float canvasTimer = 0f;
    private float listTimer = 0f;
    private float previousCanvasAlpha = 0f;
    private readonly float uiTransitionLength = .2f;
    private readonly float listTransitionLength = 0.2f;

    public void Disable() {
        if(!canvasGroup) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        
        uiDisabled = true;
        previousCanvasAlpha = canvasGroup.alpha;
        canvasTimer = 0; 
    }

    public void Enable() {
        gameObject.SetActive(true);
        if(!canvasGroup) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        
        uiDisabled = false;
        previousCanvasAlpha = canvasGroup.alpha;
        canvasTimer = 0;
    }

    void Start()
    {
        if(!canvasGroup) canvasGroup = gameObject.AddComponent<CanvasGroup>();

        if(texts.Length == 0) return;
        
        oldTextAlphas = new float[texts.Length];
        textTargetAlphas = new float[texts.Length];

        for(int i = 0; i < texts.Length; i++) {
            oldTextAlphas[i] = 0.2f;
            textTargetAlphas[i] = 0.2f;
        }

        textTargetAlphas[0] = 1f;

        // Input System
        // InputAction up = inputActionsAsset.FindAction("Up");
        // InputAction down = inputActionsAsset.FindAction("Down");
        // InputAction select = inputActionsAsset.FindAction("Select");
        // up.Enable();
        // down.Enable();
        // select.Enable();
        // up.performed += SelectNext;
        // down.performed += SelectPrevious;
        // select.performed += SelectCurrent;
    }

    void Update()
    {
        Animate();
        if(uiDisabled) return;

        if(Input.GetActionPressed("Select")) SelectCurrent();
        if(Input.GetActionPressed("Up")) SelectPrevious();
        if(Input.GetActionPressed("Down")) SelectNext();
    }

    void ResetOldAlphas() {
        listTimer = 0f;
        for(int i = 0; i < texts.Length; i++) {
            oldTextAlphas[i] = texts[i].alpha;
        }
    }

    void SelectNext(InputAction.CallbackContext context = new InputAction.CallbackContext()) {
        if(SelectedIndex >= texts.Length - 1) return;

        textTargetAlphas[SelectedIndex] = 0.2f;
        textTargetAlphas[SelectedIndex + 1] = 1f;

        ResetOldAlphas();
        SelectedIndex++;
    }
    void SelectPrevious(InputAction.CallbackContext context = new InputAction.CallbackContext()) {
        if(SelectedIndex <= 0) return;

        textTargetAlphas[SelectedIndex] = 0.2f;
        textTargetAlphas[SelectedIndex - 1] = 1f;

        ResetOldAlphas();
        SelectedIndex--;
    }
    void SelectCurrent(InputAction.CallbackContext context = new InputAction.CallbackContext()) {
        if(texts[SelectedIndex].gameObject.TryGetComponent<Button>(out var btn)) btn.onClick.Invoke();
        // menuActions[SelectedIndex].Invoke();
    }

    void Animate() {
        canvasTimer += Time.deltaTime;
        listTimer += Time.deltaTime;

        if(uiDisabled) {
            canvasGroup.alpha = Mathf.SmoothStep(previousCanvasAlpha, 0, canvasTimer/uiTransitionLength);

            if(canvasTimer >= uiTransitionLength) gameObject.SetActive(false);
            return;
        } else {
            gameObject.SetActive(true);
        }

        canvasGroup.alpha = Mathf.SmoothStep(previousCanvasAlpha, 1, canvasTimer/uiTransitionLength);

        for(int i = 0; i < texts.Length; i++) {
            TextMeshProUGUI text = texts[i];
            text.alpha = Mathf.SmoothStep(oldTextAlphas[i], textTargetAlphas[i], listTimer/listTransitionLength);
        }
    }
}
