using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("bullet") && !collision.CompareTag("Player") && !collision.CompareTag("shield") && !collision.CompareTag("coin"))
        {
            if (collision.CompareTag("enemy") || collision.CompareTag("enemyBoss"))
            {
                if (collision.GetComponent<Enemy>())
                {
                    collision.GetComponent<Enemy>().TakeDamage(damage);
                }
                else if (collision.GetComponent<EnemyBoss>())
                {
                    collision.GetComponent<EnemyBoss>().TakeDamage(damage);
                }


            }

            Destroy(this.gameObject);
        }


    }
}
