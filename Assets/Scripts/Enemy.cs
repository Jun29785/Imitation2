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
    [SerializeField] [Range(0, 10f)]
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
    private List<GameObject> bullets;
    public List<GameObject> Bullets { get { return bullets; } }

    bool isEntered = false;

    GameObject EnemySprite;

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
    }

    void EnemyEnter()
    {
        switch (EnemyType)
        {
            case EnemyType.Germ:
                if (transform.position.y > 4f)
                {
                    transform.Translate(Speed * 1.1f * Time.deltaTime * Vector2.down);
                }
                else
                {
                    isEntered = true;
                    EnemyFire();
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
        }
    }

    void EnemyFire()
    {
        switch (EnemyType)
        {
            case EnemyType.Germ:
                StopCoroutine(GermFirePattern());
                StartCoroutine(GermFirePattern());
                break;
        }
    }

    IEnumerator GermFirePattern()
    {
        for (int i = 0, term = -90; i < 6; i++,term += 30)
        {
            GameObject obj = (GameObject)Instantiate(Bullets[0]);
            obj.transform.position = FirePos.position;
            if (obj.transform.position.x >= 0)
                obj.transform.rotation = Quaternion.Euler(0, 0, term);
            else
                obj.transform.rotation = Quaternion.Euler(0, 0, -term);
            // Enemy Bullet Direction Settings
            //obj.GetComponent<EnemyBullet>().Direction = Vector2.down;
            yield return new WaitForSeconds(0.7f);
        }
        Invoke("EnemyFire", 2f);
    }
}
