using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState                   // ���� �������̽�
{
    public void ChangeTo(PlayerController _player);         // ���·� ��ȯ �� �� ��
    public void Proceed();                                  // �ش� ���� �� ������Ʈ ����
}

public class PlayerStateMgr                     // ���� ������ Ŭ����
{
    private readonly PlayerController m_player; // �÷��̾� ��Ʈ�ѷ�

    public IPlayerState CurState { get; private set; }      // ���� ����
    public void ChangeToState(IPlayerState _state) { CurState = _state; CurState.ChangeTo(m_player); }      // ���� ���� ����

    public PlayerStateMgr(PlayerController _player) { m_player = _player; } // ������
}
