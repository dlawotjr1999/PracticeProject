using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAttackState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;

    private float m_attackDelay;
    private float DelayCnt { get; set; }

    private bool AttackingDefense { get; set; }                 // 방어 대상 공격하는지

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }

        m_monster.StopMove();                                   // 움직임 멈추기
        m_monster.AttackPlayer();                               // 몬스터의 공격 함수 실행
        m_attackDelay = m_monster.AttackSpeed;                  // 공격 속도 설정
        m_monster.AttackCoolCnt = m_monster.AttackSpeed;        // 몬스터 실제 공격 쿨타임 설정

        DelayCnt = m_attackDelay;                               // 카운트 설정
        AttackingDefense = false;

        m_monster.AttackAnim();
    }

    public void ChangeToDef(MonsterScript _monster)       // 방어전 공격
    {
        if (!m_monster) { m_monster = _monster; }

        m_monster.StopMove();                                   // 움직임 멈추기
        m_monster.AttackDefenseObj();                           // 몬스터의 공격 함수 실행
        m_attackDelay = m_monster.AttackSpeed;                  // 공격 속도 설정
        m_monster.AttackCoolCnt = m_monster.AttackSpeed;        // 몬스터 실제 공격 쿨타임 설정

        DelayCnt = m_attackDelay;                               // 카운트 설정
        AttackingDefense = true;

        m_monster.AttackAnim();
    }

    public void Proceed()
    {
        if(!AttackingDefense)
           m_monster.WatchPlayer();            // 플레이어 쳐다보기
        DelayCnt -= Time.deltaTime;

        if(DelayCnt <= 0)
        {
            if (AttackingDefense)
                m_monster.AttackDefenseMonster();
            else
                m_monster.ApproachMonster();
        }
    }
}
