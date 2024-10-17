using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPrefabs : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_monsterPrefabs = new GameObject[(int)EMonsterName.LAST];
    [SerializeField]
    private GameObject m_groupMonsterPrefab;

    public GameObject GetMonsterPrefab(EMonsterName _monster) { return m_monsterPrefabs[(int)_monster]; }
    public GameObject GroupMonsterPrefab { get { return m_groupMonsterPrefab; } }
}