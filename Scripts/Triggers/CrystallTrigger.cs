using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystallTrigger : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private float speed = 10f;

    private int crystallsCounter = 1;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, gameManager.PlayerPosition.position, Time.deltaTime * speed);

        float distance = Vector3.Distance(transform.position, gameManager.PlayerPosition.position);
        if (distance <= 0.2f)
        {
            StatsManager.Instance.AddCrystallSession(crystallsCounter);

            LeanPool.Despawn(this.gameObject);
        }
    }

}
