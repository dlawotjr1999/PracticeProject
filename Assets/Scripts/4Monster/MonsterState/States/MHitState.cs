using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHitState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;

    private const float ParalTime = 0.5f;
    private float ParalCnt { get; set; }

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }

        ParalCnt = ParalTime;

        m_monster.HitAnim();
    }

    public void Proceed()
    {
        ParalCnt -= Time.deltaTime;

        if(ParalCnt <= 0)
        {
            m_monster.ApproachMonster();
        }
    }
}
