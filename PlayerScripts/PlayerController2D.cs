using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    [Header("PLAYER COMPONENTS")]

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;
    
    [Header("MOVEMENT PROPERTIES")]

    //This will determine if player goes left or right
    [SerializeField] private Vector2 moveInput;
    //This will determine how fast the player moves
    [SerializeField] private float moveSpeed;


    #region START METHOD
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetInteger("movement", 0);
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    #endregion

    #region MOVE INPUT

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        anim.SetInteger("movement", 1);
    }

    #endregion
}
