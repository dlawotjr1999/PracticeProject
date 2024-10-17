using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDieState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }

        m_monster.DieAction();                            // ��� ����

        StartCoroutine(AnimDone());
    }

    private IEnumerator AnimDone()
    {
        yield return new WaitForSeconds(m_monster.DieAnimLength);       // �ִϸ��̼� ������
        PlayMgr.CreateMonsterEffect(EMonsterEffect.DISAPPEAR, transform.position);
        Destroy(gameObject);                                            // ���ֱ�
    }

    public void Proceed()
    {

    }
}
