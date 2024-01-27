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
    // private int maxJumps = 2;
    // private int jumpsRemaining = 0;

    /* private float coyoteTime = 0.2f; */
    // private float coyoteCounter = 0f;

    // Tentando o dash
    private bool canDash = true; // posso dar o dash
    private bool isDashing; // dash ativo
    private float dashingPower = 24f; // força do dash
    private float dashingTime = 0.2f; // tempo do dash
    private float dashingCoolDown = 0.5f;

    [SerializeField] TrailRenderer tr;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(isDashing) {
            return;
        }

        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);

        if (rb.velocity.x > 0f)
        {
            transform.localScale = new Vector3(0.7f, transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-0.7f, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Update()
    {
        if(isDashing) {
            return;
        }

        if(Input.GetButtonDown("Fire2") && canDash) {
            StartCoroutine(Dash());
        }
    }

    public void Move(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
    }

    IEnumerator Dash() {
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
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(feet.position, feetSize);
    }*/
}
