using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseRoom : SpawnRoom
{
    private DefenseObjScript m_defenseObj;                          // 방어 대상
    private DefenseUIScript m_defenseUI;                            // 만들어진 UI

    [SerializeField]
    private GameObject m_defenseUIPref;                             // 디펜스 UI 프리펍

    private string EnterInfo =
        "놈들이 몰려온다! 이건 망가지면 안되는데...!";
    private string ClearInfo =
        "부서지지 않아 다행인데... 이 놈들 무언가에 조종당하고 있는 게 분명하다.";
    private string FailInfo =
        "갑자기 물건에 달려들 이유가 없는데! 이 놈들, 분명히 누군가 조종하고 있는 것 같다.";


    public Vector3 DefenseObjPos { get { return m_defenseObj.transform.position; } }

    private bool WaveDone { get; set; } = false;                    // 웨이브 소환 완료 여부
    public bool MissionFailed { get; private set; }                 // 미션 실패 여부

    private const float SpawnGapTime = 5;                          // 웨이브간 간격

    private void Spawn14() { SpawnMonster(0); SpawnMonster(2); }
    private void Spawn23() { SpawnMonster(1); SpawnMonster(3); }

    private IEnumerator Wave1(float _time)          // 1웨이브
    {
        yield return new WaitForSeconds(_time);
        Spawn14();
        StartCoroutine(Wave2(SpawnGapTime));
    }
    private IEnumerator Wave2(float _time)          // 2웨이브
    { 
        yield return new WaitForSeconds(_time);
        Spawn23();
        StartCoroutine(Wave3(SpawnGapTime)); 
    }
    private IEnumerator Wave3(float _time)          // 3웨이브
    { 
        yield return new WaitForSeconds(_time);
        Spawn14();
        StartCoroutine(Wave4(SpawnGapTime));
    }
    private IEnumerator Wave4(float _time)          // 3웨이브
    {
        yield return new WaitForSeconds(_time);
        Spawn23();
        WaveDone = true;
    }

    public override void SpawnMonster(int _idx)
    {
        base.SpawnMonster(_idx);
        foreach (MonsterScript monster in m_monsterList)    // 방어전 on
        {
            if(monster.Defensing) { return; }
            monster.DefenseOn();
        }
    }

    public override void EnterRoom()                // 입장 후 소환 안되도록 오버라이드
    {
        PlayMgr.SetCurRoom(this);
    }

    private IEnumerator DefenseUIOn()                      // UI 생성
    {
        yield return new WaitForSeconds(5.5f);
        GameObject defenseUI = Instantiate(m_defenseUIPref, PlayMgr.MainCanvasTrans);
        m_defenseUI = defenseUI.GetComponentInChildren<DefenseUIScript>();
        m_defenseUI.InitHP(DefenseObjScript.MaxHP);
    }
    private void DefenseUIOff()                     // UI 삭제
    {
        Destroy(m_defenseUI.gameObject);
    }

    public void SetHP(int _hp)                      // HP 설정
    {
        m_defenseUI.SetHP(_hp);
    }

    public void StartDefense()                      // 방어전 시작
    {
        StartCoroutine(DefenseUIOn());
        CreateRoomInfoUI(EnterInfo);
        MissionFailed = false;
        WaveDone = false;
        StartCoroutine(Wave1(2));
    }

    public void DefenseFail()                       // 방어전 실패
    {
        MissionFailed = true;
        CreateRoomInfoUI(FailInfo);
    }

    public override void NormalClearRoom()
    {
        m_gate.OpenDoor();
        if (!MissionFailed)
        {
            PlayMgr.PopupReward(true);
            CreateRoomInfoUI(ClearInfo);
        }
        else
        {
            DefenseFail();
        }
        DefenseUIOff();
    }

    public override void MonsterDead(MonsterScript _monster)
    {
        m_monsterList.Remove(_monster);
        if (m_monsterList.Count == 0 && WaveDone)               // 몬스터 다 죽고 웨이브 다 소환 된건지 확인
        {
            NormalClearRoom();
        }
    }

    public override void Awake()
    {
        base.Awake();
        m_defenseObj = GetComponentInChildren<DefenseObjScript>();
        m_defenseObj.SetParent(this);
    }
}
