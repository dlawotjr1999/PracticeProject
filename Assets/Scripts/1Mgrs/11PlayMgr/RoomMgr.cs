using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMgr : MonoBehaviour
{
    [SerializeField]
    private GameObject m_roomsObj;        // �� ������Ʈ
        
    public GameRoomScript[] Rooms { get; private set; }   // �� �� �����ڵ�

    public GameRoomScript CurRoom { get; private set; }
    public void SetCurRoom(GameRoomScript _room) { CurRoom = _room; }


    public Vector3 DefenseObjPos { get {
            DefenseRoom room = CurRoom as DefenseRoom;
            if(room == null) { Debug.LogError("����� �� �ƴ�"); return Vector3.zero; }
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
