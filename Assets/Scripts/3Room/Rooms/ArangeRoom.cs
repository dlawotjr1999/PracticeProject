using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArangeRoom : SpawnRoom
{
    [SerializeField]
    private float m_hpPer;                          // ���ܾ��ϴ� ü�� %
    public float HPPer { get { return m_hpPer; } }

    private ArangeUIScript m_arangeUI;

    [SerializeField]
    private GameObject m_arangeUIPref;

    private const string EnterInfo =
        "�������� ���� ��������. ���̴� ����� ������ ���̸� �ȵ� �� ����.";
    private const string FailInfo =
        "������ �ؾ� �ߴ� �� ������, �̰͵� ������� ��������...";

    private int ArangeCnt { get; set; } = 0;
    private int TotalCnt { get; set; }

    public override void EnterRoom()
    {
        base.EnterRoom();
        CreateRoomInfoUI(EnterInfo);
        GameObject ui = Instantiate(m_arangeUIPref, PlayMgr.MainCanvasTrans);
        m_arangeUI = ui.GetComponent<ArangeUIScript>();
        m_arangeUI.InitUI();
    }

    public override void SpawnMonster()
    {
        base.SpawnMonster();
        foreach (MonsterScript monster in m_monsterList)
        {
            monster.ArangeOn();                     // ���� �� on
        }
        TotalCnt = m_monsterList.Count;
        Debug.Log(TotalCnt);
    }

    public void MonsterAranged(MonsterScript _monster)              // ���� ���� �Ϸ��
    {
        m_monsterList.Remove(_monster);
        ArangeCnt++;
        m_arangeUI.MonsterAranged(ArangeCnt);
        ChkDone();
    }

    public override void MonsterDead(MonsterScript _monster)
    {
        m_monsterList.Remove(_monster);
        ChkDone();
    }

    private void ChkDone()
    {
        if (m_monsterList.Count > 0) return;

        ArangeDone();
        m_arangeUI.ArangeDone();
    }

    private void ArangeDone()                       // ���� �Ϸ�
    {
        if (ArangeCnt == TotalCnt)
        {
            GimicClearRoom();
        }
        else
        {
            ArangeFail();
        }
    }

    private void ArangeFail()
    {
        CreateRoomInfoUI(FailInfo);
        m_gate.OpenDoor();
    }
}
