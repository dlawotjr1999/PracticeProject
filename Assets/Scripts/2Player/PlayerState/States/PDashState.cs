using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class PDashState : MonoBehaviour, IPlayerState
{
    private PlayerController m_player;

    private float DashTime { get; set; }
    private float TimeCnt { get; set; }

    public void ChangeTo(PlayerController _player)
    {
        if (!m_player) { m_player = _player; }

        m_player.StopMove();

        // 대쉬 정보 설정
        Vector3 dashDir = m_player.SkillAim.normalized;
        m_player.SetRotation(new(dashDir.x, dashDir.z));
        DashTime = m_player.DashTime;

        TimeCnt = DashTime;

        m_player.DashAnim();            // 애니메이션
    }

    private void OnCollisionStay(Collision _collision)
    {
        if (_collision.gameObject.CompareTag(ValueDefine.WallTag))
        {
            if(!m_player) { return; }

            Vector3 targetPos = _collision.transform.position;
            Vector3 player = m_player.transform.position;
            Vector3 gap = targetPos - player;
            Vector3 adj = new(gap.x, 0, gap.z);
            Vector3 adjFlat = adj.normalized * 0.1f;
            player -= adjFlat;
            m_player.transform.position = player;
            m_player.IdlePlayer();
        }
    }

    public void Proceed()
    {
        m_player.DashMove();
        TimeCnt -= Time.deltaTime;
        if (TimeCnt <= 0)
        {
            m_player.IdlePlayer();
        }
    }
}