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

    // ������������ �� ������� �������� ������� � OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }


    private void Start()
    {
        _StatsManager = GetComponent<StatsManager>();
        _UIManager = UIManager.Instance;
    }


    // ����������� ����� ��������� �������
    void Rewarded(int id)
    {
        // ���� ID = 1, �� ����� "+100 �����"
        if (id == 1)
        {
            _StatsManager.AddCoins(1500);
            _UIManager.UpdateAllTextUI();
        }
            

        // ���� ID = 2, �� ����� "+������".
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

    // ����� ��� ������ ����� �������
    public void ExampleOpenRewardAd(int id)
    {
        // �������� ����� �������� ����� �������
        YandexGame.RewVideoShow(id);
    }
}
