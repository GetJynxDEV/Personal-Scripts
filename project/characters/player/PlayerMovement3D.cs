using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("COMPONENTS")]

    //Attach the camera to the player as a Child Object
    [Header("Camera")]
    public Camera playerCamera;

    public float lookSpeed = 2f;
    public float lookXLimitDown = 90f;
    public float lookXLimitUp = -70f;

    private float verticalVelocity = 0f;

    //state = 1 means walking, 2 means running, 3 means jumping, 4 means attacking
    [SerializeField] private Animator anim;

    [SerializeField] private CharacterController characterController;

    [Header("Movement")]

    public bool isMoving;
    public bool canMove = true;

    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float curSpeedX;
    [SerializeField] private float curSpeedY;

    [Header("Walking")]
    public bool canWalk = true;
    public bool isWalking = false;
    public float walkSpeed = 6;

    [Header("Run")]
    public bool canRun = true;
    public bool isRunning = false;
    public float runSpeed = 12;

    [Header("Jump")]
    public bool canJump = true;
    public float jumpPower = 7f;

    public float gravity = 10f;
    private float velocity;

    [Header("Croutch")]
    public bool canCroutch = true;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    private float defaultWalkSpeed;
    private float defaultRunSpeed;

    [Header("Shooting")]

    public bool canShoot = false;
    public bool isShooting = false;
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Audio")]
    [SerializeField] private AudioSource gunAudioSource;
    [SerializeField] private AudioClip[] gunClip;

    [Header("Scripts")]
    [SerializeField] private Stats status;
    [SerializeField] private GameManager manager;

    #region Unity Methods

    void Start()
    {
        SetDefault();

        anim = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        status = GetComponent<Stats>();

        //Lock Cursor to center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        toggleCursor();

        //MOVEMENT

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 inputDirection = (forward * moveInput.y) + (right * moveInput.x);

        float speed = isRunning ? runSpeed : walkSpeed;
        moveDirection = inputDirection.normalized * speed;
        moveDirection.y = verticalVelocity;

        //Apply Gravity
        if (characterController.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        //ANIMATION
        animationUpdate();

        #region Camera and Movement

        if (canMove && status.isAlive)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, lookXLimitUp, lookXLimitDown);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion
    }

    #endregion

    #region MOVEMENT

    public void movement(InputAction.CallbackContext context)
    {
        if (status.isAlive)
        {
            if (context.performed && !isShooting)
            {
                //Read the input value as a Vector2 
                moveInput = context.ReadValue<Vector2>();

                isMoving = true;
            }

            else if (context.canceled)
            {
                moveInput = Vector2.zero;
                isMoving = false;
            }
        }
    }

    public void running(InputAction.CallbackContext context)
    {
        if (status.isAlive)
        {
            if (context.performed && canRun && !isShooting)
            {
                isRunning = true;
                isWalking = false;
                //Set the current speed to run speed
                currentSpeed = runSpeed;
            }
            else if (context.canceled)
            {
                isRunning = false;
                isWalking = true;

                //Set the current speed to walk speed
                currentSpeed = walkSpeed;
            }
        }

    }

    public void jump(InputAction.CallbackContext context)
    {
        if (status.isAlive)
        {
            if (context.performed && canJump && characterController.isGrounded)
            {
                verticalVelocity = jumpPower;
            }
        }

    }

    #endregion

    #region ATTACK

    public void Attack(InputAction.CallbackContext context)
    {
        if (status.isAlive && !manager.isPaused)
        {
            
        }
    }

    #endregion

    #region PAUSE

    public void pause(InputAction.CallbackContext context)
    {
        //If User oress the pause button, toggle the pause state
        if (status.isAlive)
        {
            if (context.performed)
            {
                manager.isPaused = !manager.isPaused;
            }
        }
    }

    void toggleCursor()
    {
        canMove = !manager.isPaused;

        if (manager.isPaused || !status.isAlive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!manager.isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    #endregion

    #region ANIMATION

    private void animationUpdate()
    {
        bool hasInput = moveInput.magnitude > 0.1f;
    }

    #endregion

    void SetDefault()
    {
        defaultWalkSpeed = walkSpeed;
        defaultRunSpeed = runSpeed;
    }

    void SetSpeedToDefault()
    {
        walkSpeed = defaultWalkSpeed;
        runSpeed = defaultRunSpeed;
    }
}

