using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController04 : MonoBehaviour
{
    public ExternalCollider room1;
    public ExternalCollider room2;
    public SpriteColor fg1;
    public SpriteColor fg2;

    public Color bkg = new(118 / 100, 119 / 100, 127 / 100);
    private Color transparent = new(1,1,1,0);

    void Update()
    {
        if(room2.triggerEnter) {
            // Show Room 1
            fg2.color = bkg;
            fg1.color = transparent;
        }
        if(room1.triggerEnter) {
            // Show Room 2
            fg1.color = bkg;
            fg2.color = transparent;
        }
        if(room2.triggerExit) {
            // Show Room 1
            fg2.color = Color.white;
            fg1.color = Color.white;
        }
        if(room1.triggerExit) {
            // Show Room 2
            fg2.color = Color.white;
            fg1.color = Color.white;
        }
    }
}
