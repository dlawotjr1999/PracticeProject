using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArangeRoom : SpawnRoom
{
    [SerializeField]
    private float m_hpPer;                          // 남겨야하는 체력 %
    public float HPPer { get { return m_hpPer; } }

    private ArangeUIScript m_arangeUI;

    [SerializeField]
    private GameObject m_arangeUIPref;

    private const string EnterInfo =
        "어질러진 방을 정리하자. 보이는 놈들을 완전히 죽이면 안될 것 같다.";
    private const string FailInfo =
        "정리를 해야 했던 것 같지만, 이것도 정리라고 생각하자...";

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
            monster.ArangeOn();                     // 정리 중 on
        }
        TotalCnt = m_monsterList.Count;
        Debug.Log(TotalCnt);
    }

    public void MonsterAranged(MonsterScript _monster)              // 몬스터 정리 완료됨
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

    private void ArangeDone()                       // 정리 완료
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
