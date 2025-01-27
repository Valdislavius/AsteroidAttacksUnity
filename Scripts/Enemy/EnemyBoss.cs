using Microlight.MicroBar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public BossState _BossState;
    
    public float MaxHealth = 50000;
    public float NowHealth = 0;

    public Transform [] MoveToPointsFly;
    public Transform[] MoveToPointsJump;

    private Vector3 Target;

    private float distanceToTarget = 0f;

    private float BossStateTimer = 10f;

    public MicroBar _MicroBar;

    //private Rigidbody2D rb;

    private float SpawnEnemyTimer = 0f;
    [SerializeField] GameObject MiniBoss;

    public SpawnManager spawnManager;

    //private void OnEnable()
    //{

    //}

    void Start()
    {
        NowHealth = MaxHealth;
        Target = MoveToPointsFly[Random.Range(0, MoveToPointsFly.Length)].position;
        //rb = GetComponent<Rigidbody2D>();

        _MicroBar.Initialize(MaxHealth);
        _MicroBar.SetNewMaxHP(MaxHealth);
        _MicroBar.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = MaxHealth.ToString();
    }
    void Update()
    {
        BossStateTimer -= Time.deltaTime;

        if(BossStateTimer <= 0)
        {
            if(_BossState == BossState.Jump)
            {
                _BossState = BossState.Fly;
               // rb.bodyType = RigidbodyType2D.Kinematic;

               // rb.gravityScale = 0f;
            }
            else
            {
                _BossState = BossState.Jump;
               // rb.bodyType = RigidbodyType2D.Kinematic;

                //rb.gravityScale = 0f;
            }

            BossStateTimer = 10f;
        }



        if(_BossState == BossState.Jump)
        {
            distanceToTarget = Vector2.Distance(transform.position, Target);

            if (distanceToTarget > 0.2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target, Time.deltaTime * 4f);
            }
            else
            {
                if(Target == MoveToPointsJump[0].position)
                {
                    Target = MoveToPointsJump [Random.Range(1,3)].position;
                }
                else
                {
                    Target = MoveToPointsJump[0].position;
                }
            }
        }
        else if(_BossState == BossState.Fly)
        {
            distanceToTarget = Vector2.Distance(transform.position, Target);

            if(distanceToTarget > 0.2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target, Time.deltaTime * 4f);
            }
            else
            {
                Target = MoveToPointsFly[Random.Range(0, MoveToPointsFly.Length)].position;
            }

            SpawnEnemyTimer += Time.deltaTime;
            if(SpawnEnemyTimer >= 2f)
            {
                for(int i = 0; i < 2; i++)
                {
                    GameObject enemy = Instantiate(MiniBoss, transform.position, Quaternion.identity);

                    Vector2 RandomDirection = Random.insideUnitCircle.normalized;
                    //RandomDirection.y = Mathf.Abs(RandomDirection.y);
                    Rigidbody2D rb = enemy.transform.GetChild(0).GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.AddForce(RandomDirection * 2f, ForceMode2D.Impulse);
                    }
                }

                SpawnEnemyTimer = 0f;   
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if(NowHealth - damage > 0)
        {
            NowHealth -= damage;

            _MicroBar.UpdateBar(NowHealth);

            _MicroBar.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = NowHealth.ToString();
        }
        else
        {
            NowHealth = 0f;
            _MicroBar.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = NowHealth.ToString();
            
            spawnManager.CompleteBossLevel();

            Destroy(this.gameObject);
        }
    }


    public enum BossState
    {
        Jump,
        Fly
    }
}
