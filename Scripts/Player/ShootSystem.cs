using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootSystem : MonoBehaviour
{
    public int damage = 40;

    [SerializeField] private Transform[] SpawnPoints;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject explosion;

    private float timer = 1f;
    public float Cooldown = 0.5f;

    private int Herolevel;
    private int Guns;

    private float bulletSize = 0.15f;

    [SerializeField] private GameObject shieldPlatform;

    [SerializeField] private GameObject Drone;

    [SerializeField] private bool CanDie = false;
    void Update()
    {
        Herolevel = GetComponent<LevelUp>().NowLevel;

        timer += Time.deltaTime;
        if (timer > Cooldown && Input.GetMouseButton(0))
        {
            Fire();
            timer = 0f;
        }
    }

    void Fire()
    {
        if (Herolevel < 5)
        {
            Guns = 1;
            bulletSize = 0.15f;
        }
        else if (Herolevel >= 5 && Herolevel < 10)
        {
            Guns = 3;
            bulletSize = 0.2f;
        }
        else if (Herolevel >= 10 && Herolevel < 15)
        {
            Guns = 5;
            bulletSize = 0.25f;
        }
        else if (Herolevel >= 15)
        {
            Guns = 7;
            bulletSize = 0.30f;
        }
        for (int i = 0; i < Guns; i++)
        {
            GameObject bullet = Instantiate(BulletPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            bullet.transform.localScale = new Vector2(bulletSize, bulletSize);

            bullet.GetComponent<BulletSystem>().damage = damage;


            bullet.GetComponent<Rigidbody2D>().velocity = transform.up * 20f;

            Destroy(bullet, 4f);

        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanDie)
        {
            if (collision.CompareTag("enemy") || collision.CompareTag("enemyBoss"))
            {
                GameObject Dead = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(Dead, 2);

                StatsManager.Instance.AddCoins(StatsManager.Instance.GetCoinsSession());
                StatsManager.Instance.AddCrystalls(StatsManager.Instance.GetCrystallSession());

                Time.timeScale = 0.4f;

                UIManager.Instance.ActiveDeadPanel();
                this.gameObject.SetActive(false);
            }
        }

    }

    public void RespawnOnLevel()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(true);
        StartCoroutine("Immortality");
    }

    IEnumerator Immortality()
    {
        CanDie = false;
        yield return new WaitForSeconds(3f);
        CanDie = true;
    }

    public void ActivePlatform()
    {
        StartCoroutine("PlatformDelay");
    }

    IEnumerator PlatformDelay()
    {
        shieldPlatform.SetActive(true);
        yield return new WaitForSeconds(10f);
        shieldPlatform.SetActive(false);
    }


    public void ActiveDrone()
    {
        StartCoroutine("DroneDelay");
    }

    IEnumerator DroneDelay()
    {
        Drone.SetActive(true);
        yield return new WaitForSeconds(10f);
        Drone.SetActive(false);
    }
}
