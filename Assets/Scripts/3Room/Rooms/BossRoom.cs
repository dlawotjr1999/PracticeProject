using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : GameRoomScript
{
    private const string EnterInfo =
        "놈들이 조종당했던 힘이 느껴진다. 이 곳에 그 배후가 있는 것 같다.";
    private const string EncounterInfo =
        "이 놈이 말썽의 근원인 듯하다. 강한 듯하지만, 저지해야 한다.";

    public override void EnterRoom()
    {
        base.EnterRoom();
        CreateRoomInfoUI(EnterInfo);
    }

    private void EncounterBoss()
    {
        CreateRoomInfoUI(EncounterInfo);

    }
}
