using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public bool guided = false;
    public bool canPickWeapon = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Collider2D[] weaponCollider = Physics2D.OverlapCircleAll((Vector2)transform.position, 0.3f);
        int var = 0;

        if(weaponCollider != null)
        {
            foreach (Collider2D collider in weaponCollider)
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    var++;
                }
            }
        }

        if (var > 0)
        {
            canPickWeapon = true;
        }
        else
        {
            canPickWeapon = false;
        }

    }

    private void Update()
    {
        if(transform.localPosition.x > 10f || transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.velocity = new Vector2(0f, 0f);
            rb.isKinematic = true;
        }
    }
}
