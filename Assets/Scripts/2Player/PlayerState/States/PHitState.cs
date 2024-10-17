using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHitState : MonoBehaviour, IPlayerState
{
    private PlayerController m_player;

    private const float ParalTime = 0.5f;                 // ���� �ð�
    private float ParalCnt { get; set; }                // ���� ī��Ʈ

    public void ChangeTo(PlayerController _player)
    {
        if(!m_player) { m_player = _player; }

        m_player.StopMove();
        ParalCnt = ParalTime;

        m_player.HitAnim();
    }

    public void Proceed()
    {
        ParalCnt -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))    // �����̽��� ���� ��
        {
            m_player.UseDash();                 // �뽬 ���·�
            return;
        }

        if (ParalCnt <= 0)
        {
            m_player.IdlePlayer();
        }
    }
}
