using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMonsterSize        // ?????? ????
{
    MINI,
    MID,
    BOSS,
    GROUP,
    LAST
}

public enum EMonsterType        // AI ????
{
    NON_MELEE,         // ?????? ????
    MELEE,             // ???? ????
    GROUP_MELEE,       // ???? ???? ????
    RANGED,            // ???? ??????
    GROUP_RANGED,      // ???? ???? ??????
    RUSH_RANGED,       // ???? ???? ??????
    SUMMON_RANGED,     // ???? ???? ??????
    LAST
}

public enum EMonsterName        // ?????? ????
{
    MIMIC,      // ???? ????
    SPIDER,
    BOOK,
    BROOM,
    ELEMENTAL,
    CAULDRON,
    GOLEM,
    CLOCK,
    GHOST,
    LAST
}

public class MonsterInfo
{
    public EMonsterSize Size { get; private set; }      // ????
    public EMonsterType Type { get; private set; }      // AI
    public int MaxHP { get; private set; }              // ???? HP
    public float Damage { get; private set; }           // ??????
    public float RoamingSpeed { get; private set; }     // ???? ????
    public float ApproachSpeed { get; private set; }    // ???? ????
    public float AttackRange { get; private set; }      // ???? ????
    public float DetectRange { get; private set; }      // ???? ????
    public float AttackSpeed { get; private set; }      // ???? ????
    public float CriticalRate { get; private set; }     // ????????
    public float EvasionRate { get; private set; }      // ??????

    public MonsterInfo(EMonsterSize _size, EMonsterType _type, int _maxHP, float _damage, float _roaming, float _approach, float _atkRange, 
        float _dtcRange, float _atkSpeed, float _critRate, float _evasionRate) 
    { Size = _size; Type = _type; MaxHP = _maxHP; Damage = _damage; RoamingSpeed = _roaming; ApproachSpeed = _approach; AttackRange = _atkRange;
        DetectRange = _dtcRange; AttackSpeed = _atkSpeed; CriticalRate = _critRate; EvasionRate = _evasionRate; }
}