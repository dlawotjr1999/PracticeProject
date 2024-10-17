using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRoamingState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;                        // ������ �ִ� ����

    private Vector3 m_destination;                          // �ι� ������

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }

        m_destination = new(Random.Range(8f, -8f), 0, Random.Range(8f, -8f));           // ���� ������ ����
        m_monster.SetDestination(transform.position + m_destination);                   // �׺� ��� ����
        m_monster.SetSpeed(true);                                                       // �ι� �ӵ� ����

        m_monster.MoveAnim();
    }

    private void ArriveChk()            // �����ߴ��� Ȯ��
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
