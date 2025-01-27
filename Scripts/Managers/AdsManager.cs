using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AdsManager : MonoBehaviour
{
    private StatsManager _StatsManager;
    private UIManager _UIManager;

    [SerializeField] private ShootSystem _shootSystem;


    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }


    private void Start()
    {
        _StatsManager = GetComponent<StatsManager>();
        _UIManager = UIManager.Instance;
    }


    // Подписанный метод получения награды
    void Rewarded(int id)
    {
        // Если ID = 1, то выдаём "+100 монет"
        if (id == 1)
        {
            _StatsManager.AddCoins(1500);
            _UIManager.UpdateAllTextUI();
        }
            

        // Если ID = 2, то выдаём "+оружие".
        else if (id == 2)
        {
            _StatsManager.AddCrystalls(50);
            _UIManager.UpdateAllTextUI();
        }

        else if(id == 3)
        {
            _shootSystem.RespawnOnLevel();
        }
            
    }

    // Метод для вызова видео рекламы
    public void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
        YandexGame.RewVideoShow(id);
    }
}
