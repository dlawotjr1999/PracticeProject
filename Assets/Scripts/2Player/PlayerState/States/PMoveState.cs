using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMoveState : MonoBehaviour, IPlayerState
{
    private PlayerController m_player;

    private const float ArriveDist = 0.25f;

    public void ChangeTo(PlayerController _player)
    {
        if (!m_player) { m_player = _player; }

        m_player.StartMove();                   // 움직임 시작
        m_player.WalkAnim();                    // 애니메이션
    }

    private void ArriveChk()
    {
        if (!m_player.Arrived)
            return;

        m_player.IdlePlayer();
    }

    public void Proceed()
    {
        ArriveChk();

        if (Input.GetKeyDown(KeyCode.Space))    // 스페이스바
        {
            m_player.StopMove();                // 움직임 멈추고
            m_player.UseDash();                 // 대쉬
            return;
        }
        if (Input.GetMouseButtonDown(0) && !FunctionDefine.MouseOnUI)        // 마우스 좌클릭
        {
            m_player.StopMove();                // 움직임 멈추고
            m_player.ChkNAttack();              // 스킬 사용
        }
        if (Input.GetMouseButton(1))            // 마우스 우클릭 중
        {
            if (m_player.IsShootingSkill)       // 선택된 스킬 X
            {
                m_player.SetDestination();      // 움직이기
                return;
            }
            else
            {                                   // 선택된 스킬 o
                m_player.ResetSkill();          // 스킬 선택 해제
            }
        }
    }
}
