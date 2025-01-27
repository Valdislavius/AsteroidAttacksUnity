using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    public float speed = 2.0f;  // Скорость движения фона
    private Vector3 startPosition;
    private float spriteHeight;

    void Start()
    {
        // Сохраняем начальную позицию
        startPosition = transform.position;

        // Получаем высоту спрайта
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // Рассчитываем новую позицию
        float newPosition = Mathf.Repeat(Time.time * speed, spriteHeight);

        // Применяем новую позицию к спрайту
        transform.position = startPosition + Vector3.down * newPosition;
    }
}
