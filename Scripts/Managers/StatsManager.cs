using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public int AllCountCoins = 0;
    private int SessionCountCoins = 0;

    public int AllCrystallCount = 0;
    private int SessionCrystallCount = 0;

    public int AllHeartsCount = 0;

    public int Score = 0;
    //private int RecordScore = 0;

    public int LevelCounter = 0;

    private UIManager uiManager;

    void Start()
    {
        uiManager = UIManager.Instance;

        AllCountCoins = SaveLoadData.LoadDataInt("Coins");
        uiManager.UpdateCoinsCountAllText(AllCountCoins);

        AllCrystallCount = SaveLoadData.LoadDataInt("Crystalls");
        uiManager.UpdateCrystallsCountAllText(AllCrystallCount);

        AllHeartsCount = SaveLoadData.LoadDataInt("Hearts");
        uiManager.UpdateHeartsUi(AllHeartsCount);

        uiManager.UpdateTextUI(Score, SessionCountCoins, SessionCrystallCount);
    }

    void Update()
    {

    }

    private void Awake()
    {
        Instance = this;

    }

    public void AddCoins(int value)
    {
        AllCountCoins += value;
        SaveLoadData.SaveDataInt("Coins", AllCountCoins);
    }
    public void AddCoinsSession(int coin)
    {
        SessionCountCoins += coin;
        uiManager.UpdateTextUI(Score, SessionCountCoins, SessionCrystallCount);
    }
    public void AddCrystalls(int value)
    {
        AllCrystallCount += value;
        SaveLoadData.SaveDataInt("Crystalls", AllCrystallCount);
    }

    public void AddCrystallSession(int crystall)
    {
        SessionCrystallCount += crystall;
        uiManager.UpdateTextUI(Score, SessionCountCoins, SessionCrystallCount);
    }

    public void AddHearts(int value)
    {
        AllHeartsCount += value;
        SaveLoadData.SaveDataInt("Hearts", AllHeartsCount);
        uiManager.UpdateHeartsUi(AllHeartsCount);
    }

    public void AddScore(int score)
    {
        Score += score;
        uiManager.UpdateTextUI(Score, SessionCountCoins, SessionCrystallCount);
    }

    public void AddLevelComplete()
    {
        LevelCounter++;
        SaveLoadData.SaveDataInt("LevelCounter", LevelCounter);
    }



    public int GetCoinsSession()
    {
        return SessionCountCoins;
    }
    public int GetCrystallSession()
    {
        return SessionCrystallCount;
    }
}
