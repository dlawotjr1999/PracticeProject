using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonsterValues
{
    public static MonsterInfo GetMonsterInfo(EMonsterName _monsterName)
    {
        if (m_monsterInfoMap.TryGetValue(_monsterName, out MonsterInfo info))
            return info;
        return null;
    }

    public static Dictionary<EMonsterName, MonsterInfo> m_monsterInfoMap = new()
    {
        // �Ϲ� ���� ���� (ü�� / ���ݷ� / �ιּӵ� / ���ټӵ� / ���ݹ��� / �������� / ���ݼӵ� / ġ��Ÿ�� / ȸ����)
        { EMonsterName.MIMIC, new MonsterInfo(EMonsterSize.MINI, EMonsterType.NON_MELEE,
            60,     10,     3.5f,   4,  4,      6,  2,  0.05f,  0.05f) },
        { EMonsterName.SPIDER, new MonsterInfo(EMonsterSize.MINI, EMonsterType.MELEE,
            40,     10,     4f,   5,  2,      8,  2,  0.05f,  0.05f) },
        { EMonsterName.BOOK, new MonsterInfo(EMonsterSize.MINI, EMonsterType.GROUP_MELEE,
            60,     10,     3.5f,   4,  2,      6,  2,  0.05f,  0.05f) },
        { EMonsterName.BROOM, new MonsterInfo(EMonsterSize.MINI, EMonsterType.RANGED,
            60,     10,     3.5f,   4,  4,      6,  2,  0.05f,  0.05f) },
        { EMonsterName.ELEMENTAL, new MonsterInfo(EMonsterSize.MINI, EMonsterType.GROUP_RANGED,
            40,     10,     2.5f,   4,  4,      6,  2,  0.05f,  0.05f) },

        // ���� ���� ����
        { EMonsterName.CAULDRON, new MonsterInfo(EMonsterSize.MID, EMonsterType.SUMMON_RANGED,
            200,     20,     2.5f,   4,  4,      6,  2,  0.05f,  0.05f) },
        { EMonsterName.GOLEM, new MonsterInfo(EMonsterSize.MID, EMonsterType.RUSH_RANGED,
            200,     20,     2.5f,   4,  4,      6,  2,  0.05f,  0.05f) },
        { EMonsterName.CLOCK, new MonsterInfo(EMonsterSize.MID, EMonsterType.RANGED,
            200,     20,     2.5f,   4,  4,      6,  2,  0.05f,  0.05f) },

        // ���� ���� ����
        { EMonsterName.GHOST, new MonsterInfo(EMonsterSize.BOSS, EMonsterType.SUMMON_RANGED,
            500,     20,     2.5f,   4,  4,      6,  2,  0.05f,  0.05f) },
    };
}