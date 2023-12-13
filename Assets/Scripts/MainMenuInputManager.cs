using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuInputManager : MonoBehaviour
{
    [Header("First Selected Options")]
    [SerializeField]
    private GameObject mainMenuGameObject;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(mainMenuGameObject);
    }
}
