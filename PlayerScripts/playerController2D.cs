using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//REMINDER
//THIS SCRIPT USES THE INPUT SYSTEM FROM THE PACKAGE MANAGER

public class playerController2D : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;

    [Header("MOVEMENT SPEED")]
    //moveSpeed Input determine how fast the player can Run
    [SerializeField] private float moveSpeed;
    //jumpForce Input determine how high the player can Jump
    [SerializeField] private float jumpForce;
    //doubleJumpForce Input determine how hight the player can Double Jump
    [SerializeField] private float doubleJumpForce;
    //dashSpeed Input determines how long the player can Dash
    [SerializeField] private float dashSpeed;

    [Header("MOVEMENT COOLDOWN")]
    [SerializeField] private float jumpCD;
    [SerializeField] private float dashCD;

    [Header("MOVEMENT VARIABLE")]
    //Remaining Jump of Player
    [SerializeField] private int jumpRemaining;
    //Maximum Jump of Player
    [SerializeField] private int maxJump = 2;
    //Remaining Dash of Player
    [SerializeField] private int dashRemaining;
    //Maximum Dash of Player
    [SerializeField] private int maxDash = 1;
    [Header("MOVEMENT BOOLEAN")]
    //Check if Player is Running
    [SerializeField] private bool isRunning;
    //Check if Player is Jumping
    [SerializeField] private bool isJumping;
    //Check where the Player is facing
    [SerializeField] private bool isFacingRight;

    [Header("DIRECTION")]
    //horizontalMovements will determine where the player goes
    [SerializeField] private float horizontalMovements;

    [Header("GROUND CHECK")]
    //Determines the Position of the Ground
    [SerializeField] private Transform playerGroundDetect;
    //Distance between Player and Ground
    [SerializeField] private Vector2 playerGroundDistance = new Vector2(0.5f, 0.5f);
    //LayerMask of Ground
    [SerializeField] private LayerMask groundMask;
    //Check if Player is on the Ground
    [SerializeField] private bool isGrounded;

    #region START FUNCTION

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //Determines where the Player Horizontal Movement Output is
        rb.velocity = new Vector2(horizontalMovements * moveSpeed, rb.velocity.y);
    }

    #endregion

    #region INPUT SYSTEM

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovements = context.ReadValue<Vector2>().x;

        //FLIP CHARACTER
        if (horizontalMovements < 0)
        {
            sr.flipX = true;
        }

        else if (horizontalMovements > 0)
        {
            sr.flipX = false;
        }

        //BOOLEAN

        //ANIMATION
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && jumpRemaining > 1)
        {
            //Deduct 1 to Player's remaining Jump
            jumpRemaining--;

            //Start Player's Jump Function
            StartCoroutine(jumpAnim());
        }

        if (context.performed && isGrounded && isJumping)
        {
            //If Player Jump deduct 1 to Player's remaining Jump
            jumpRemaining--;

            //Start Player's Jump Function
            StartCoroutine(doubleJumpAnim());
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && dashRemaining == 1)
        {
            //Deduct 1 to Player's remaining Dash
            dashRemaining--;

            //Start Player's Dash Function
            StartCoroutine(dashAnim());
        }
    }

    #endregion

    #region MOVEMENT ANIMATION
    private IEnumerator jumpAnim()
    {
        //Set to true when Function Start
        isJumping = true;

        //Jump Function
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        //ANIMATION

        yield return new WaitForSeconds(jumpCD);
    }

    private IEnumerator doubleJumpAnim()
    {
        //Double Jump Function
        rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);

        yield return new WaitForSeconds(jumpCD);
    }

    private IEnumerator dashAnim()
    {
        //Check where the Player is facing
        float dashDirection = isFacingRight ? 1f : -1f;

        //Dash Function
        rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y);

        yield return new WaitForSeconds(dashCD);

        dashRemaining = maxDash;
    }

    #endregion

    #region RAYCAST

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //For Ground Check
        Gizmos.DrawRay(playerGroundDetect.position, Vector2.down * playerGroundDistance);
    }

    private void groundCheck()
    {
        if (Physics2D.OverlapBox(playerGroundDetect.position, playerGroundDistance, 0, groundMask))
        {
            if (!isJumping)
            {
                isGrounded = true;

                jumpRemaining = maxJump;
            }
        }
    }

    #endregion
}
