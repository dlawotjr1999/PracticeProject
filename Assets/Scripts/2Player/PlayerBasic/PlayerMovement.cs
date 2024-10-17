using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    private const float ArriveDist = 0.25f;                                 // ���� ���� �Ÿ�

    public float DashSpeed { get; } = 15;                                   // �뽬 �̵� �ӵ�
    public readonly float DashTime = 0.33f;                                 // �� �뽬 �ð�

    public const int MaxStamina = 5;                                        // �ִ� ���¹̳�
    public const float m_staminaRestoreTime = 2;                           // ���¹̳� ȸ�� �ð�
    private float CurRestoreTime { get; set; } = 0;                         // ���¹̳� ȸ�� ī��Ʈ (UI��)


    public bool Arrived { get { if (m_navAgent.pathPending) return false; if (m_navAgent.remainingDistance >= ArriveDist) return false; return true; } }

    // �⺻ �̵�
    public void StopMove()                          // ������ ���߱�
    {
        m_navAgent.velocity = Vector3.zero;
        m_navAgent.ResetPath();
    }

    public void SetDestination()                    // ������ ����
    {
        Vector3 destination = GetMouseTarget();
        m_navAgent.SetDestination(destination);
        m_navAgent.velocity = (destination - transform.position).normalized * CurMoveSpeed;
    }
    public void StartMove()                         // ������ ����
    {
        SetDestination();
    }


    private void SetRotation()                      // ȸ�� ����
    {
        Vector2 forward = new(transform.position.z, transform.position.x);
        Vector2 steeringTarget = new(m_navAgent.steeringTarget.z, m_navAgent.steeringTarget.x);

        Vector2 dir = steeringTarget - forward;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = Vector3.up * angle;
    }

    // �뽬 �̵�
    public void UseDash()           // �뽬 ��� (�����̽���)
    {
        if (CurStamina == 0)
        {
            // ���¹̳� ���� ǥ��
            return;
        }
        DashPlayer();
        UseStamina();
    }

    public void DashMove()        // �뽬 �� �ൿ
    {
        transform.Translate(DashSpeed*Time.deltaTime*Vector3.forward);
    }

    private void UseStamina()                                   // ���¹̳� ���
    {
        AdjStamina(-1);
        if (CurStamina == MaxStamina - 1)
        {
            StartCoroutine(RestoringStamina());
        }
        PlayMgr.SetStaminaPer(CurRestoreTime);
    }
    private IEnumerator RestoringStamina()                      // ���¹̳� ȸ�� �ڷ�ƾ
    {
        CurRestoreTime = 0;
        while (CurStamina < MaxStamina)
        {
            CurRestoreTime += Time.deltaTime;
            PlayMgr.SetStaminaPer(CurRestoreTime);
            if(CurRestoreTime >= m_staminaRestoreTime)
            {
                RestoreStamina();
            }
            yield return null;
        }
    }
    private void RestoreStamina()                               // ���¹̳� ȸ�� �׼�
    {
        CurRestoreTime -= m_staminaRestoreTime;
        AdjStamina(1);
        if(CurStamina == MaxStamina)
        {
            CurRestoreTime = 0;
        }
    }
    private void AdjStamina(int _adj)                           // ���¹̳� �� ���� �� (ȸ��, ���)
    {
        CurStamina += _adj;
        PlayMgr.SetCurStamina(CurStamina);
    }
}
