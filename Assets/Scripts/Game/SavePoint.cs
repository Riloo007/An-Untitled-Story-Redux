using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public SaveManager saveManager;
    public Transform playerTransform;
    public float allowedDistance;
    public Animator animator;
    private Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // animator.SetBool(0, false);
        float distance = Vector3.Distance(playerTransform.position, thisTransform.position);

        if(Input.GetKeyDown(KeyCode.DownArrow) && distance < allowedDistance) {
            // Save
            Debug.Log("Save Instigated");
            animator.SetTrigger("Save");
            saveManager.SaveGame();
        }
    }
}
