using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState                   // 상태 인터페이스
{
    public void ChangeTo(PlayerController _player);         // 상태로 변환 시 할 일
    public void Proceed();                                  // 해당 상태 시 업데이트 내용
}

public class PlayerStateMgr                     // 상태 관리자 클래스
{
    private readonly PlayerController m_player; // 플레이어 컨트롤러

    public IPlayerState CurState { get; private set; }      // 현재 상태
    public void ChangeToState(IPlayerState _state) { CurState = _state; CurState.ChangeTo(m_player); }      // 현재 상태 설정

    public PlayerStateMgr(PlayerController _player) { m_player = _player; } // 생성자
}
