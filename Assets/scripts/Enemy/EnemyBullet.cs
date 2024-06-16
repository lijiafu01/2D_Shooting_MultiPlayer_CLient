
using GameClient.Enums;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7f; // tốc độ di chuyển của viên đạn
    private Rigidbody2D rb;
    public float damage = 0f;
    public bool isSkill = false;
    public int category;
    public bool isShooter = false;
    public int shooterID;
    private void Start()
    {
        // Kiểm tra xem Rigidbody2D đã được gắn vào GameObject hay chưa
        rb = GetComponent<Rigidbody2D>();

        // Nếu chưa có Rigidbody2D, thì thêm nó vào
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0;
        Destroy(gameObject, 1.5f);
    }

    
    private void Update()
    {
        rb.velocity = transform.up * -speed;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
