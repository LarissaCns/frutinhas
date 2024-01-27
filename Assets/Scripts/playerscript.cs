using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerscript : MonoBehaviour
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

    //armas
    [SerializeField]
    private bool canPickWeapon = false;
    [SerializeField]
    private GameObject weapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
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

    public void Weapon(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (canPickWeapon)
            {
                weapon.transform.SetParent(gameObject.transform);
                weapon.transform.localPosition = new Vector3(0.7f, 0f, weapon.transform.localPosition.z);
                if (weapon.GetComponent<BoxCollider2D>().isTrigger)
                {
                    weapon.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else if (transform.childCount > 1)
            {

                transform.GetChild(1).transform.parent = null;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon"))
        {
            canPickWeapon = true;
            weapon = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon"))
        {
            canPickWeapon = false;
            weapon = null;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(feet.position, feetSize);
    }*/

}
