using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{  
    //Reminder that this Code is using InputManager as it's movement
    //Attach Player Input to your Player and change the Behavior to "Invoke Unity Events"

    [Header("PLAYER COMPONENTS")]

    //Attach all Component to this script

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform playerTransform;

    [Header("MOVEMENT PROPERTIES")]

    //This will determine the Player's Movement

    [SerializeField] private Vector2 moveInput; //This will determine if player goes left or right
    private Vector2 lastMoveInput;
    [SerializeField] private float moveSpeed; //This will determine how fast the player moves

    #region START METHOD
    private void Awake()
    {
        //Will automatically attach all component if found in the Player Component
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        playerTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        //Reset all value

        resetValue();
    }

    private void Update()
    {
        Animate();

        //Rotate Player when it goes Left || Right
        if (moveInput.x == -1)
        {
            playerTransform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else if (moveInput.x == 1)
        {
            playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        lastMoveInput = moveInput;
    }

    #endregion

    #region RESET VALUES

    public void resetValue()
    {
        //Reset all the value here
        moveInput = Vector2.zero;
        lastMoveInput = Vector2.zero;
    }

    #endregion

    #region MOVE INPUT

    public void Move(InputAction.CallbackContext context)
    {
        //If true this will prevent the player from moving
        //if (pm.isMovementLocked) return;

        moveInput = context.ReadValue<Vector2>();
    }

    #endregion

    #region ANIMATE

    public void Animate()
    {
        //XY Animation Movement
        anim.SetFloat("MoveX", moveInput.x);
        anim.SetFloat("MoveY", moveInput.y);
        anim.SetFloat("MoveMagnitude", moveInput.magnitude);
        anim.SetFloat("LastMoveX", lastMoveInput.x);
        anim.SetFloat("LastMoveY", lastMoveInput.y);

    }

    #endregion

}
