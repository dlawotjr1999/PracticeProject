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
        m_player.IdleAnim();                    // �ִϸ��̼�
    }

    public void Proceed()
    {
        if (Input.GetKeyDown(KeyCode.Space))    // �����̽��� ���� ��
        {
            m_player.UseDash();              // �뽬 ���·�
            return;
        }
        if (Input.GetMouseButtonDown(0) && !FunctionDefine.MouseOnUI)        // ���콺 ��Ŭ��
        {
            m_player.ChkNAttack();              // ��ų ���
            return;
        }
        if (Input.GetMouseButtonDown(1))        // ���콺 ��Ŭ��
        {
            if (m_player.IsShootingSkill)       // ���õ� ��ų X
            {
                m_player.MovePlayer();          // �����̱�
                return;
            }
            else
            {                                   // ���õ� ��ų o
                m_player.ResetSkill();          // ��ų ���� ����
            }
        }
    }
}
