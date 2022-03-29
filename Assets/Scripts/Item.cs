using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    [SerializeField]
    private ItemType itemType;
    public ItemType ItemType { get { return itemType; } }

    void Start()
    {
        
    }

    void Update()
    {
       
    }

    void ItemEffect()
    {
        switch (ItemType)
        {
            case ItemType.BulletUpgrade:
                GameManager.Instance.Player_Bullet += 1;
                break;
            case ItemType.God:
                // 무적모드
                break;
            case ItemType.Heal:
                GameManager.Instance.Player_Hp += 20;
                break;
            case ItemType.Pain:
                GameManager.Instance.Player_Pain -= 20;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Entered Player");
            ItemEffect();
            Destroy(this.gameObject);
        }
    }
}
