using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterScript
{
    private const float ArangeTime = 5;         // ���� �Ϸ���� �ð�
    private bool Aranged { get; set; }          // ���� �ƴ��� ����

    private void AttackAction(Vector3 _target)           // ���� �ൿ
    {
        StartCoroutine(CreateAttack(_target));
        StartCoroutine(DoneAttack());
    }

    public void AttackPlayer()                          // �÷��̾� ����
    {
        AttackAction(PlayMgr.PlayerPos);
    }

    public void AttackDefenseObj()                      // ��� ��� ����
    {
        AttackAction(PlayMgr.DefenseObjPos);
    }

    public virtual IEnumerator CreateAttack(Vector3 _target)    // ���� ���� �Լ�
    {
        yield return null;
    }

    public virtual IEnumerator DoneAttack()                     // ���� ���� �Լ�
    {
        yield return null;
    }

    private void DisplayDamage(float _damage)               // ������ UI �����ֱ�
    {
        
    }

    public virtual void DieAction()
    {
        SetDead();
        StopMove();
        DieAnim();
    }

    public virtual void GetDamage(float _damage)
    {
        if (IsDead) return;

        CurHP -= (int)_damage;

        if(CurHP <= 0)
        {
            DieMonster();
            return;
        }
        else if(CurState != m_hit)
        {
            HitMonster();
        }
        if (Aranging)           // ���� ���̸�
        {
            ChkAranged();       // ü�� Ȯ��
        }
        m_hpBar.SetHP(CurHP);
        DisplayDamage(_damage);
    }

    public virtual void ChkAranged()                            // ���� �ƴ��� Ȯ��
    {
        if (Aranged) return;

        ArangeRoom arange = m_parent as ArangeRoom;
        if(arange == null) { Debug.LogError("���� �� �ƴ� ������ ���� ��"); return; }

        float critPer = arange.HPPer;           // ���� ���� ü�� %
        float hpPer = CurHP / MaxHP;            // ���� ü�� %
        if (hpPer <= critPer)                   // ���غ��� ������
        {
            StartCoroutine(ArangeCorutine(arange));     // 5�� ���
            m_hpBar.ArangeOn();                         // ü�¹� ����
            Aranged = true;                             // ������
        }
    }

    private IEnumerator ArangeCorutine(ArangeRoom _room)        // 5�� ��� �ڷ�ƾ
    {
        yield return new WaitForSeconds(ArangeTime);
        if(!IsDead)     // ��� ������
        {
            PlayMgr.CreateMonsterEffect(EMonsterEffect.ARANGED, transform.position);
            _room.MonsterAranged(this);         // ������ ����
            m_parent.MonsterDead(this);
            Destroy(m_hpBar.gameObject);
            Destroy(gameObject);
        }
    }
}
