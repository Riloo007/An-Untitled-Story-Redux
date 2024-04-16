using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosedAreaController : MonoBehaviour
{
    public EnclosedRoom[] areas;
    public SpriteColor background;
    public SpriteColor foreground;

    void Update()
    {
        foreach (var area in areas)
        {
            if(area.collider.triggerEnter) {
                background.color = area.backgroundColorUnfocused;
                foreground.color = Color.clear;
                area.roomSprite.color = Color.white;
                area.altForegroundSprite.color = area.backgroundColorUnfocused;
            }
            if(area.collider.triggerExit) {
                background.color = Color.white;
                foreground.color = Color.white;
                // area.roomSprite.color = Color.clear;
                area.altForegroundSprite.color = Color.white;
            }
        }
    }
}

public class EnclosedArea
{
    public ExternalCollider collider;
    public SpriteColor roomSprite;
    public SpriteColor altForegroundSprite;
    public Color backgroundColorUnfocused;
}