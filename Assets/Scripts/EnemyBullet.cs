using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector2 Direction;

    [Header("Bullet Speed")]
    [SerializeField]
    [Range(0f,10f)]
    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    [Header("Bullet Damage")]
    [SerializeField]
    private int atk;
    public int Atk { get { return atk; } set { atk = value; } }

    float LifeTimer;

    void Start()
    {
        LifeTimer = 0;
    }

    void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
        LifeTimer += Time.deltaTime;
        if (LifeTimer > 5f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance
        }
    }
}
