using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using YG;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    
    [SerializeField]
    private TextMeshProUGUI ScoreText;
    [SerializeField]
    private Text CoinsText;
    [SerializeField]
    private Text CrystallsText;
    [SerializeField]
    private Text RewardText;
    [SerializeField]
    private Text HeartsText;
    [SerializeField]
    private Text HeartsSessionText;

    [SerializeField] private Text CoinsCountAllText;
    [SerializeField] private Text CrystallCountAllText;

    [SerializeField]
    private GameObject DeadPanel;

    private StatsManager statsManager;
    [SerializeField] 
    private ShootSystem _ShootSystem;

    private void Awake()
    {
        Instance = this;

        statsManager = StatsManager.Instance;
    }
    public void UpdateTextUI(int Score, int CoinsCounter, int Crysrtalls)
    {
        if (YandexGame.lang == "ru")
        {
            ScoreText.text = $"Очки: {Score}";
        }
        else if (YandexGame.lang == "tr")
        {
            ScoreText.text = $"Oyundaki: {Score}";
        }
        else
        {
            ScoreText.text = $"Points: {Score}";
        }
        CoinsText.text = $"{NumberFormater.FormatNumber(CoinsCounter)}";
        CrystallsText.text = $"{NumberFormater.FormatNumber(Crysrtalls)}";
    }

    public void UpdateHeartsUi(int Hearts)
    {
        HeartsText.text = $"{NumberFormater.FormatNumber(Hearts)}"; 
        HeartsSessionText.text = $"{NumberFormater.FormatNumber(Hearts)}";
    }

    public void UpdateAllTextUI()
    {
        if (YandexGame.lang == "ru")
        {
            ScoreText.text = $"Очки: {statsManager.Score}";
        }
        else if (YandexGame.lang == "tr")
        {
            ScoreText.text = $"Oyundaki: {statsManager.Score}";
        }
        else
        {
            ScoreText.text = $"Points: {statsManager.Score}";
        }
        CoinsCountAllText.text = $"{NumberFormater.FormatNumber(statsManager.AllCountCoins)}";
        CrystallCountAllText.text = $"{NumberFormater.FormatNumber(statsManager.AllCrystallCount)}";

    }

    public void UpdateCoinsCountAllText(int value)
    {
        CoinsCountAllText.text = NumberFormater.FormatNumber(value);
    }

    public void UpdateCrystallsCountAllText(int value)
    {
        CrystallCountAllText.text = NumberFormater.FormatNumber(value);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueLevelByHeart() 
    {
        if(statsManager.AllHeartsCount >= 3)
        {
            _ShootSystem.RespawnOnLevel();
            statsManager.AddHearts(-3);
            UpdateHeartsUi(statsManager.AllHeartsCount);
        }      
    }

    public void ActiveDeadPanel()
    {
        DeadPanel.SetActive(true);
    }

    public void LevelCompleteStats()
    {
        statsManager.AddCoins(statsManager.GetCoinsSession());
        statsManager.AddCrystalls(statsManager.GetCrystallSession());

        int rnd = Random.Range(0, 100);
        statsManager.AddCoins(50 + rnd);

        UpdateRewardText(50 + rnd);
    }

    public void UpdateRewardText(int Value)
    {
        RewardText.text = Value.ToString();
    }
}
