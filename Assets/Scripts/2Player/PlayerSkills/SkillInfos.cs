using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESkillName          // ��ų �̸�
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

public enum ESkillType          // ��ų Ÿ��
{
    SHOOT,
    THROW,
    RANGE_AROUND,
    RANGE_SECTOR,
    SUMMON,
    BUFF,
    LAST
}

public enum ESkillElement       // �Ӽ� Ÿ��
{
    NONE,
    FIRE,
    POISON,
    ELECTRIC,
    ICE,
    LAST
}

public abstract class PlayerSkill : MonoBehaviour            // ��ų ���� Ŭ����
{
    [SerializeField]
    private ESkillName m_name;                          // ��ų �̸�(�����࿡ ����)
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

public class SkillInfo      // ��ų ���� ������ ����
{
    // ���� ����
    public ESkillType Type { get; private set; }                // ��ų Ÿ��
    public ESkillElement Element { get; private set; }          // ��ų ���� �Ӽ�
    public float Damage { get; private set; }                   // ��ų ������
    public float Offset { get; private set; }                   // ��ų ���� ��ġ ������
    public float CreationHeight { get; private set; }           // ��ų ���� ����
    public float PreDelay { get; private set; }                 // ��������
    public float TotalDelay { get; private set; }               // �� ���� �ð�
    public float CoolTime { get; private set; }                 // ��Ÿ��

    public SkillInfo(ESkillType _type, ESkillElement _element, float _damage, float _offset, float _height, 
        float _preDelay, float _totalDelay, float coolTime)
    {
        Type = _type; Element = _element; Damage = _damage; Offset = _offset; CreationHeight = _height;
        PreDelay = _preDelay; TotalDelay = _totalDelay; CoolTime = coolTime;
    }
}

public class ShootingSkillInfo : SkillInfo
{
    public float Speed { get; private set; }                    // ��ų �̵� �ӵ�
    public float Range { get; private set; }                    // ��ų �ִ� �̵� �Ÿ�

    public ShootingSkillInfo(ESkillType _type, ESkillElement _element, float _damage, float _speed, float _range,
        float _offset, float _height, float _preDelay, float _totalDelay, float _coolTime)
        : base(_type, _element, _damage, _offset, _height, _preDelay, _totalDelay, _coolTime)
    { Speed = _speed; Range = _range;  }
}

public class ThrowSkillInfo : ShootingSkillInfo
{
    public float MaxHeight { get; private set; }                // ������ �ִ� ���� ����

    public ThrowSkillInfo(ESkillType _type, ESkillElement _element, float _damage, float _speed, float _range,
        float _offset, float _height, float _preDelay, float _totalDelay, float maxHeight, float _coolTime)
        : base(_type, _element, _damage, _speed, _range, _offset, _height, _preDelay, _totalDelay, _coolTime)
    { MaxHeight = maxHeight; }
}

public class AroundSkillInfo : SkillInfo
{
    public float Radius { get; private set; }                   // ��ų ������
    public float LastTime { get; private set; }                 // ���� �ð�
    public AroundSkillInfo(ESkillType _type, ESkillElement _element, float _damage, float _radius, 
        float _preDelay, float _totalDelay, float _lastTime, float _coolTime)
        : base(_type, _element, _damage, 0, 0, _preDelay, _totalDelay, _coolTime)
    { Radius = _radius; LastTime = _lastTime; }
}

public class SectorSkillInfo : AroundSkillInfo
{
    public float Degree { get; private set; }                   // ��ų ���̰�
    public SectorSkillInfo(ESkillType _type, ESkillElement _element, float _damage, float _radius, float _degree,
        float _preDelay, float _totalDelay, float _lastTime, float _coolTime)
        :base(_type, _element, _damage, _radius, _preDelay, _totalDelay, _lastTime, _coolTime)
    { Degree = _degree; }
}