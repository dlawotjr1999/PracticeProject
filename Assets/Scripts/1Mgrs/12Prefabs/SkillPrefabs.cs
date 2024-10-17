using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillPrefabs : MonoBehaviour            // ��ų ������
{
    [SerializeField]
    private GameObject[] m_skillPrefabs;
    public GameObject GetSkillPrefab(ESkillName _skill) { return m_skillPrefabs[(int)_skill]; }
}