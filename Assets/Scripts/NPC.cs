using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class NPC : MonoBehaviour
{
    [Header("NPC Type")]
    [SerializeField]
    private NpcType npcType;
    public NpcType NpcType { get { return npcType; } }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void NpcEffect()
    {
        switch (NpcType)
        {
            case NpcType.WhiteBloodCell:
                // Create Item
                break;
            case NpcType.RedBloodCell:
                GameManager.Instance.Player_Pain += 10;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player");
            NpcEffect();
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.layer == 8)
        {
            Debug.Log("Player Bullet");
            NpcEffect();
            Destroy(this.gameObject);
        }
    }
}
