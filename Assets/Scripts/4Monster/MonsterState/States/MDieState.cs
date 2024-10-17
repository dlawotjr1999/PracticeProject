using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDieState : MonoBehaviour, IMonsterState
{
    private MonsterScript m_monster;

    public void ChangeTo(MonsterScript _monster)
    {
        if(!m_monster) { m_monster = _monster; }

        m_monster.DieAction();                            // 사망 설정

        StartCoroutine(AnimDone());
    }

    private IEnumerator AnimDone()
    {
        yield return new WaitForSeconds(m_monster.DieAnimLength);       // 애니메이션 끝나면
        PlayMgr.CreateMonsterEffect(EMonsterEffect.DISAPPEAR, transform.position);
        Destroy(gameObject);                                            // 없애기
    }

    public void Proceed()
    {

    }
}
