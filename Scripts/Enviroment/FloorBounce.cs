using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBounce : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            if (collision.collider.CompareTag("enemy") || collision.collider.CompareTag("enemyBoss"))
            {

                float angle = Random.Range(-10f, 10f);

                float angleInRadians = angle * Mathf.Deg2Rad;
                Vector2 Direction = new Vector2(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)).normalized;

                rb.AddForce(Direction * 10f, ForceMode2D.Impulse);
            }
        }
    }

}
