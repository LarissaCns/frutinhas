using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class playerDamage : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnPoint;

    [Header("Armas")]
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    [Range(0f, 100f)]
    private float throwForce;

    //teleguiada
    [SerializeField]
    private Transform target;
    private weaponScript weaponInfo;

    [Header("Vidas")]
    private int lives = 4;
    [SerializeField]
    public Image[] livesImage;
    public GameObject livesArray;

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (PlayerInput.GetPlayerByIndex(0).gameObject == gameObject)
        {
           spawnPoint = new Vector2(-4.06f, -2.978364f);
           livesArray = GameObject.Find("LivesP1");
           
            for (int i = 0; i < livesArray.transform.childCount; i++)
            {
                livesImage[i] = livesArray.transform.GetChild(i).GetComponent<Image>();
            }

            sprite.sprite = Resources.Load<Sprite>("bananaIDLE1");

        }
        else if (PlayerInput.GetPlayerByIndex(1).gameObject == gameObject)
        {
            livesArray = GameObject.Find("LivesP2");
            spawnPoint = new Vector2(4.06f, -2.978364f);

            for (int i = 0; i < livesArray.transform.childCount; i++)
            {
                livesImage[i] = livesArray.transform.GetChild(i).GetComponent<Image>();
            }

            sprite.sprite = Resources.Load<Sprite>("limaoIDLE1");
        }

        transform.position = spawnPoint;
    }

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
        if (value.performed && Time.timeScale == 1)
        {
            if (weaponInfo != null && weaponInfo.canPickWeapon && transform.childCount <= 1)
            {
                weapon = weaponInfo.gameObject;
                weapon.transform.GetComponent<Rigidbody2D>().isKinematic = true;
                weapon.transform.SetParent(gameObject.transform);
                if (weapon.transform.childCount > 0) {
                    Destroy(weapon.transform.GetChild(0).gameObject);
                }
                weapon.transform.localPosition = new Vector3(0.7f, 0f, weapon.transform.localPosition.z);
                weapon.GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (transform.childCount > 1)
            {
                Rigidbody2D wpRB;
                weapon = transform.GetChild(1).gameObject;
                wpRB = weapon.transform.GetComponent<Rigidbody2D>();
                weaponInfo.playerThrowed = gameObject;
                weapon.transform.parent = null;
                wpRB.isKinematic = false;
                weapon.GetComponent<BoxCollider2D>().enabled = true;

                wpRB.gravityScale = 0f;

                wpRB.velocity = Vector2.right * throwForce;

                StartCoroutine(Verify());
            }
        }
    }

    public void LivesUI()
    {
        livesImage[lives].enabled = false;
        lives--;

        if(lives <= 0)
        {
            endGame.player1win = PlayerInput.GetPlayerByIndex(1).gameObject == gameObject;
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("void"))
        {
            LivesUI();

            if (lives >= 0)
            {
                StartCoroutine(Respawn());
            }
        }
        if (collision.CompareTag("weapon"))
        {
            weaponInfo = collision.gameObject.GetComponent<weaponScript>();

            if (weaponInfo.playerThrowed != gameObject)
            {
                if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x != 0)
                {
                    LivesUI();
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(transform.childCount > 1)
        {
            Destroy(transform.GetChild(1).gameObject);
        }
        transform.position = spawnPoint;
    }

    IEnumerator Verify()
    {
        yield return new WaitForSeconds(0.1f);

        Collider2D[] weaponCollider = Physics2D.OverlapCircleAll((Vector2)transform.position, 0.3f);

        if (weaponCollider != null)
        {
            foreach (Collider2D collider in weaponCollider)
            {
                if (collider.gameObject.CompareTag("weapon"))
                {
                    weapon = collider.gameObject;
                    weapon.transform.GetComponent<Rigidbody2D>().isKinematic = true;
                    weapon.transform.SetParent(gameObject.transform);
                    if (weapon.transform.childCount > 0)
                    {
                        Destroy(weapon.transform.GetChild(0).gameObject);
                    }
                    weapon.transform.localPosition = new Vector3(0.7f, 0f, weapon.transform.localPosition.z);
                    weapon.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }
}
