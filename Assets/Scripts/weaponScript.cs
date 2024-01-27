using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
