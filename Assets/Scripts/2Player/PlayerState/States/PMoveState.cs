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

        m_player.StartMove();                   // ������ ����
        m_player.WalkAnim();                    // �ִϸ��̼�
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

        if (Input.GetKeyDown(KeyCode.Space))    // �����̽���
        {
            m_player.StopMove();                // ������ ���߰�
            m_player.UseDash();                 // �뽬
            return;
        }
        if (Input.GetMouseButtonDown(0) && !FunctionDefine.MouseOnUI)        // ���콺 ��Ŭ��
        {
            m_player.StopMove();                // ������ ���߰�
            m_player.ChkNAttack();              // ��ų ���
        }
        if (Input.GetMouseButton(1))            // ���콺 ��Ŭ�� ��
        {
            if (m_player.IsShootingSkill)       // ���õ� ��ų X
            {
                m_player.SetDestination();      // �����̱�
                return;
            }
            else
            {                                   // ���õ� ��ų o
                m_player.ResetSkill();          // ��ų ���� ����
            }
        }
    }
}
