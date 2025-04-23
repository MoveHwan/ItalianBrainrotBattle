using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaBattle : MonoBehaviour
{
    public float health;

    public float attackPow;    // ������
    public float skillPer;     // ���� ��� ���ݷ�;
    public float attackPer;    // ���� Ȯ��

    Animator anim;
    SpriteRenderer spriteRenderer;


    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    void Start()
    {
        
    }

    void OnEnable()
    {
        health = attackPow;
    }

    // ���� ���� �ִϸ��̼�
    public void AttackAni()
    {
        anim.SetTrigger("Attack");
    }

    // ������ ���� ������ �ֱ�
    public void AttackMom()
    {
        float rand = Random.value; // 0.0 ~ 1.0 ������ ��
        if (rand < attackPer)   // ���� ����
        {
            float damage = attackPow * skillPer;
            BattleManager.instance.AttackSucc(damage);
        }
        else
        {
            BattleManager.instance.AttackFail();
        }
    }

    // ������ ��
    public void AttackDone()
    {
        BattleManager.instance.ChangeAttack();
    }

    // �¾��� ��
    public void HitDmg(float damage)
    {
        health -= damage;
        TakeHitEffect();

        if (health <= 0)
        {
            BattleManager.instance.BattleEnd();
        }
    }

    public void TakeHitEffect()
    {
        StartCoroutine(HitFeedback());
    }

    IEnumerator HitFeedback()
    {
        Vector3 originalPos = transform.parent.localPosition;
        Color originalColor = spriteRenderer.color;

        // ���� ������
        spriteRenderer.color = Color.red;

        // ����
        float shakeAmount = 0.1f;
        int shakeCount = 3;
        for (int i = 0; i < shakeCount; i++)
        {
            transform.parent.localPosition = originalPos + (Vector3.right * Random.Range(-shakeAmount, shakeAmount));
            yield return new WaitForSeconds(0.02f);
        }

        // ������� ����
        transform.parent.localPosition = originalPos;
        spriteRenderer.color = originalColor;
    }
}
