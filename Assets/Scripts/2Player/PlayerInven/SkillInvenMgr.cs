using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvenMgr : MonoBehaviour
{
    private bool[] m_haveSkill = new bool[(int)ESkillName.LAST];        // 모든 스킬 보유 정보
    public bool HaveSkill(ESkillName _skill) { return m_haveSkill[(int)_skill]; }  // 스킬 보유 여부

    public void ObtainSkill(ESkillName _skill)                                                              // 스킬 획득
    { 
        if (HaveSkill(_skill)) { Debug.LogError("스킬 이미 소유중"); return; }
        m_haveSkill[(int)_skill] = true; 
    }





    private void InitInventory()            // 인벤토리 초기 설정
    {
        ObtainSkill(ESkillName.SHOOT_NONE); // (기본 공격 얻기)
    }

    private void Start()
    {
        InitInventory();
    }
}