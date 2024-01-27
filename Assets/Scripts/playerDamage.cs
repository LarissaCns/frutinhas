using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class playerDamage : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnPoint;

    [Header("Armas")]
    [SerializeField]
    private bool canPickWeapon = false;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    [Range(0f, 100f)]
    private float throwForce;

    [Header("Vidas")]
    private int lives = 4;
    [SerializeField]
    private Image[] livesImage;

    private void Update()
    {
        if(transform.localScale.x > 0)
        {
            throwForce = Mathf.Abs(throwForce);
        }
        else if (transform.localScale.x < 0)
        {
            throwForce = Mathf.Abs(throwForce) * (-1);
        }

        if (!livesImage[0].enabled)
        {
            Destroy(gameObject);
        }
    }

    public void Weapon(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (canPickWeapon)
            {
                weapon.transform.GetComponent<Rigidbody2D>().isKinematic = true;
                weapon.transform.SetParent(gameObject.transform);
                if (weapon.transform.childCount > 0) {
                    Destroy(weapon.transform.GetChild(0).gameObject);
                }
                weapon.transform.localPosition = new Vector3(0.7f, 0f, weapon.transform.localPosition.z);
                if (weapon.GetComponent<BoxCollider2D>().isTrigger)
                {
                    weapon.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else if (transform.childCount > 1)
            {
                Rigidbody2D wpRB;
                weapon = transform.GetChild(1).gameObject;
                wpRB = weapon.transform.GetComponent<Rigidbody2D>();
                weapon.transform.parent = null;
                wpRB.isKinematic = false;
                if (weapon.GetComponent<BoxCollider2D>().isTrigger)
                {
                    weapon.GetComponent<BoxCollider2D>().enabled = true;
                }
                wpRB.gravityScale = 0f;
                wpRB.velocity = new Vector2(throwForce, wpRB.velocity.y);
            }
        }
    }

    public void LivesUI()
    {
        livesImage[lives].enabled = false;
        lives--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon"))
        {
            canPickWeapon = true;
            weapon = collision.gameObject;
        }
        if (collision.CompareTag("void"))
        {
            LivesUI();

            if (lives >= 0)
            {
                StartCoroutine(Respawn());
            }
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

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        if(transform.childCount > 1)
        {
            Destroy(transform.GetChild(1).gameObject);
        }
        transform.position = spawnPoint;
    }
}