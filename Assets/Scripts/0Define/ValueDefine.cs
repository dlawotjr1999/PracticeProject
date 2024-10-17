using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 내 공통적으로 쓰이는 값
public static class ValueDefine
{
    public readonly static int AimLayerIdx = (1 << LayerMask.NameToLayer("Aim"));               // 조준점 레이어 인덱스
    public readonly static int HideWallLayerIdx = (1 << LayerMask.NameToLayer("HideWall"));     // 숨김 가능 벽 레이어

    public const string PlayerTag = "Player";
    public const string WallTag = "Wall";
    public const string MonsterTag = "Monster";
    public const string PlayerSkillTag = "PlayerSkill";
    public const string MonsterSkillTag = "MonsterSkill";
    public const string HealingTag = "Healing";
    public const string SpikeTrapTag = "SpikeTrap";
    public const string DefenseObjTag = "DefenseObj";
    public const string CollideObjTag = "CollideObj";

    public const KeyCode InteractKey = KeyCode.F;
}
