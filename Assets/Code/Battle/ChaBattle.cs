using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaBattle : MonoBehaviour
{
    public float health;

    public float attackPow;    // 전투력
    public float skillPer;     // 투력 대비 공격력;
    public float attackPer;    // 공격 확률

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

    // 공격 시작 애니메이션
    public void AttackAni()
    {
        anim.SetTrigger("Attack");
    }

    // 때리는 순간 데미지 주기
    public void AttackMom()
    {
        float rand = Random.value; // 0.0 ~ 1.0 사이의 값
        if (rand < attackPer)   // 공격 성공
        {
            float damage = attackPow * skillPer;
            BattleManager.instance.AttackSucc(damage);
        }
        else
        {
            BattleManager.instance.AttackFail();
        }
    }

    // 때리기 끝
    public void AttackDone()
    {
        BattleManager.instance.ChangeAttack();
    }

    // 맞았을 때
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

        // 색상 빨갛게
        spriteRenderer.color = Color.red;

        // 흔들기
        float shakeAmount = 0.1f;
        int shakeCount = 3;
        for (int i = 0; i < shakeCount; i++)
        {
            transform.parent.localPosition = originalPos + (Vector3.right * Random.Range(-shakeAmount, shakeAmount));
            yield return new WaitForSeconds(0.02f);
        }

        // 원래대로 복구
        transform.parent.localPosition = originalPos;
        spriteRenderer.color = originalColor;
    }
}
