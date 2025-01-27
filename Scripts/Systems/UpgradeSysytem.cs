using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UpgradeSysytem : MonoBehaviour
{
    [SerializeField] private ShootSystem _ShootSystem;
    [SerializeField] private StatsManager _StatsManager;
    [SerializeField] private LevelUp _LevelUp;
    [SerializeField] private UIManager _UIManager;


    [SerializeField] private Text AttackSpeedLevelText;
    [SerializeField] private Text AttackSpeedCostText;
    [SerializeField] private Text DamageLevelText;
    [SerializeField] private Text DamageCostText;

    [SerializeField] private Text NowLevelText;
    [SerializeField] private Text HeroLevelCostText;
    [SerializeField] private Text HeroLevelCrystallCostText;

    public int HeroLevelUpCost = 100;
    public int HeroLevelUpCrystallCost = 50;

    public int DamageLevelNow = 0;
    public int DamageLevelMax = 100;

    public int AttackSpeedLevelNow = 0;
    public int AttackSpeedLevelMax = 100;

    public int AttackSpeedCost = 0;
    public int DamageCost = 0;


    void Start()
    {
        DamageLevelNow = SaveLoadData.LoadDataInt("DamageLevel");
        AttackSpeedLevelNow = SaveLoadData.LoadDataInt("AttackSpeedLevel");
        _LevelUp.NowLevel = SaveLoadData.LoadDataInt("HeroLevel");

        _ShootSystem.Cooldown = SaveLoadData.LoadDataFloat("CoolDownPlayer", 0.8f);
        _ShootSystem.damage = SaveLoadData.LoadDataInt("DamagePlayer", 40);

        DamageCost = SaveLoadData.LoadDataInt("DamageCost", DamageCost);
        AttackSpeedCost = SaveLoadData.LoadDataInt("AttackSpeedCost", AttackSpeedCost);
        HeroLevelUpCost = SaveLoadData.LoadDataInt("HeroLevelCost", HeroLevelUpCost);
        HeroLevelUpCrystallCost = SaveLoadData.LoadDataInt("HeroLevelUpCrystallCost", HeroLevelUpCrystallCost);


        DamageLevelUpText();
        AttackSpeedUpText();
        HeroLevelUpText();
    }

    void Update()
    {

    }

    public void AttackSpeedUp()
    {
        if (AttackSpeedLevelNow < AttackSpeedLevelMax && _StatsManager.AllCountCoins > AttackSpeedCost)
        {
            _ShootSystem.Cooldown -= 0.0125f;

            AttackSpeedLevelNow++;

            SaveLoadData.SaveDataInt("AttackSpeedLevel", AttackSpeedLevelNow);
            SaveLoadData.SaveDataFloat("CoolDownPlayer", _ShootSystem.Cooldown);

            _StatsManager.AllCountCoins -= AttackSpeedCost;

            AttackSpeedUpCostText();
            AttackSpeedUpText();

            SaveLoadData.SaveDataInt("Crystalls", _StatsManager.AllCrystallCount);
            SaveLoadData.SaveDataInt("Coins", _StatsManager.AllCountCoins);

            _UIManager.UpdateAllTextUI();
        }

    }

    public void DamageUp()
    {
        if (DamageLevelNow < DamageLevelMax && _StatsManager.AllCountCoins > DamageCost)
        {           
            _ShootSystem.damage += 10;

            DamageLevelNow++;

            SaveLoadData.SaveDataInt("DamageLevel", DamageLevelNow);
            SaveLoadData.SaveDataInt("DamagePlayer", _ShootSystem.damage);

            _StatsManager.AllCountCoins -= DamageCost;

            DamageLevelUpText();
            DamageCostUpText();

            SaveLoadData.SaveDataInt("Crystalls", _StatsManager.AllCrystallCount);
            SaveLoadData.SaveDataInt("Coins", _StatsManager.AllCountCoins);

            _UIManager.UpdateAllTextUI();
        }

    }

    public void DamageLevelUpText()
    {

        DamageLevelText.text = $"{DamageLevelNow} / {DamageLevelMax}";
        DamageCostText.text = $"{DamageCost}";

    }

    public void DamageCostUpText()
    {
        DamageCost += 100;
        
        SaveLoadData.SaveDataInt("DamageCost", DamageCost);
    }

    public void AttackSpeedUpText()
    {

        AttackSpeedLevelText.text = $"{AttackSpeedLevelNow} / {AttackSpeedLevelMax}";
        AttackSpeedCostText.text = $"{AttackSpeedCost}";
    }

    public void AttackSpeedUpCostText()
    {
        AttackSpeedCost += 100;

        SaveLoadData.SaveDataInt("AttackSpeedCost", AttackSpeedCost);
    }

    public void HeroLevelUp()
    {

        if (_StatsManager.AllCountCoins > HeroLevelUpCost && _LevelUp.NowLevel < _LevelUp.Maxlevel && _StatsManager.AllCrystallCount > HeroLevelUpCrystallCost)
        {
            //_StatsManager.AllCountCoins -= HeroLevelUpCost;

            _LevelUp.IncreaseNumber();

            _StatsManager.AddCoins(-HeroLevelUpCost);
            _StatsManager.AddCrystalls(-HeroLevelUpCrystallCost);
            _UIManager.UpdateAllTextUI();

            HeroLevelUpCost += 100;
            HeroLevelUpCrystallCost += 50;

            SaveLoadData.SaveDataInt("HeroLevelCost", HeroLevelUpCost);
            SaveLoadData.SaveDataInt("HeroLevelUpCrystallCost", HeroLevelUpCrystallCost);
            SaveLoadData.SaveDataInt("HeroLevel", _LevelUp.NowLevel);

            SaveLoadData.SaveDataInt("Crystalls", _StatsManager.AllCrystallCount);
            SaveLoadData.SaveDataInt("Coins", _StatsManager.AllCountCoins);

            HeroLevelUpText();
        }
    }

    public void HeroLevelUpText()
    {
        if(_LevelUp.NowLevel < _LevelUp.Maxlevel)
        {
            NowLevelText.text = $"{_LevelUp.NowLevel}";
            HeroLevelCostText.text = $"{HeroLevelUpCost}";
            HeroLevelCrystallCostText.text = $"{HeroLevelUpCrystallCost}";
        }
        else
        {
            NowLevelText.text = $"{_LevelUp.NowLevel}";

            if (YandexGame.lang == "ru")
            {
                HeroLevelCostText.text = "Макс уровень";
                HeroLevelCrystallCostText.text = "Макс уровень";
            }
            else if (YandexGame.lang == "tr")
            {
                HeroLevelCostText.text = "Maksimum seviye";
                HeroLevelCrystallCostText.text = "Maksimum seviye";
            }
            else
            {
                HeroLevelCostText.text = "Max level";
                HeroLevelCrystallCostText.text = "Max level";
            }
        }

    }
}
