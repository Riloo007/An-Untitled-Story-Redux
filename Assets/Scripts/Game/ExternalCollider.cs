using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class ExternalCollider : MonoBehaviour
{
    public bool triggerEnter;
    public bool triggerExit;
    public bool playerIsColliding;

    private int frameCount;

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
        frameCount = 0;
        if(other.CompareTag("Player")) {
            triggerEnter = true;
            playerIsColliding = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        frameCount = 0;
        if(other.CompareTag("Player")) {
            triggerExit = true;
            playerIsColliding = false;
        }
    }

    void Update() {
        if(frameCount > 1) {
            triggerEnter = false;
            triggerExit = false;
        }

        frameCount += 1;
    }
}
