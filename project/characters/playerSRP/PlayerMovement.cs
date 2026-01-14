using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput), typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;

    [Header("Run")]
    public bool canRun = true;

    [Header("Jump")]
    public bool canJump = true;
    public float jumpPower = 7f;
    public float gravity = 10f;

    [Header("Crouch")]
    public bool canCrouch = true;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeedMultiplier = 0.5f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private PlayerInput playerInput;
    private PlayerStats stats;

    private float currentWalkSpeed;
    private float currentRunSpeed;
    private bool isCrouching = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        stats = GetComponent<PlayerStats>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateSpeeds();
        HandleMovement();
        HandleCrouch();
    }

    private void UpdateSpeeds()
    {
        if (isCrouching)
        {
            currentWalkSpeed = stats._speed * crouchSpeedMultiplier;
            currentRunSpeed = stats._speed * crouchSpeedMultiplier;
        }
        else
        {
            currentWalkSpeed = stats._speed;
            currentRunSpeed = stats._speed * 2;
        }
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float currentSpeed = CalculateCurrentSpeed();

        float curSpeedX = canMove ? currentSpeed * playerInput._moveInput.y : 0f;
        float curSpeedY = canMove ? currentSpeed * playerInput._moveInput.x : 0f;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private float CalculateCurrentSpeed()
    {
        if (!canRun)
        {
            return currentWalkSpeed;
        }

        return playerInput._isRunning ? currentRunSpeed : currentWalkSpeed;
    }

    private void HandleCrouch()
    {
        if (!canCrouch) return;

        if (playerInput._isCrouching && canMove)
        {
            if (!isCrouching)
            {
                isCrouching = true;
                characterController.height = crouchHeight;
            }
        }
        else
        {
            if (isCrouching)
            {
                isCrouching = false;
                characterController.height = defaultHeight;
            }
        }
    }
}
