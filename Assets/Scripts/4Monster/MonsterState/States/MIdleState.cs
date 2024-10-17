using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIdleState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;

    private const float m_idleTime = 1;                 // IDLE�� �ִ� �ð�
    private float RoamingCnt { get; set; }

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }

        m_monster.StopMove();                           // ������ ���߱�
        RoamingCnt = m_idleTime;                        // �ιֱ��� �ð� üũ

        m_monster.IdleAnim();
    }

    public void Proceed()
    {
        RoamingCnt -= Time.deltaTime;

        if (RoamingCnt <= 0)                            // �ð��� �Ǹ�
        {
            m_monster.RoamingMonster();                 // �ι�����
        }
    }
}
