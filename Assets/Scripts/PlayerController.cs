using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Move Speed")]
    [SerializeField]
    [Range(0f, 10f)]
    private float speed;
    public float Speed { get { return speed; } }

    [Header("Player Bullet")]
    [SerializeField]
    private List<GameObject> bullets;
    public List<GameObject> Bullets { get { return bullets; } }

    [Header("Player Fire Position")]
    [SerializeField]
    private Transform firePos;
    public Transform FirePos { get { return firePos; } }

    float FireTimer = 0;
    Vector2 direction;

    void Start()
    {
        
    }

    void Update()
    {
        FireTimer += Time.deltaTime;
        
        if (GameManager.Instance.isFire && FireTimer > GameManager.Instance.FireTimer)
        {
            GameManager.Instance.isFire = false;
            FireTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        direction = new Vector2();
        if (Input.GetKey(KeyCode.A))
            direction += Vector2.left;
        if (Input.GetKey(KeyCode.S))
            direction += Vector2.down;
        if (Input.GetKey(KeyCode.D))
            direction += Vector2.right;
        if (Input.GetKey(KeyCode.W))
            direction += Vector2.up;
        transform.Translate(direction * Speed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.Space) && !GameManager.Instance.isFire)
        {
            GameManager.Instance.isFire = true;
            // Bullet Create
            GameObject obj = (GameObject)Instantiate(Bullets[GameManager.Instance.Player_Bullet]);
            obj.transform.position = FirePos.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
