using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMgr : MonoBehaviour
{
    [SerializeField]
    private GameObject m_roomsObj;        // 맵 오브젝트
        
    public GameRoomScript[] Rooms { get; private set; }   // 각 방 관리자들

    public GameRoomScript CurRoom { get; private set; }
    public void SetCurRoom(GameRoomScript _room) { CurRoom = _room; }


    public Vector3 DefenseObjPos { get {
            DefenseRoom room = CurRoom as DefenseRoom;
            if(room == null) { Debug.LogError("방어전 방 아님"); return Vector3.zero; }
            return room.DefenseObjPos;
        } 
    }

    public void CloseDoor(int _idx)
    {
        if(_idx < 0 || _idx >= Rooms.Length) { return; }
        
        Rooms[_idx].CloseDoor();
    }

    private void SetRooms()
    {
        Rooms = m_roomsObj.GetComponentsInChildren<GameRoomScript>();
        for (int i = 0; i<Rooms.Length; i++)
        {
            Rooms[i].SetParent(this, i);
        }
    }

    private void Awake()
    {
        SetRooms();
    }
}
