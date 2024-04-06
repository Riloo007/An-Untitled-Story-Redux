using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ContainToScreen : MonoBehaviour
{
    private CanvasScaler cs;

    // Start is called before the first frame update
    void Start()
    {
        cs = GetComponent<CanvasScaler>();
    }

    void Update()
    {
        if(Screen.width * 270 > Screen.height * 350) cs.matchWidthOrHeight = 1f;
        else cs.matchWidthOrHeight = 0f;
    }
}
