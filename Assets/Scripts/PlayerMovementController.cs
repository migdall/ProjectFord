using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float walkSpeed = 3;
    [SerializeField]
    private float runSpeed = 10;
    private const float PLAYER_HEIGHT = 0f;

    [SerializeField]
    private Camera virtualCamera;

    private Animator animator;

    private PlayerInput controls;
    private InputAction moveAction;
    private InputAction runAction;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<PlayerInput>();
        moveAction = controls.actions.FindAction("Move");
        runAction = controls.actions.FindAction("Run");

        animator = GetComponent<Animator>();

        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player based on input
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // Create a Vector3 of the input ignoring the movement in the Y-axis
        Vector3 inputVector = new Vector3(moveInput.x, 0f, moveInput.y);

        float inputMagnitude = Mathf.Clamp01(inputVector.magnitude);
        float currentSpeed = inputMagnitude * speed;

        Vector3 getMoveDir = Quaternion.AngleAxis(virtualCamera.transform.rotation.eulerAngles.y, Vector3.up) * inputVector * Time.deltaTime;
        getMoveDir.Normalize();

        Vector3 velocity = getMoveDir * currentSpeed;

        if (getMoveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(getMoveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 5 * Time.deltaTime);
            transform.forward = Vector3.ProjectOnPlane(virtualCamera.transform.forward, Vector3.up);
        }

        // inputVector *= speed;
        // inputVector = transform.TransformDirection(inputVector);

        // Move the player via the character controller
        // Vector3 moveControllerVector = inputVector * Time.deltaTime;

        // Rotate the body of the player based on the direction the camera's
        // transform is facing
        // Vector3 movementDirection = Vector3.ProjectOnPlane(virtualCamera.transform.forward, Vector3.up);

        // Vector3 cameraDirection = virtualCamera.transform.forward;
        // cameraDirection.y = 0;

        // Vector3 relativePosition = transform.position - virtualCamera.transform.position;
        // Quaternion relativePositionNormalized = Quaternion.LookRotation(relativePosition.normalized);
        // Quaternion newRotation = new Quaternion(0, relativePositionNormalized.y, 0, relativePositionNormalized.w);

        Move(velocity);
        // LookAround(newRotation);
        HandleAnimation(moveInput);
    }

    private void HandleAnimation(Vector2 moveVector)
    {
        if (moveVector.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
            speed = walkSpeed;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (moveVector.magnitude >= 1.0f && runAction.ReadValue<float>() > 0)
        {
            animator.SetBool("isRunning", true);
            speed = runSpeed;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void LookAround(Quaternion newRotation)
    {
        // transform.rotation = newRotation;
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * 10);
        // transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
    }

    private void Move(Vector3 moveVector)
    {
        // transform.forward = moveDirection;
        controller.Move(moveVector * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, PLAYER_HEIGHT, transform.position.z);
    }
}
