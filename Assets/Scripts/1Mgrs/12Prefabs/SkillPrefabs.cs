using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillPrefabs : MonoBehaviour            // ½ºÅ³ ÇÁ¸®Æà
{
    [SerializeField]
    private GameObject[] m_skillPrefabs;
    public GameObject GetSkillPrefab(ESkillName _skill) { return m_skillPrefabs[(int)_skill]; }
}