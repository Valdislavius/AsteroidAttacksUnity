using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ShootSystem>().ActivePlatform();


            Destroy(this.gameObject);
        }
    }
}
