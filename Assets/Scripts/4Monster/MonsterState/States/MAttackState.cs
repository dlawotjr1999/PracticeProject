using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAttackState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;

    private float m_attackDelay;
    private float DelayCnt { get; set; }

    private bool AttackingDefense { get; set; }                 // ��� ��� �����ϴ���

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }

        m_monster.StopMove();                                   // ������ ���߱�
        m_monster.AttackPlayer();                               // ������ ���� �Լ� ����
        m_attackDelay = m_monster.AttackSpeed;                  // ���� �ӵ� ����
        m_monster.AttackCoolCnt = m_monster.AttackSpeed;        // ���� ���� ���� ��Ÿ�� ����

        DelayCnt = m_attackDelay;                               // ī��Ʈ ����
        AttackingDefense = false;

        m_monster.AttackAnim();
    }

    public void ChangeToDef(MonsterScript _monster)       // ����� ����
    {
        if (!m_monster) { m_monster = _monster; }

        m_monster.StopMove();                                   // ������ ���߱�
        m_monster.AttackDefenseObj();                           // ������ ���� �Լ� ����
        m_attackDelay = m_monster.AttackSpeed;                  // ���� �ӵ� ����
        m_monster.AttackCoolCnt = m_monster.AttackSpeed;        // ���� ���� ���� ��Ÿ�� ����

        DelayCnt = m_attackDelay;                               // ī��Ʈ ����
        AttackingDefense = true;

        m_monster.AttackAnim();
    }

    public void Proceed()
    {
        if(!AttackingDefense)
           m_monster.WatchPlayer();            // �÷��̾� �Ĵٺ���
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
