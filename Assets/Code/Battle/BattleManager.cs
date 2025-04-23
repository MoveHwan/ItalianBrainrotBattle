using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    bool playerTurn;
    bool enemyTurn;

    public ChaBattle playerCha;
    public ChaBattle enemyCha;

    //public float playerPow;
    //public float enemyPow;

    bool battleEnd;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartBattle()
    {
        battleEnd = false;

        if(playerCha.attackPow >= enemyCha.attackPow)
        {
            playerTurn = true;
            AttackCom();
        }
        else
        {
            enemyTurn = true;
            AttackCom();
        }
    }

    // ���� ���
    void AttackCom()
    {
        if (playerTurn)
        {
            playerCha.AttackAni();
        }
        else if (enemyTurn)
        {
            enemyCha.AttackAni();
        }
    }

    // ���� ����
    public void AttackSucc(float damage)
    {
        if (playerTurn)
        {
            enemyCha.HitDmg(damage);
        }
        else if (enemyTurn)
        {
            playerCha.HitDmg(damage);
        }
    }

    // ���� ����
    public void AttackFail()
    {

    }

    // ���� ����
    public void ChangeAttack()
    {
        if (playerTurn && !battleEnd)
        {
            playerTurn = false;
            enemyTurn = true;
            AttackCom();
        }
        else if (enemyTurn && !battleEnd)
        {
            enemyTurn = false;
            playerTurn = true;
            AttackCom();
        }
    }

    public void BattleEnd()
    {
        battleEnd = true;
        Debug.Log("battleEnd");
    }
}
