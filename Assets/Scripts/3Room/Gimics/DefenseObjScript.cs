using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseObjScript : MonoBehaviour
{
    private DefenseRoom m_parent;           // 속한 방
    public void SetParent(DefenseRoom _parent) { m_parent = _parent; }

    private bool PlayerEntered { get; set; } = false;           // 플레이어가 접근했는지

    public const int MaxHP = 500;                               // 최대 체력
    private int CurHP { get; set; }                             // 현재 체력

    private float PlayerDist { get { return (PlayMgr.PlayerPos - transform.position).magnitude; } }     // 플레이어와 거리(접근 확인용)
    private const float NearDist = 10;                          // 접근 판정 거리

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

    private void StartDefense()                                 // 방어전 시작
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
