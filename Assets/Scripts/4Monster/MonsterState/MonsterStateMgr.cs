using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterState              // 상태 인터페이스
{
    public void ChangeTo(MonsterScript _monster);
    public void Proceed();
}

public class MonsterStateMgr                // 몬스터 상태 관리자
{
    private readonly MonsterScript m_monster;       // 가지고 있는 몬스터
    
    public IMonsterState CurState { get; set; }     // 현재 상태
    public void ChangeTo(IMonsterState _state) { CurState = _state; CurState.ChangeTo(m_monster); }     // 현재 상태 설정
    public void AttackDefense(IMonsterState _state) { CurState = _state; ((MAttackState)CurState).ChangeToDef(m_monster); } // 예외

    public MonsterStateMgr(MonsterScript _monster) { m_monster = _monster; }        // 생성자
}
