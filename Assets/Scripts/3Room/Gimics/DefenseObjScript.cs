using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseObjScript : MonoBehaviour
{
    private DefenseRoom m_parent;           // ���� ��
    public void SetParent(DefenseRoom _parent) { m_parent = _parent; }

    private bool PlayerEntered { get; set; } = false;           // �÷��̾ �����ߴ���

    public const int MaxHP = 500;                               // �ִ� ü��
    private int CurHP { get; set; }                             // ���� ü��

    private float PlayerDist { get { return (PlayMgr.PlayerPos - transform.position).magnitude; } }     // �÷��̾�� �Ÿ�(���� Ȯ�ο�)
    private const float NearDist = 10;                          // ���� ���� �Ÿ�

    private void GetDamage(float _damage)
    {
        if(m_parent.MissionFailed) return;

        CurHP -= (int)_damage;
        if (CurHP <= 0)
        {
            m_parent.DefenseFail();
            return;
        }
        m_parent.SetHP(CurHP);
    }

    private void StartDefense()                                 // ����� ����
    {
        PlayerEntered = true;
        m_parent.StartDefense();

        CurHP = MaxHP;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.tag == ValueDefine.MonsterSkillTag)
        {
            GetDamage(10);
        }
    }

    private void Update()
    {
        if (!PlayerEntered && PlayerDist <= NearDist)
        {
            StartDefense();
        }
    }
}
