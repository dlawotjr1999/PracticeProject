using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIdleState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;

    private const float m_idleTime = 1;                 // IDLE에 있는 시간
    private float RoamingCnt { get; set; }

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }

        m_monster.StopMove();                           // 움직임 멈추기
        RoamingCnt = m_idleTime;                        // 로밍까지 시간 체크

        m_monster.IdleAnim();
    }

    public void Proceed()
    {
        RoamingCnt -= Time.deltaTime;

        if (RoamingCnt <= 0)                            // 시간이 되면
        {
            m_monster.RoamingMonster();                 // 로밍으로
        }
    }
}
