using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillInvenElm : MonoBehaviour       // 공사중
{
    private SkillInvenUIScript m_parent;                // 소속된 UI
    public void SetParent(SkillInvenUIScript _parent) { m_parent = _parent; }

    private RectTransform m_rect;
    private EventTrigger m_eventTrigger;

    public ESkillName CurSkill { get; private set; } = ESkillName.LAST;     // 해당하는 스킬
    private Image m_skillIcon;                                              // 스킬 아이콘 이미지

    public bool Selected { get; set; }

    public void SetSelect(bool _select)
    {
        if (CurSkill == ESkillName.LAST) { return; }

        if (_select)
        {
            m_skillIcon.color = Color.red;
            Selected = true;
        }
        else
        {
            m_skillIcon.color = Color.white;
            Selected = false;
        }
    }

    public void SetSkill(ESkillName _skill)                     // 해당하는 스킬 표시
    {
        CurSkill = _skill;
        Sprite skillIcon = PlayMgr.GetSkillIcon(CurSkill);
        m_skillIcon.sprite = skillIcon;
        m_skillIcon.color = Color.white;
    }
    public void CreateCarrier(PointerEventData _data)               // 슬롯 등록에 사용할 오브젝트 생성
    {
        if (Selected) { return; }
        GameObject carrier = Instantiate(PlayMgr.SkillCarrierPrefab, PlayMgr.ControlCanvasTrans);
        carrier.GetComponent<RectTransform>().position = m_rect.position;               // 현재 위치로
        carrier.GetComponent<SkillCarrier>().SetCarrier(CurSkill);                      // 운반할 스킬 설정
    }


    private void SetComps()
    {
        m_rect = GetComponent<RectTransform>();
        m_eventTrigger = GetComponent<EventTrigger>();
        m_skillIcon = GetComponentInChildren<Image>();
    }

    private void SetEvent()
    {
        FunctionDefine.AddEvent(m_eventTrigger, EventTriggerType.PointerDown, CreateCarrier);
    }

    private void Awake()
    {
        SetComps();
        SetEvent();
    }
}
