using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicMonster : MonsterScript
{
    [SerializeField]
    private GameObject m_attackPref;

    private const float AttackOffset = 1.5f;   // 공격 생성 오프셋
    private const float AttackHeight = 0.75f;
    private const float PreDelay = 0.5f;       // 공격 모션 선딜레이
    private const float AnimLength = 1;
    // DieAnimLength

    public override IEnumerator CreateAttack(Vector3 _target)
    {
        yield return new WaitForSeconds(PreDelay);
        Vector3 dir = (_target - transform.position).normalized;
        GameObject attack = Instantiate(m_attackPref,
            transform.position + dir * AttackOffset + Vector3.up * AttackHeight, Quaternion.identity);
        Vector3 deg = new(0, FunctionDefine.VecToDeg(dir), 0);
        attack.transform.eulerAngles = deg;
        transform.eulerAngles = deg;
    }

    public override IEnumerator DoneAttack()
    {
        yield return new WaitForSeconds(AnimLength);
        IdleAnim();
    }

    public override void Awake()
    {
        m_name = EMonsterName.MIMIC;
        base.Awake();
    }
}