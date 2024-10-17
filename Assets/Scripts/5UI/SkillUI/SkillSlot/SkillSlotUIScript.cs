using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUIScript : MonoBehaviour
{
    private SkillSlotElm[] m_holders;
    public RectTransform[] HoldersTrans { get { return new RectTransform[3] { m_holders[0].m_rect, m_holders[1].m_rect, m_holders[2].m_rect }; } }

    public void SetSkillSlot(ESkillName _skill, int _idx)
    {
        if (_idx < 0 || _idx >= m_holders.Length) { Debug.LogError("스킬슬롯 범위 오류"); return; }

        Sprite skillIcon = PlayMgr.GetSkillIcon(_skill);
        m_holders[_idx].SetSkillSlot(skillIcon);
    }

    public void StartSkillCoolTime(int _idx, float _coolTime)
    {
        m_holders[_idx].StartCoolTime(_coolTime);
    }


    private void Awake()
    {
        m_holders = GetComponentsInChildren<SkillSlotElm>();
    }

    private void Start()
    {
        for(int i=0;i<m_holders.Length;i++) { m_holders[i].CoolTimeDone(); }
    }
}
