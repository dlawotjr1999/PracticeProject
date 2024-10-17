using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIdleState : MonoBehaviour, IPlayerState
{
    private PlayerController m_player;

    public void ChangeTo(PlayerController _player)
    {
        if(!m_player) { m_player = _player; }

        m_player.StopMove();
        m_player.IdleAnim();                    // 애니메이션
    }

    public void Proceed()
    {
        if (Input.GetKeyDown(KeyCode.Space))    // 스페이스바 누를 시
        {
            m_player.UseDash();              // 대쉬 상태로
            return;
        }
        if (Input.GetMouseButtonDown(0) && !FunctionDefine.MouseOnUI)        // 마우스 좌클릭
        {
            m_player.ChkNAttack();              // 스킬 사용
            return;
        }
        if (Input.GetMouseButtonDown(1))        // 마우스 우클릭
        {
            if (m_player.IsShootingSkill)       // 선택된 스킬 X
            {
                m_player.MovePlayer();          // 움직이기
                return;
            }
            else
            {                                   // 선택된 스킬 o
                m_player.ResetSkill();          // 스킬 선택 해제
            }
        }
    }
}
