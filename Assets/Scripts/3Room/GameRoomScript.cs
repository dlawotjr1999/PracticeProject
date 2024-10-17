using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameRoomScript : MonoBehaviour      // 방 관리자 가상 클래스
{
    private RoomMgr m_parent;
    private int RoomIdx { get; set; }
    public void SetParent(RoomMgr _parent, int _idx) { m_parent = _parent; RoomIdx = _idx; }

    protected RoomGateScript m_gate;

    protected bool Entered { get; set; } = false;
    protected bool Exited { get; set; } = false;

    public virtual void OnTriggerEnter(Collider _other)
    {
        if (!Entered && _other.CompareTag("Player"))
        {
            EnterRoom();
            Entered = true;
        }
    }

    public virtual void OnTriggerExit(Collider _other)
    {
        if(!Exited && _other.CompareTag("Player"))
        {
            ExitRoom();
            Exited = true;
        }
    }

    public virtual void EnterRoom() { PlayMgr.SetCurRoom(this); m_parent.CloseDoor(RoomIdx - 1); }
    public virtual void ExitRoom() { }
    public virtual void NormalClearRoom() { PlayMgr.PopupReward(false); m_gate.OpenDoor(); }
    public virtual void GimicClearRoom() { PlayMgr.PopupReward(true); m_gate.OpenDoor(); }

    public void CloseDoor() { m_gate.CloseDoor(); }


    protected void CreateRoomInfoUI(string _info)
    {
        GameObject ui = Instantiate(PlayMgr.RoomInfoPrefab, PlayMgr.MainCanvasTrans);
        RoomInfoUIScript script = ui.GetComponent<RoomInfoUIScript>();
        script.SetInfoTxt(_info);
    }


    public virtual void Awake()
    {
        m_gate = GetComponentInChildren<RoomGateScript>();
        if (m_gate)
        {
            m_gate.SetParent(this);
        }
    }
}
