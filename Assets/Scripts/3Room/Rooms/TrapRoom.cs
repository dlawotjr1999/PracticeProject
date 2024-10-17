using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoom : SpawnRoom
{
    private const string EnterInfo =
        "�� ���� ���� ���� ��������.";

    public override void EnterRoom()
    {
        base.EnterRoom();
        CreateRoomInfoUI(EnterInfo);
    }
}
