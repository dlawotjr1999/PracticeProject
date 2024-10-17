using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragDropUI : MonoBehaviour
{
    protected EventTrigger m_eventTrigger;
    protected RectTransform m_rect;

    protected Vector2 OriginPos { get; set; }                   // �巡�� �� ��ġ
    private Vector2 MouseStartPos { get; set; }                 // �巡�� ���� �� ���콺 ��ġ

    public virtual void BeginDrag()       // �巡�� ����
    {
        OriginPos = m_rect.position;
        MouseStartPos = Input.mousePosition;
    }
    public virtual void BeginDrag(PointerEventData _data)       // �巡�� ����
    {
        OriginPos = m_rect.position;
        MouseStartPos = _data.position;
    }
    public virtual void OnDrag(PointerEventData _data)          // �巡�� ��
    {
        Vector2 offset = _data.position - MouseStartPos;
        m_rect.position = OriginPos + offset;
        AdjPos();
    }
    public virtual void EndDrag(PointerEventData _data)         // �巡�� ����
    {

    }

    public virtual void AdjPos()                                // UI�� ���� ��ġ ����
    {

    }


    public virtual void SetComps()
    {
        if(!TryGetComponent(out m_eventTrigger)) { Debug.LogError("������ �̺�ƮƮ���� ����"); return; }
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
