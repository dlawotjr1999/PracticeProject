using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRoamingState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;                        // 가지고 있는 몬스터

    private Vector3 m_destination;                          // 로밍 목적지

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }

        m_destination = new(Random.Range(8f, -8f), 0, Random.Range(8f, -8f));           // 랜덤 목적지 설정
        m_monster.SetDestination(transform.position + m_destination);                   // 네비 경로 설정
        m_monster.SetSpeed(true);                                                       // 로밍 속도 설정

        m_monster.MoveAnim();
    }

    private void ArriveChk()            // 도착했는지 확인
    {
        if (!m_monster.Arrived)
            return;

        m_monster.IdleMonster();
    }

    public void Proceed()
    {
        ArriveChk();
    }
}
