using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isFire;
    public float FireTimer;

    [Header("Player Stats")]
    [SerializeField]
    [Range(0, 100)]
    private int player_Hp;
    public int Player_Hp { get { return player_Hp; } set { player_Hp = value; } }
    [SerializeField] [Range(0,10)]
    private int player_Atk;
    public int Player_Atk { get { return player_Atk; } set { player_Atk = value; } }
    [SerializeField]
    private int player_Pain;
    public int Player_Pain { get { return player_Pain; } set { player_Pain = value; } }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        Mathf.Clamp(player_Hp, 0, 100);
        Mathf.Clamp(player_Pain, 0, 100);
    }
}
