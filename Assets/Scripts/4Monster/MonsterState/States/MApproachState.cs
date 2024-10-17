using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MApproachState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;                            // ?????? ???? ??????

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }


        m_monster.SetDestination(PlayMgr.PlayerPos);            // ???????? ????????
        m_monster.SetSpeed(false);                              // ???? ???? ????

        m_monster.MoveAnim();
    }

    private void ChkCanAttack()        // ???? ???? ???? ????
    {
        if (!m_monster.CanAttack || m_monster.AttackCoolCnt > 0)
            return;

        m_monster.AttackMonster();              // ????????
    }

    public void Proceed()
    {
        if (!m_monster.Detecting)
        {
            m_monster.IdleMonster();
        }
        ChkCanAttack();

        m_monster.SetDestination(PlayMgr.PlayerPos);            // ???????? ????????
    }
}
