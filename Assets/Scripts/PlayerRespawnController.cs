using UnityEngine;

public class PlayerRespawnController : MonoBehaviour
{
    // The position the player will respawn at
    [SerializeField]
    private Vector3 respawnPosition;
    [SerializeField]
    private float respawnHeight = 0f;
    [SerializeField]
    private Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.y < respawnHeight)
        {
            Debug.Log("Player fell off the map, respawning...");
            CharacterController controller = playerTransform.GetComponent<CharacterController>();
            controller.enabled = false;
            playerTransform.position = respawnPosition;
            controller.enabled = true;
        }
    }
}
