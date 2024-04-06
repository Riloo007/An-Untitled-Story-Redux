using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Initiate() {
        // TODO Confirm quit, save state, etc

        Debug.Log("Quitting Application");
        Application.Quit();
    }
}
