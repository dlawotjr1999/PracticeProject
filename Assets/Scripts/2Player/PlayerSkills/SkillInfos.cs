using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESkillName          // 스킬 이름
{
    SHOOT_NONE,
    SHOOT_FIRE,
    SHOOT_POISON,
    SHOOT_ELECTRIC,
    SHOOT_ICE,
    
    THROW_NONE,
    THROW_FIRE,
    THROW_POISON,
    THROW_ELECTRIC,
    THROW_ICE,

    AROUND_NONE,
    AROUND_POISON,
    AROUND_ELECTRIC,
    AROUND_ICE,
    SECTOR_FIRE,

    BUFF,
    SUMMON,
    LAST
}

public enum ESkillType          // 스킬 타입
{
    SHOOT,
    THROW,
    RANGE_AROUND,
    RANGE_SECTOR,
    SUMMON,
    BUFF,
    LAST
}

public enum ESkillElement       // 속성 타입
{
    NONE,
    FIRE,
    POISON,
    ELECTRIC,
    ICE,
    LAST
}

public abstract class PlayerSkill : MonoBehaviour            // 스킬 가상 클래스
{
    [SerializeField]
    private ESkillName m_name;                          // 스킬 이름(프리펍에 설정)
    public ESkillName SkillName { get { return m_name; } }

    protected ESkillType m_type;
    public ESkillElement Element { get; protected set; }
    public float Damage { get; protected set; }
    public int Critical { get; private set; }
    public void SetCritical(int _critical) { Critical = _critical; }

    protected Vector3 m_aim;

    public virtual void SetAim(Vector3 _aim)
    {
        m_aim = _aim.normalized;
    }

    public virtual void SetSkillInfo(SkillInfo _info)
    {
        m_type = _info.Type;
        Element = _info.Element;
        Damage = _info.Damage * PlayMgr.PlayerDamage;
    }

    public virtual void SetTarget(Vector3 _target)
    {

    }
}

public class SkillInfo      // 스킬 정보 데이터 형식
{
    // 공통 정보
    public ESkillType Type { get; private set; }                // 스킬 타입
    public ESkillElement Element { get; private set; }          // 스킬 원소 속성
    public float Damage { get; private set; }                   // 스킬 데미지
    public float Offset { get; private set; }                   // 스킬 생성 위치 오프셋
    public float CreationHeight { get; private set; }           // 스킬 생성 높이
    public float PreDelay { get; private set; }                 // 선딜레이
    public float TotalDelay { get; private set; }               // 총 시전 시간
    public float CoolTime { get; private set; }                 // 쿨타임

    public SkillInfo(ESkillType _type, ESkillElement _element, float _damage, float _offset, float _height, 
        float _preDelay, float _totalDelay, float coolTime)
    {
        Type = _type; Element = _element; Damage = _damage; Offset = _offset; CreationHeight = _height;
        PreDelay = _preDelay; TotalDelay = _totalDelay; CoolTime = coolTime;
    }
}

public class ShootingSkillInfo : SkillInfo
{
    public float Speed { get; private set; }                    // 스킬 이동 속도
    public float Range { get; private set; }                    // 스킬 최대 이동 거리

    public ShootingSkillInfo(ESkillType _type, ESkillElement _element, float _damage, float _speed, float _range,
        float _offset, float _height, float _preDelay, float _totalDelay, float _coolTime)
        : base(_type, _element, _damage, _offset, _height, _preDelay, _totalDelay, _coolTime)
    { Speed = _speed; Range = _range;  }
}

public class ThrowSkillInfo : ShootingSkillInfo
{
    public float MaxHeight { get; private set; }                // 포물선 최대 도달 높이

    public ThrowSkillInfo(ESkillType _type, ESkillElement _element, float _damage, float _speed, float _range,
        float _offset, float _height, float _preDelay, float _totalDelay, float maxHeight, float _coolTime)
        : base(_type, _element, _damage, _speed, _range, _offset, _height, _preDelay, _totalDelay, _coolTime)
    { MaxHeight = maxHeight; }
}

public class AroundSkillInfo : SkillInfo
{
    public float Radius { get; private set; }                   // 스킬 반지름
    public float LastTime { get; private set; }                 // 유지 시간
    public AroundSkillInfo(ESkillType _type, ESkillElement _element, float _damage, float _radius, 
        float _preDelay, float _totalDelay, float _lastTime, float _coolTime)
        : base(_type, _element, _damage, 0, 0, _preDelay, _totalDelay, _coolTime)
    { Radius = _radius; LastTime = _lastTime; }
}

public class SectorSkillInfo : AroundSkillInfo
{
    public float Degree { get; private set; }                   // 스킬 사이각
    public SectorSkillInfo(ESkillType _type, ESkillElement _element, float _damage, float _radius, float _degree,
        float _preDelay, float _totalDelay, float _lastTime, float _coolTime)
        :base(_type, _element, _damage, _radius, _preDelay, _totalDelay, _lastTime, _coolTime)
    { Degree = _degree; }
}