using UnityEngine;
using UnityEngine.InputSystem;

//Ensure that you have Stats and Game Manager script attached to your Player

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    Animator anim;
    Rigidbody rb;

    [Header("Movement")]
    private Vector2 moveInput;
    private Vector3 moveDirection;
    public bool canMove = true;

    [Header("Attack")]
    public float normalAttackCD;
    public float specialAttackCD;
    public int specialAttackCost;
    private bool canNormalAttack = true;
    private bool canSpecialAttack = true;

    [Header("Scripts")]
    Stats status;

    #region Unity Methods

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<Stats>();
    }

    private void Update()
    {
        #region Joystick Method

        if (!canMove || !status.isAlive || GameManager.Instance.isPaused) return;

        moveInput = Gamepad.current?.leftStick.ReadValue() ?? Vector2.zero;
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (moveDirection.magnitude > 0.1f)
        {
            rb.MovePosition(transform.position + moveDirection.normalized * status.currentSpeed * Time.deltaTime);
            transform.forward = moveDirection;
            anim.SetBool("isMoving", true);
        }

        else
        {
            anim.SetBool("isMoving", false);
        }

        #endregion
    }

    #endregion

    #region Attack Methods

    public void NormalAttack()
    {
        if (!canNormalAttack || !status.isAlive || GameManager.Instance.isPaused) return;
        
        canNormalAttack = false;

        //Animation
        anim.SetTrigger("normalAttack");

        //Audio

        //Attack Logic

        Invoke(nameof(ResetNormalAttack), normalAttackCD);
    }

    void ResetNormalAttack() => canNormalAttack = true;

    #endregion
}

