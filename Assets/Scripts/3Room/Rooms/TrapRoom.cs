using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoom : SpawnRoom
{
    private const string EnterInfo =
        "방 맞은 편에서 무언가 느껴진다.";

    public override void EnterRoom()
    {
        base.EnterRoom();
        CreateRoomInfoUI(EnterInfo);
    }
}
