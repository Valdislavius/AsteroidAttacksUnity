using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinion : MonoBehaviour
{

    public GameObject Target;
    [SerializeField] private float speed = 5f;
    private float minDistance = 1f;

    private Vector2 initialPosition;

    [SerializeField] GameObject BulletPrefab;
    [SerializeField] private int damage = 10;

    [SerializeField] private Transform SpawnPoint;

    private float bulletSize = 0.05f;

    private float timer = 1f;
    public float Cooldown = 0.5f;
    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Cooldown)
        {
            Fire();
            timer = 0f;
        }

        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if(distance < minDistance)
        {
            transform.position = new Vector2(transform.position.x, initialPosition.y);
            return;
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, Target.transform.position, Time.deltaTime * speed);
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(BulletPrefab, SpawnPoint.position, SpawnPoint.rotation);
        bullet.transform.localScale = new Vector2(bulletSize, bulletSize);

        bullet.GetComponent<BulletSystem>().damage = damage;


        bullet.GetComponent<Rigidbody2D>().velocity = transform.up * 20f;

        Destroy(bullet, 4f);
    }
}
