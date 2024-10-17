using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomMonster : MonsterScript
{
    [SerializeField]
    private GameObject m_attackPref;

    private const float AttackOffset = 1;
    private const float AttackMoveSpeed = 8;

    private const float PreDelay = 0.33f;
    private const float AnimLength = 1.25f;

    public override IEnumerator CreateAttack(Vector3 _target)
    {
        yield return new WaitForSeconds(PreDelay);
        Vector3 dir = (_target - transform.position).normalized;
        GameObject attack = Instantiate(m_attackPref, transform.position + dir * AttackOffset, Quaternion.identity);
        attack.GetComponent<Rigidbody>().velocity = dir * AttackMoveSpeed;
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
        m_name = EMonsterName.BROOM;
        DieAnimLength = 0.833f;
        base.Awake();
    }
}
