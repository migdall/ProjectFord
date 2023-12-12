using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    // Manages the player's interaction with this object in the scene
    public GameObject floatingCanvasPrefab;
    private GameObject floatingCanvasPrefabClone;
    private bool isInteracting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInteracting)
        {
            Debug.Log("Player entered the trigger");
            floatingCanvasPrefabClone = Instantiate(floatingCanvasPrefab, transform.position, Quaternion.identity);
            floatingCanvasPrefabClone.transform.SetParent(transform);
            isInteracting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger");
            Destroy(floatingCanvasPrefabClone);
            isInteracting = false;
        }
    }
}
