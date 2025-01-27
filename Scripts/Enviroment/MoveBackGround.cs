using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    public float speed = 2.0f;  // �������� �������� ����
    private Vector3 startPosition;
    private float spriteHeight;

    void Start()
    {
        // ��������� ��������� �������
        startPosition = transform.position;

        // �������� ������ �������
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // ������������ ����� �������
        float newPosition = Mathf.Repeat(Time.time * speed, spriteHeight);

        // ��������� ����� ������� � �������
        transform.position = startPosition + Vector3.down * newPosition;
    }
}
