using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColor : MonoBehaviour
{
    [Tooltip("Duration of the transition in milliseconds")]
    public float transitionDuration = 300;
    public Color color = Color.white;
    public Color colorTarget;
    public Color previousColor;
    public float timer;
    public SpriteRenderer sr;

    void Start() {
        previousColor = colorTarget = color;
        transitionDuration /= 1000;

        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(color != colorTarget) {
            timer = 0;
            colorTarget = color;
            previousColor = sr.color;
        }

        timer += Time.deltaTime;
        sr.color = Color.Lerp(previousColor, colorTarget, timer / transitionDuration);
    }
}
