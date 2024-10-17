using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : GameRoomScript
{
    private const string EnterInfo =
        "����� �������ߴ� ���� ��������. �� ���� �� ���İ� �ִ� �� ����.";
    private const string EncounterInfo =
        "�� ���� ������ �ٿ��� ���ϴ�. ���� ��������, �����ؾ� �Ѵ�.";

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
