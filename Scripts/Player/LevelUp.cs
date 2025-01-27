using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUp : MonoBehaviour
{

    [SerializeField]
    Sprite[] playerModels;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    public int NowLevel = 0;
    [SerializeField]
    public int Maxlevel = 25;

    void Start()
    {
        //UpdateNumberText();
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    IncreaseNumber();
        //}
    }

    public void IncreaseNumber()
    {
        if (NowLevel < Maxlevel)
        {
            NowLevel++;

            switch (NowLevel)
            {
                case 5:
                    ChangeCharacterModel(1);
                    break;
                case 10:
                    ChangeCharacterModel(2);
                    break;
                case 15:
                    ChangeCharacterModel(3);
                    break;
                default:
                    break;
            }

        }
    }

    void ChangeCharacterModel(int modelIndex)
    {
        spriteRenderer.sprite = playerModels[modelIndex - 1];
    }
}
