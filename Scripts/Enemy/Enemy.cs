//using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemySize enemySize;

    [SerializeField]
    private GameObject SmallEnemyPrefab;

    [SerializeField]
    public float maxHealth = 100f;
    private float nowHealth = 0f;

    [SerializeField]
    private TextMeshProUGUI EnemyHealthUI;

    [SerializeField]
    private GameObject EnemyUI;

    [SerializeField]
    private GameObject CoinPrefab;

    [SerializeField]
    private GameObject CrystallPrefab;

    [SerializeField]
    private GameObject Explosion;

    [SerializeField]
    private AudioClip DeadClip;

    [SerializeField]
    private GameObject Shield;

    [SerializeField]
    private GameObject Drone;

    [SerializeField]
    private float maxSpeed = 10f;

    private bool wasDied = false;

    public SpawnManager spawnManager;

    private Rigidbody2D rb;

    private void Start()
    {
        nowHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        transform.Rotate(0, 0, 0.5f);
        EnemyUI.transform.position = this.transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        rb.velocity = velocity;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        nowHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (nowHealth - damage > 0)
        {
            nowHealth -= damage;
            UpdateHealthUI();
        }
        else
        {
            if (!wasDied)
            {
                wasDied = true;
                StatsManager.Instance.AddScore(Random.Range(100, 200));

                if (enemySize != EnemySize.Small)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        GameObject smallEnemy = Instantiate(SmallEnemyPrefab, transform.position, transform.rotation);
                        smallEnemy.transform.GetChild(0).GetComponent<Enemy>().maxHealth += Mathf.Round(maxHealth - (maxHealth * 0.1f));

                        Vector2 randomDirection = Random.insideUnitCircle.normalized;
                        randomDirection.y = Mathf.Abs(randomDirection.y);
                        Rigidbody2D rbSmall = smallEnemy.transform.GetChild(0).GetComponent<Rigidbody2D>();
                        if (rbSmall != null)
                        {
                            rbSmall.AddForce(randomDirection * 10f, ForceMode2D.Impulse);
                        }

                        if (spawnManager != null)
                        {
                            smallEnemy.transform.GetChild(0).GetComponent<Enemy>().spawnManager = spawnManager;
                            
                            spawnManager.AddEnemy(); // Увеличиваем счётчик активных врагов
                        }
                    }
                }
                else
                {
                    int shieldChance = Random.Range(0, 100);
                    int droneChance = Random.Range(0, 100);
                    int crystallChance = Random.Range(0, 4);
                    if (shieldChance > 30 && shieldChance < 55)
                        Instantiate(Shield, transform.position, Quaternion.identity);
                    else if (droneChance > 65 && droneChance < 90)
                        Instantiate(Drone, transform.position, Quaternion.identity);
                    else if (crystallChance == 0)
                        Instantiate(CrystallPrefab, transform.position, Quaternion.identity);

                    Instantiate(CoinPrefab, transform.position, transform.rotation);
                }

                Instantiate(Explosion, transform.position, transform.rotation);
                SoundManager.instance.PlayShot(DeadClip);

                if (spawnManager != null)
                {
                    spawnManager.EnemyKilled(); // Увеличиваем счётчик убитых врагов
                }

                Destroy(this.transform.parent.gameObject);
            }
        }
    }

    private void UpdateHealthUI()
    {
        EnemyHealthUI.text = nowHealth.ToString();
    }
}

public enum EnemySize
{
    Large,
    Medium,
    Small
}
