using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragDropUI : MonoBehaviour
{
    protected EventTrigger m_eventTrigger;
    protected RectTransform m_rect;

    protected Vector2 OriginPos { get; set; }                   // 드래그 전 위치
    private Vector2 MouseStartPos { get; set; }                 // 드래그 시작 시 마우스 위치

    public virtual void BeginDrag()       // 드래그 시작
    {
        OriginPos = m_rect.position;
        MouseStartPos = Input.mousePosition;
    }
    public virtual void BeginDrag(PointerEventData _data)       // 드래그 시작
    {
        OriginPos = m_rect.position;
        MouseStartPos = _data.position;
    }
    public virtual void OnDrag(PointerEventData _data)          // 드래그 중
    {
        Vector2 offset = _data.position - MouseStartPos;
        m_rect.position = OriginPos + offset;
        AdjPos();
    }
    public virtual void EndDrag(PointerEventData _data)         // 드래그 종료
    {

    }

    public virtual void AdjPos()                                // UI에 따른 위치 조정
    {

    }


    public virtual void SetComps()
    {
        if(!TryGetComponent(out m_eventTrigger)) { Debug.LogError("부착된 이벤트트리거 없음"); return; }
        m_rect = GetComponent<RectTransform>();
    }

    public virtual void SetEvents()
    {
        FunctionDefine.AddEvent(m_eventTrigger, EventTriggerType.BeginDrag, BeginDrag);
        FunctionDefine.AddEvent(m_eventTrigger, EventTriggerType.Drag, OnDrag);
        FunctionDefine.AddEvent(m_eventTrigger, EventTriggerType.EndDrag, EndDrag);
    }

    private void Awake()
    {
        SetComps();
        SetEvents();
    }
}
