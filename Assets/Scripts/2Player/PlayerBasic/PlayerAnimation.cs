using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    private void ChangeAnim(string _trigger) { m_anim.SetTrigger(_trigger); }

    // 애니메이터에 추가하여 사용
    public void IdleAnim() { ChangeAnim("IDLE"); }
    public void WalkAnim() { ChangeAnim("WALK"); }
    public void AttackAnim() { ChangeAnim("ATTACK"); }
    public void AttackThrowAnim() { ChangeAnim("ATTACK_THROW"); }
    public void AttackAroundAnim() { ChangeAnim("ATTACK_AROUND"); }
    public void AttackSectorAnim() { ChangeAnim("ATTACK_SECTOR"); }
    public void DashAnim() { ChangeAnim("DASH"); }
    public void HitAnim() { ChangeAnim("HIT"); }
    public void DieAnim() { ChangeAnim("DIE"); }
}
