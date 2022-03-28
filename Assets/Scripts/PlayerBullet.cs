using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Bullet Speed")]
    [SerializeField]
    [Range(0f, 10f)]
    private float speed;
    public float Speed { get { return speed; } }

    float LifeTimer;

    void Start()
    {
        
    }

    private void Update()
    {
        LifeTimer += Time.deltaTime;
        if (LifeTimer > 3.0f)
            Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * Speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            collision.transform.parent.GetComponent<Enemy>().Hp -= GameManager.Instance.Player_Atk;
        }
    }
}
