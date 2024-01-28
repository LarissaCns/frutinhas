using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    [Header("Velocidade")]
    [SerializeField]
    [Range(0f, 100f)]
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
    private Vector2 feetSize = new Vector2(0.5f, 0.5f);
    [SerializeField]
    private LayerMask groundLayer;
    private int maxJumps = 2;
    private int jumpsRemaining = 0;

    private float coyoteTime = 0.2f;
    private float coyoteCounter = 0f;

    public bool canDash = true; // posso dar o dash
    public bool isDashing = false; // dash ativo
    private float dashingPower = 24f; // força do dash
    private float dashingTime = 0.2f; // tempo do dash
    private float dashingCoolDown = 0.5f;

    [SerializeField] TrailRenderer tr;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr.emitting = false;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);

        if (rb.velocity.x > 0f)
        {
            transform.localScale = new Vector3(0.7f, transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < 0f)
        {
        }
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetButtonDown("Fire2") && canDash)
        {
            StartCoroutine(Dash());
        }

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale; // pegando o valor da gravidade 
        rb.gravityScale = 0f; // desliga a gravidade
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true; // liga o rastro
        yield return new WaitForSeconds(dashingTime); // espera um tempo
        tr.emitting = false; // desliga o rastro
        rb.gravityScale = originalGravity; // liga a gravidade novamente
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown); // tempo do dash
        canDash = true;
    }
    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(feet.position, feetSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            return true;
        }

        return false;
    }

    public void Move(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext value)
    {
        if ((jumpsRemaining > 0 || coyoteCounter > 0f) && value.performed)
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

    /* private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(feet.position, feetSize);
    } */
}