using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDefenseState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;
    private const float DefenseRadius = 3.5f;

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }

        m_monster.MoveAnim();

        m_monster.SetDestination(PlayMgr.DefenseObjPos);
    }

    public void Proceed()
    {
        Vector3 defense = PlayMgr.DefenseObjPos;
        float dist = (transform.position - defense).magnitude;
        if (dist <= DefenseRadius + m_monster.AttackRange)
        {
            m_monster.AttackDefenseMonster();
        }
    }
}