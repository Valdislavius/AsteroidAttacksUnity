using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using YG;

public class SettingsSystem : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SoundSlider;

    public AudioSource MusicSource;
    public AudioSource SoundSource;

    public Volume PPV;
    public bool IsPPV = false;
    void Start()
    {
        YandexGame.LoadProgress();


        MusicSource.volume = SaveLoadData.LoadDataFloat("MusicVolume", 0.5f);
        SoundSource.volume = SaveLoadData.LoadDataFloat("SoundVolume", 0.5f);

        MusicSlider.value = MusicSource.volume;
        SoundSlider.value = SoundSource.volume;


        if (!YandexGame.savesData.isFirstGame)
        {
            if (Application.isMobilePlatform)
            {
                PPV.enabled = false;
            }
            else
            {
                PPV.enabled = true;
            }
        }
        else
        {
            PPV.enabled = YandexGame.savesData.isPPV;
        }
    }

    void Update()
    {

    }

    public void OnMusicSliderChange()
    {
        MusicSource.volume = MusicSlider.value;

        SaveLoadData.SaveDataFloat("MusicVolume", MusicSource.volume);
    }

    public void OnSoundSliderChange()
    {
        SoundSource.volume = SoundSlider.value;

        SaveLoadData.SaveDataFloat("SoundVolume", SoundSource.volume);
    }

    public void OnPPVChange()
    {
        YandexGame.savesData.isFirstGame = true;
        
        IsPPV = !IsPPV;
        PPV.enabled = IsPPV;

        YandexGame.savesData.isPPV = IsPPV;
        YandexGame.SaveProgress();
    }
}
