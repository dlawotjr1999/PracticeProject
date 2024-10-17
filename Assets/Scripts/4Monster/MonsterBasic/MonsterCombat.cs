using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterScript
{
    private const float ArangeTime = 5;         // 정리 완료까지 시간
    private bool Aranged { get; set; }          // 정리 됐는지 여부

    private void AttackAction(Vector3 _target)           // 공격 행동
    {
        StartCoroutine(CreateAttack(_target));
        StartCoroutine(DoneAttack());
    }

    public void AttackPlayer()                          // 플레이어 공격
    {
        AttackAction(PlayMgr.PlayerPos);
    }

    public void AttackDefenseObj()                      // 방어 대상 공격
    {
        AttackAction(PlayMgr.DefenseObjPos);
    }

    public virtual IEnumerator CreateAttack(Vector3 _target)    // 실제 공격 함수
    {
        yield return null;
    }

    public virtual IEnumerator DoneAttack()                     // 공격 종료 함수
    {
        yield return null;
    }

    private void DisplayDamage(float _damage)               // 데미지 UI 보여주기
    {
        
    }

    public virtual void DieAction()
    {
        SetDead();
        StopMove();
        DieAnim();
    }

    public virtual void GetDamage(float _damage)
    {
        if (IsDead) return;

        CurHP -= (int)_damage;

        if(CurHP <= 0)
        {
            DieMonster();
            return;
        }
        else if(CurState != m_hit)
        {
            HitMonster();
        }
        if (Aranging)           // 정리 중이면
        {
            ChkAranged();       // 체력 확인
        }
        m_hpBar.SetHP(CurHP);
        DisplayDamage(_damage);
    }

    public virtual void ChkAranged()                            // 정리 됐는지 확인
    {
        if (Aranged) return;

        ArangeRoom arange = m_parent as ArangeRoom;
        if(arange == null) { Debug.LogError("정리 방 아닌 곳에서 정리 중"); return; }

        float critPer = arange.HPPer;           // 정리 기준 체력 %
        float hpPer = CurHP / MaxHP;            // 현재 체력 %
        if (hpPer <= critPer)                   // 기준보다 낮으면
        {
            StartCoroutine(ArangeCorutine(arange));     // 5초 재기
            m_hpBar.ArangeOn();                         // 체력바 변경
            Aranged = true;                             // 정리중
        }
    }

    private IEnumerator ArangeCorutine(ArangeRoom _room)        // 5초 경과 코루틴
    {
        yield return new WaitForSeconds(ArangeTime);
        if(!IsDead)     // 살아 있으면
        {
            PlayMgr.CreateMonsterEffect(EMonsterEffect.ARANGED, transform.position);
            _room.MonsterAranged(this);         // 정리됨 판정
            m_parent.MonsterDead(this);
            Destroy(m_hpBar.gameObject);
            Destroy(gameObject);
        }
    }
}
