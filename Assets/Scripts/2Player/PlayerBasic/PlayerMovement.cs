using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    private const float ArriveDist = 0.25f;                                 // 도착 판정 거리

    public float DashSpeed { get; } = 15;                                   // 대쉬 이동 속도
    public readonly float DashTime = 0.33f;                                 // 총 대쉬 시간

    public const int MaxStamina = 5;                                        // 최대 스태미나
    public const float m_staminaRestoreTime = 2;                           // 스태미나 회복 시간
    private float CurRestoreTime { get; set; } = 0;                         // 스태미나 회복 카운트 (UI용)


    public bool Arrived { get { if (m_navAgent.pathPending) return false; if (m_navAgent.remainingDistance >= ArriveDist) return false; return true; } }

    // 기본 이동
    public void StopMove()                          // 움직임 멈추기
    {
        m_navAgent.velocity = Vector3.zero;
        m_navAgent.ResetPath();
    }

    public void SetDestination()                    // 목적지 설정
    {
        Vector3 destination = GetMouseTarget();
        m_navAgent.SetDestination(destination);
        m_navAgent.velocity = (destination - transform.position).normalized * CurMoveSpeed;
    }
    public void StartMove()                         // 움직임 시작
    {
        SetDestination();
    }


    private void SetRotation()                      // 회전 설정
    {
        Vector2 forward = new(transform.position.z, transform.position.x);
        Vector2 steeringTarget = new(m_navAgent.steeringTarget.z, m_navAgent.steeringTarget.x);

        Vector2 dir = steeringTarget - forward;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = Vector3.up * angle;
    }

    // 대쉬 이동
    public void UseDash()           // 대쉬 사용 (스페이스바)
    {
        if (CurStamina == 0)
        {
            // 스태미나 없음 표시
            return;
        }
        DashPlayer();
        UseStamina();
    }

    public void DashMove()        // 대쉬 시 행동
    {
        transform.Translate(DashSpeed*Time.deltaTime*Vector3.forward);
    }

    private void UseStamina()                                   // 스태미나 사용
    {
        AdjStamina(-1);
        if (CurStamina == MaxStamina - 1)
        {
            StartCoroutine(RestoringStamina());
        }
        PlayMgr.SetStaminaPer(CurRestoreTime);
    }
    private IEnumerator RestoringStamina()                      // 스태미나 회복 코루틴
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
    private void RestoreStamina()                               // 스태미나 회복 액션
    {
        CurRestoreTime -= m_staminaRestoreTime;
        AdjStamina(1);
        if(CurStamina == MaxStamina)
        {
            CurRestoreTime = 0;
        }
    }
    private void AdjStamina(int _adj)                           // 스태미나 값 변경 시 (회복, 사용)
    {
        CurStamina += _adj;
        PlayMgr.SetCurStamina(CurStamina);
    }
}
