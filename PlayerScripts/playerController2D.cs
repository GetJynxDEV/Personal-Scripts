using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Fields and Properties

    [Header("COMPONENTS")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [Header("MOVEMENT")]
    //This determines the Player's move direction
    [SerializeField] private float horizontalInput;
    //This determines the Player's move speed
    [SerializeField] private float speed;

    [Header("JUMP")]
    //This determines the Player's jump force
    [SerializeField] private float jumpForce;
    //This determines the remaining jumps the Player can perform
    [SerializeField] private int remainingJumps;
    //This determines the maximum number of jumps the Player can perform
    [SerializeField] private int maxJumps;
    //This determines the cooldown time before the Player can jump again
    [SerializeField] private float jumpCooldown;
    //This determine if the Player can jump again
    [SerializeField] private bool canJump;
    //This determines if the Player is jumping
    [SerializeField] private bool isJumping;

    [Header("GROUND CHECK")]
    //This determines the distance from the ground to check if the Player is grounded
    [SerializeField] private Transform groundOrigin;
    //This determines the distance between the Player and the ground to check if the Player is grounded
    [SerializeField] private Vector2 playerGroundDistance;
    //This determines the layer mask for the ground
    [SerializeField] private LayerMask groundMask;
    //This checks if the Player is grounded
    [SerializeField] private bool isGrounded;

    #endregion

    #region Start

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Player Movements
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        //Ground Check
        GroundCheck();
    }

    #endregion

    #region Move Input

    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;

        if (isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }

        else
        {
            anim.SetBool("isGrounded", false);
        }
    }

    #endregion

    #region Jump Input

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            if (isGrounded && remainingJumps > 0)
            {
                StartCoroutine(jumpCoroutine());
            }
        }
    }

    private IEnumerator jumpCoroutine()
    {
        //Player will not be able to jump again
        canJump = false;
        //Player is jumping
        isJumping = true;
        //Decrease the remaining jumps
        remainingJumps--;

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        yield return new WaitForSeconds(.5f);
        //Player is no longer jumping
        isJumping = false;

        yield return new WaitForSeconds(jumpCooldown);

        //Player will be able to jump again
        canJump = true;
    }

    #endregion

    #region Raycast

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundOrigin.position, playerGroundDistance, 0, groundMask))
        {
            if (!isJumping)
            {
                isGrounded = true;
                anim.SetBool("isGrounded", true);

                remainingJumps = maxJumps;  
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundOrigin.position, Vector2.down * playerGroundDistance);
    }
    #endregion
}
