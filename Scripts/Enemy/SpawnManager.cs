using Microlight.MicroBar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using YG;

//using Lean.Pool;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private GameObject enemySmallPrefab;
    [SerializeField] private GameObject enemyMediumPrefab;
    [SerializeField] private GameObject enemyLargePrefab;
    [SerializeField] private GameObject enemyBossPrefab;

    public float spawnerInterval = 2;
    private float tempInterval = 0;

    public int numberOfEnemies = 4;
    public int nowTheEnemies = 0;
    public int waveEnemys = 1;

    public int WasKilledEnemies = 0;

    private int addHealthSmall = 10;
    private int addHealthMedium = 10;
    private int addHealthLarge = 10;

    private int randPoint;

    public int levelCount = 0;

    public UnityEvent OnLevelComplete;

    private bool WasComplete = false;
    private bool IsBoss = false;
    private bool IsBossOnLevel = false;

    [SerializeField] private Transform[] BossPointsFly;
    [SerializeField] private Transform[] BossPointsJump;

    [SerializeField] private MicroBar _Microbar;

    private EnemyBoss _enemyBoss;

    private int BaseHealth = 15;
    private int TargetHealth = 1200;
    private double RandomFactor = 0.1;

    private void Start()
    {
        tempInterval = spawnerInterval;
        levelCount = SaveLoadData.LoadDataInt("levelCount", 0);
        IsBoss = System.Convert.ToBoolean(SaveLoadData.LoadDataInt("IsBoss", 0));
    }

    private void Update()
    {
        if (WasComplete)
            return;

        spawnerInterval -= Time.deltaTime;

        if (spawnerInterval <= 0)
        {
            if (WasKilledEnemies >= nowTheEnemies && nowTheEnemies > 0)
            {
                waveEnemys++;
                nowTheEnemies = 0;
                WasKilledEnemies = 0;

                if (waveEnemys >= 5)
                {
                    WasComplete = true;
                    OnLevelComplete.Invoke();
                    levelCount++;

                    if (levelCount > SaveLoadData.LoadDataInt("levelCount", 0))
                    {
                        YandexGame.NewLeaderboardScores("Leaderboard", levelCount);
                    }
                    
                    IsBoss = levelCount % 10 == 0;
                    SaveLoadData.SaveDataInt("IsBoss", System.Convert.ToInt32(IsBoss));
                    SaveLoadData.SaveDataInt("levelCount", levelCount);
                    return;
                }
            }

            if (IsBoss)
            {
                if (!IsBossOnLevel)
                {
                    SpawnBoss();
                }
                else if (_enemyBoss != null && _enemyBoss.NowHealth <= 0)
                {
                    CompleteBossLevel();
                }
            }
            else
            {
                if (nowTheEnemies == 0)
                {
                    ChooseSpawnEnemy();
                }
            }

            spawnerInterval = tempInterval;
        }
    }

    private void SpawnBoss()
    {
        randPoint = Random.Range(0, spawnPoint.Length);
        GameObject boss = Instantiate(enemyBossPrefab, spawnPoint[randPoint].transform.position, Quaternion.identity);
        _Microbar.transform.parent.gameObject.SetActive(true);

        _enemyBoss = boss.transform.GetChild(0).GetComponent<EnemyBoss>();
        _enemyBoss.spawnManager = this;

        _enemyBoss._MicroBar = _Microbar;
        _enemyBoss.MoveToPointsFly = BossPointsFly;
        _enemyBoss.MoveToPointsJump = BossPointsJump;

        IsBossOnLevel = true;
        nowTheEnemies++; // Считаем босса как врага
    }

    public void CompleteBossLevel()
    {
        _Microbar.transform.parent.gameObject.SetActive(false);
        IsBossOnLevel = false;
        OnLevelComplete.Invoke();
        levelCount++;

        if (levelCount > SaveLoadData.LoadDataInt("levelCount", 0))
        {
            YandexGame.NewLeaderboardScores("Leaderboard", levelCount);
        }
        
        StatsManager.Instance.AddHearts(Random.Range(3, 6));

        IsBoss = false;
        WasComplete = true;

        SaveLoadData.SaveDataInt("IsBoss", System.Convert.ToInt32(IsBoss));
    }

    private void ChooseSpawnEnemy()
    {
        double GrowthFactor = Mathf.Pow((float)TargetHealth / BaseHealth, 1.0f / 60f);
        double Health = BaseHealth * Mathf.Pow((float)GrowthFactor, levelCount - 1);
        double RandomizedHealth = Health * (1 + RandomFactor * (Random.Range(0, 2) * 2 - 1));

        addHealthLarge = (int)RandomizedHealth;
        addHealthMedium = (int)RandomizedHealth;
        addHealthSmall = (int)RandomizedHealth;

        int randChoice = Random.Range(0, 100);

        if (randChoice <= 50) // Small enemies (3-4)
        {
            int randCount = Random.Range(3, 5);
            for (int i = 0; i < randCount; i++)
            {
                SpawnEnemy(enemySmallPrefab, addHealthSmall);
            }
        }
        else if (randChoice > 50 && randChoice <= 80) // Medium enemies (1-2)
        {
            int rndEnemy = Random.Range(0, 2);
            if (rndEnemy == 0)
            {
                int randCount = Random.Range(1, 3);
                for (int i = 0; i < randCount; i++)
                {
                    SpawnEnemy(enemyMediumPrefab, addHealthMedium);
                }
            }
            else
            {
                int randCount = Random.Range(1, 4);
                
                SpawnEnemy(enemyMediumPrefab, addHealthMedium);

                for (int i = 0; i < randCount; i++)
                {
                    SpawnEnemy(enemySmallPrefab, addHealthSmall);
                }
            }
        }
        else if (randChoice > 80) // Large enemy (1)
        {
            SpawnEnemy(enemyLargePrefab, addHealthLarge);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, int health)
    {
        randPoint = Random.Range(0, spawnPoint.Length);
        var enemy = Instantiate(enemyPrefab, spawnPoint[randPoint].transform.position, Quaternion.identity);
        enemy.transform.GetChild(0).GetComponent<Enemy>().maxHealth += health;
        enemy.transform.GetChild(0).GetComponent<Enemy>().spawnManager = this;
        nowTheEnemies++; // Увеличиваем счётчик активных врагов

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Rigidbody2D rb = enemy.transform.GetChild(0).GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(randomDirection * 2f, ForceMode2D.Impulse);
        }
    }

    public void AddEnemy()
    {
        nowTheEnemies++; // Увеличиваем счётчик активных врагов
    }

    public void EnemyKilled()
    {
        WasKilledEnemies++;

        /*if (WasKilledEnemies >= nowTheEnemies) // Если все враги уничтожены
        {
            nowTheEnemies = 0; // Сброс счётчика активных врагов
        }*/
    }
}
