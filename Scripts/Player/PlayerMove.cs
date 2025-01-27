using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMove : MonoBehaviour

{
    [SerializeField] private float speed = 100f;

    [SerializeField] private float rotationSpeed = 5f;

    private float targetRotationY = 0f;


    void Start()
    {
        
    }

    void Update()
    {
        Move();
        
        float clampedX = Mathf.Clamp(transform.position.x, -2.34f, 2.44f);
        transform.position = new Vector2(clampedX, transform.position.y);
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
        }
        else if (Input.GetMouseButton(0))
        {
            //Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), 0);
            //transform.Translate(mouse * Time.deltaTime * speed);

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPos = new Vector2(mousePos.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

            // Вычисляем направление движения (влево или вправо)
            float direction = targetPos.x - transform.position.x;

            // Резко устанавливаем угол поворота при смещении
            if (direction < 0)
            {
                targetRotationY = -40f;
            }
            else if (direction > 0)
            {
                targetRotationY = 40f;
            }
            else
            {
                // Если направление не изменилось, плавно возвращаем угол к 0
                targetRotationY = 0f;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 0.6f;
        }
        else
        {
            // Если мышка не нажата, плавно возвращаем угол к 0
            targetRotationY = 0f;
        }

        // Плавно интерполируем угол поворота к целевому углу
        Quaternion targetRotation = Quaternion.Euler(0, targetRotationY, 0);

        if (targetRotationY != 0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 1f);
        }
    }
}
