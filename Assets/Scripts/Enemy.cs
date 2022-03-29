using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int hp;
    public int Hp { get { return hp; } set { hp = value; } }

    [SerializeField]
    private int atk;
    public int Atk { get { return atk; } set { atk = value; } }

    [SerializeField]
    [Range(0, 10f)]
    private float speed;
    public float Speed { get { return speed; } }

    [Header("FirePos")]
    [SerializeField]
    private Transform firePos;
    public Transform FirePos { get { return firePos; } }

    [Header("Enemy Type")]
    [SerializeField]
    private EnemyType enemyType;
    public EnemyType EnemyType { get { return enemyType; } }

    [Header("Enemy Direction")]
    [SerializeField]
    private EnemyDirection direction;
    public EnemyDirection Direction { get { return direction; } }

    [Header("Enemy Bullet")]
    [SerializeField]
    private GameObject bullet;
    public GameObject Bullet { get { return bullet; } }

    bool isEntered = false;
    bool canFire = false;
    GameObject EnemySprite;

    float turnTimer = 0;
    Vector2 Dir;

    private void Awake()
    {
        EnemySprite = transform.GetChild(0).gameObject;
    }

    void Start()
    {
        EnemyEnter();
    }

    void Update()
    {
        //Debug.Log("Enemy HP : " + Hp);
        if (!(Hp > 0))
            Destroy(this.gameObject);
        if (isEntered)
        {
            EnemyMove();
        }
        if (canFire)
        {
            canFire = false;
            EnemyFire();
        }
    }

    void EnemyEnter()
    {
        switch (EnemyType)
        {
            case EnemyType.Germ: // 세균
                if (transform.position.y > 4f)
                {
                    transform.Translate(Speed * 1.1f * Time.deltaTime * Vector2.down);
                }
                else
                {
                    isEntered = true;
                    canFire = true;
                }
                break;
            case EnemyType.Virus: // 바이러스
                if (transform.position.y < 4f)
                    transform.Translate(Speed * 2.3f * Time.deltaTime * Vector2.up);
                else
                {
                    isEntered = true;
                    canFire = true;
                }
                break;
            case EnemyType.Cancer:
                if (transform.position.y > 4f)
                    transform.Translate(speed * 1.3f * Time.deltaTime * Vector2.down);
                else
                {
                    isEntered = true;
                    canFire = true;
                }
                break;
            default:
                isEntered = true;
                break;
        }
        if (!isEntered)
            Invoke("EnemyEnter", Time.deltaTime);
    }

    void EnemyMove()
    {
        switch (EnemyType)
        {
            case EnemyType.Bacteria:
                transform.Translate(Speed / 100 * Vector2.down * Time.fixedDeltaTime);
                EnemySprite.transform.RotateAround(transform.position, Vector3.back, Speed * 2 * Time.fixedDeltaTime * (int)Direction);
                break;
            case EnemyType.Germ:
                transform.Translate(Vector2.down * Speed * Time.deltaTime);
                break;
            case EnemyType.Virus:
                transform.Translate(Vector2.down * Speed * Time.deltaTime);
                break;
            case EnemyType.Cancer:
                CancerMove();
                break;
        }
    }

    void EnemyFire()
    {
        switch (EnemyType)
        {
            case EnemyType.Germ:
                GermFire();
                break;
            case EnemyType.Virus:
                if (isEntered)
                {
                    StopCoroutine(VirusFirePattern());
                    StartCoroutine(VirusFirePattern());
                }
                break;
        }
    }

    IEnumerator VirusFirePattern()
    {
        for (int i = 0, term = -90; i < 6; i++, term += 30)
        {
            GameObject obj = (GameObject)Instantiate(Bullet);
            obj.GetComponent<EnemyBullet>().Atk = Atk;
            obj.transform.position = FirePos.position;
            if (obj.transform.position.x >= 0)
                obj.transform.rotation = Quaternion.Euler(0, 0, term);
            else
                obj.transform.rotation = Quaternion.Euler(0, 0, -term);
            obj.GetComponent<EnemyBullet>().Direction = Vector2.down;
            yield return new WaitForSeconds(0.04f);
        }
        Invoke("EnemyFire", Random.Range(0.5f, 1.7f));
    }

    void GermFire()
    {
        GameObject obj = (GameObject)Instantiate(bullet);
        obj.transform.position = FirePos.position;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = (player.position - obj.transform.position).normalized;
        obj.GetComponent<EnemyBullet>().Direction = direction;
        Invoke("EnemyFire", Random.Range(0.2f, 1.5f));
    }

    void CancerMove()
    {
        // 계단 형식 지그재그 이동
        turnTimer += Time.deltaTime;
        if (turnTimer > 1f)
        {
            Dir = Vector2.down;
        }
        else if(turnTimer > .5f)
        {
            Dir = new Vector2((float)Direction, 0);
        }
        transform.Translate(Dir * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어와 닿으면 데미지의 절반
            GameManager.Instance.Player_Hp -= Atk;
        }
        if (collision.CompareTag("despawn") && isEntered)
        {
            // 디스폰 벽에 닿으면 삭제 및 고통 게이지 증가
            Destroy(this.gameObject);
            GameManager.Instance.Player_Pain += (Atk / 2);
        }
    }
}
