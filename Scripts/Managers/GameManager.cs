using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform PlayerPosition;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Application.targetFrameRate = 60;   
    }

    void Update()
    {
        
    }
}
