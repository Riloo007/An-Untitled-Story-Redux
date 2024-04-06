using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public PlayerController playerController;
    public bool overrideTransformEnabled = true;
    public Transform overrideTransform;
    public Transform cameraTransform;
    public Transform playerTransform;
    public float loadDistance = 5f; // Distance threshold for loading backgrounds
    public GameObject backgroundParent; // Reference to the parent GameObject containing background prefabs
    public Image borderColor;
    public Transform closestSceneTransform;

    private Transform targetTransform;
    private float panTimer = 999;
    private float panLength = 999;
    private Vector3 oldCameraPos;

    void Update()
    {
        cameraTransform.position = new Vector3(closestSceneTransform.position.x, closestSceneTransform.position.y, -10f);
        if(closestSceneTransform.gameObject.TryGetComponent<SceneColor>(out var sc)) borderColor.color = sc.color;
        else borderColor.color = Color.black;

        // Update Scenes

        float smallestDistance = 10f;
        foreach (Transform sceneTransform in backgroundParent.transform)
        {
            
            float distanceToScene = Vector3.Distance(overrideTransformEnabled ? overrideTransform.position : playerTransform.position, sceneTransform.position);
            if(distanceToScene < smallestDistance) {
                smallestDistance = distanceToScene;
                closestSceneTransform = sceneTransform;
            }

            // Load the scene if it's within a reasonable distance
            if (distanceToScene < loadDistance || sceneTransform.gameObject.CompareTag("AlwaysLoaded")) sceneTransform.gameObject.SetActive(true);
            else sceneTransform.gameObject.SetActive(false);
        }

        AnimationClock();
    }

    void AnimationClock() {
        if(panTimer < panLength) { // Animation is not yet finished, calculate the next frame
            // overrideTransform.position = Vector3.Lerp(oldCameraPos, targetTransform.position, panTimer/panLength);

            overrideTransform.position = new Vector3(
                 Mathf.SmoothStep(oldCameraPos.x, targetTransform.position.x, panTimer/panLength),
                 Mathf.SmoothStep(oldCameraPos.y, targetTransform.position.y, panTimer/panLength),
                 Mathf.SmoothStep(oldCameraPos.z, targetTransform.position.z, panTimer/panLength));
            
            panTimer += Time.deltaTime;
            if(panTimer >= panLength) {
                overrideTransformEnabled = false;
                playerController.active = true;
            }
        }
    }

    public void PanToLocation(Transform tgt, float duration) {
        targetTransform = tgt;
        panLength = duration;
        panTimer = 0;
        oldCameraPos = cameraTransform.position;
        overrideTransform.position = oldCameraPos;
        overrideTransformEnabled = true;
        playerController.active = false;
    }
}
