# An Untitled Story Redux
Welcome! Listed below are methods and practices used to keep this project somewhat organized and consistent.



## Folder Setup
```
📁 Assets
    📁 Scripts
        📄 General Scripts ...
        📁 Events ( General events such as saving a game, loading a game, quitting, etc. )
        📁 Game ( Ingame scripts such as interactable objects and bosses. )
        📁 UI ( Anything directly UI related; showing or navigating menus, etc. )
        📁 UnityEditor ( Anything used ONLY on the development side / in the editor )
    📁 Scenes
        📄 Scene Prefabs ...
        📁 Scene Name ...
            📄 (Scene Name).psd
            📄 Other Scene Source Files ...
```


## Screen and Scene Setup

- Each scene is 350 x 270 pixels.
- Each scene is defined in a prefab with a unique name, typically a number.
- Each scene typically comprises of the base following:
    - Foreground (Stored in [Scenes/(sceneName)/FG.png]): Renders above the player
    - Background (Stored in [Scenes/(sceneName)/BG.png]): Renders below the player
    - A Collider, placed on the Ground Layer
    - A [SceneColor.cs] on the root node, which just changes the color of the extra screen space for individual scenes.
- Scenes are all placed in one single Unity Scene, "Multiscene.unity"
- The prefabs can easily be snapped into place in the multiscene by checking the "Snap to Scene Grid" option in the transform controls.
- At playtime, these scenes are loaded and unloaded automatically by [SceneManager.cs] to save computing resources.
- Due to the single-pass nature of URP, to achieve background blur and possible extra effects, the scene is set up with three cameras:
    - CAMERA 1: Renders the game, player, objects, etc to a 350x270px Render Texture.
    - CAMERA 2: Placed out of the way and independantly of the scene, renders another 350x270px Render Texture of a Raw Image. The Raw image has a material applied that does a horizontal blur when the game is paused.
    - CAMERA 3: Placed out of the way of both Cameras 1 and 2, it renders to the screen. In front of this camera is placed the Raw Image from Camera 2, this time with a vertical blur when the game is paused. The UI is also rendered by this camera.



## Player Setup

- The player is stored in its own prefab.
- The player size is about 13x13 pixels.
- Due to the complicated nature of the game adding so many abilities, the player controller should be modularized somehow. Still a work in progress.



## UI Setup

- [UIController.cs] is used to control which menu is shown. Using `ShowMenu(int index)`, the menu with a specific index is transitioned to. Doing this disables all other menus so there's no worry about the menus rendering on top of each other.
- Each menu is defined using [UIListController].
- Each menu is assigned an index in the inspector under the UIController. This index should not be changed as it becomes the hardcoded index used by various scripts to navigate to and show the correct menus.



## Save Setup

- TODO
- Controlled by [SaveManager.cs]
