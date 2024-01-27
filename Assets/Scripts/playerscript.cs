using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerscript : MonoBehaviour
{
    [Header("Velocidade")]
    [SerializeField]
    [Range(0f,100f)]
    private float speed;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Pulo")]
    [SerializeField]
    [Range(0f, 100f)]
    private float jumpForce;
    [SerializeField]
    private Transform feet;
    [SerializeField]
    private Vector2 feetSize = new Vector2(0.5f,0.5f);
    [SerializeField]
    private LayerMask groundLayer;
    private int maxJumps = 2;
    private int jumpsRemaining = 0;

    private float coyoteTime = 0.2f;
    private float coyoteCounter = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext value){
        moveInput = value.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext value)
    {
        if ((jumpsRemaining > 0 || coyoteCounter> 0f) && value.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsRemaining--;
        }

        else if (value.canceled && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            jumpsRemaining--;
            coyoteCounter = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
    }

    private void Update()
    {
        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }

    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(feet.position, feetSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(feet.position, feetSize);
    }

}
