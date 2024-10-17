using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHitState : MonoBehaviour, IPlayerState
{
    private PlayerController m_player;

    private const float ParalTime = 0.5f;                 // 경직 시간
    private float ParalCnt { get; set; }                // ㄴ의 카운트

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

        if (Input.GetKeyDown(KeyCode.Space))    // 스페이스바 누를 시
        {
            m_player.UseDash();                 // 대쉬 상태로
            return;
        }

        if (ParalCnt <= 0)
        {
            m_player.IdlePlayer();
        }
    }
}
