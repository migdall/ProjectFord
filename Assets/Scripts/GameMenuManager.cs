using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;
    private bool isPaused = false;

    [Header("First Selected Options")]
    [SerializeField]
    private GameObject mainMenuGameObject;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameInputManager.Instance.menuOpen)
        {
            if (isPaused)
            {
                isPaused = false;
                pauseMenu.SetActive(false);
                player.GetComponent<PlayerMovementController>().enabled = true;
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                player.GetComponent<PlayerMovementController>().enabled = false;
                EventSystem.current.SetSelectedGameObject(mainMenuGameObject);
            }
        }
    }

    public void LoadMainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }
}
