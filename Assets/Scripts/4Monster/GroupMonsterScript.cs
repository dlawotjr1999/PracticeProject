using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMonsterScript : MonoBehaviour
{
    private List<MonsterScript> m_monsterList = new();
    public List<MonsterScript> MonsterList { get { return m_monsterList; } }

    public void AddMonster(MonsterScript _monster)
    {
        m_monsterList.Add(_monster);
    }

    public void MonsterDead(MonsterScript _monster)
    {
        m_monsterList.Remove(_monster);
        if (m_monsterList.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    public void DetectPlayer()
    {
        foreach (MonsterScript monster in m_monsterList)
        {
            monster.ApproachMonster();
        }
    }
}
