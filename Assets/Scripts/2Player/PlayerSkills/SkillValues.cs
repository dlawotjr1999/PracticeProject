using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillValues     // ��ų ���� �����ϴ� ��
{
    public const float ThrowDist = 15;

    public static SkillInfo GetSkillInfo(ESkillName _skillName)             // ��ų �⺻ ����
    {
        if(m_skillInfoMap.TryGetValue(_skillName, out SkillInfo info))
        {
            return info;
        }
        return null;
    }
    public static float GetSkillDamage(ESkillName _skillName)               // ��ų ������ x �÷��̾� ������ ����
    {
        if (m_skillInfoMap.TryGetValue(_skillName, out SkillInfo info))
        {
            return info.Damage * PlayMgr.PlayerDamage;
        }
        return 0;
    }

    public static Dictionary<ESkillName, SkillInfo> m_skillInfoMap = new()
    {
        // �߻��� ��ų (������ / �̵��ӵ� / �̵��Ÿ� / �����Ÿ� / �������� / �������� / �ѵ����� / ��Ÿ��)
        { ESkillName.SHOOT_NONE, new ShootingSkillInfo(ESkillType.SHOOT, ESkillElement.NONE,
            20, 13, 7, 0.5f, 1f, 0.25f, 0.5f, 0.5f) },
        { ESkillName.SHOOT_FIRE, new ShootingSkillInfo(ESkillType.SHOOT, ESkillElement.FIRE,
            35, 10, 7, 0.5f, 1f, 0.25f, 0.5f, 0.5f) },
        { ESkillName.SHOOT_POISON, new ShootingSkillInfo(ESkillType.SHOOT, ESkillElement.POISON,
            30, 10, 9, 0.5f, 1f, 0.25f, 0.5f, 0.5f) },
        { ESkillName.SHOOT_ELECTRIC, new ShootingSkillInfo(ESkillType.SHOOT, ESkillElement.ELECTRIC,
            30, 17, 7, 0.5f, 1f, 0.25f, 0.5f, 0.5f) },
        { ESkillName.SHOOT_ICE, new ShootingSkillInfo(ESkillType.SHOOT, ESkillElement.ICE,
            30, 10, 9, 0.5f, 1f, 0.25f, 0.5f, 0.5f) },


        // ��ô�� ��ų (������ / �̵��ӵ� / �̵��Ÿ� / �����Ÿ� / �������� / �������� / �ѵ����� / �ִ���� / ��Ÿ��)
        { ESkillName.THROW_NONE, new ThrowSkillInfo(ESkillType.THROW, ESkillElement.NONE,
            40, 13, ThrowDist, 1, 2, 0.5f, 1, 5, 3) },
        { ESkillName.THROW_FIRE, new ThrowSkillInfo(ESkillType.THROW, ESkillElement.FIRE,
            45, 13, ThrowDist, 1, 2, 0.5f, 1, 5, 3) },
        { ESkillName.THROW_POISON, new ThrowSkillInfo(ESkillType.THROW, ESkillElement.POISON,
            45, 13, ThrowDist, 1, 2, 0.5f, 1, 6, 3) },
        { ESkillName.THROW_ELECTRIC, new ThrowSkillInfo(ESkillType.THROW, ESkillElement.ELECTRIC,
            45, 17, ThrowDist, 1, 2, 0.3f, 1, 4, 3) },
        { ESkillName.THROW_ICE, new ThrowSkillInfo(ESkillType.THROW, ESkillElement.ICE,
            45, 13, ThrowDist, 1, 2, 0.5f, 1, 6, 3) },

        
        // ����_�ֺ��� ��ų (������ / ������ / �������� / �ѵ����� / ���ӽð� / ��Ÿ��)
        { ESkillName.AROUND_NONE, new AroundSkillInfo(ESkillType.RANGE_AROUND, ESkillElement.NONE,
            50, 7, 0.5f, 1, 2, 10) },
        { ESkillName.AROUND_POISON, new AroundSkillInfo(ESkillType.RANGE_AROUND, ESkillElement.POISON,
            50, 9, 0.5f, 1, 4, 10) },
        { ESkillName.AROUND_ELECTRIC, new AroundSkillInfo(ESkillType.RANGE_AROUND, ESkillElement.ELECTRIC,
            50, 7, 0.5f, 1, 2, 10) },
        { ESkillName.AROUND_ICE, new AroundSkillInfo(ESkillType.RANGE_AROUND, ESkillElement.ICE,
            50, 8, 0.5f, 1, 2, 10) },


        // ����_������ ��ų (������ / ������ / ���̰� / �������� / �ѵ����� / ���ӽð� / ��Ÿ��)
        { ESkillName.SECTOR_FIRE, new SectorSkillInfo(ESkillType.RANGE_SECTOR, ESkillElement.FIRE,
            50, 9, 45, 0.5f, 1, 2, 10) },

        // ��ȯ�� ��ų (������ / ��Ÿ��)
        { ESkillName.SUMMON, new SkillInfo(ESkillType.SUMMON,ESkillElement.NONE,
            10, 0, 0, 0, 0, 15) },
        { ESkillName.BUFF, new SkillInfo(ESkillType.BUFF,ESkillElement.NONE,0,0,0,0,0,25) }
    };
}
