using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDieState : MonoBehaviour, IPlayerState
{
    private PlayerController m_player;

    public void ChangeTo(PlayerController _player)
    {
        if (!m_player) { m_player = _player; }

        m_player.SetDead();
        m_player.StopMove();

        m_player.DieAnim();     // 애니메이션
    }

    public void Proceed()
    {

    }
}