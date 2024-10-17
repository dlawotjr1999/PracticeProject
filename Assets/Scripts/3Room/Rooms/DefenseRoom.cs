using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseRoom : SpawnRoom
{
    private DefenseObjScript m_defenseObj;                          // ��� ���
    private DefenseUIScript m_defenseUI;                            // ������� UI

    [SerializeField]
    private GameObject m_defenseUIPref;                             // ���潺 UI ������

    private string EnterInfo =
        "����� �����´�! �̰� �������� �ȵǴµ�...!";
    private string ClearInfo =
        "�μ����� �ʾ� �����ε�... �� ��� ���𰡿� �������ϰ� �ִ� �� �и��ϴ�.";
    private string FailInfo =
        "���ڱ� ���ǿ� �޷��� ������ ���µ�! �� ���, �и��� ������ �����ϰ� �ִ� �� ����.";


    public Vector3 DefenseObjPos { get { return m_defenseObj.transform.position; } }

    private bool WaveDone { get; set; } = false;                    // ���̺� ��ȯ �Ϸ� ����
    public bool MissionFailed { get; private set; }                 // �̼� ���� ����

    private const float SpawnGapTime = 5;                          // ���̺갣 ����

    private void Spawn14() { SpawnMonster(0); SpawnMonster(2); }
    private void Spawn23() { SpawnMonster(1); SpawnMonster(3); }

    private IEnumerator Wave1(float _time)          // 1���̺�
    {
        yield return new WaitForSeconds(_time);
        Spawn14();
        StartCoroutine(Wave2(SpawnGapTime));
    }
    private IEnumerator Wave2(float _time)          // 2���̺�
    { 
        yield return new WaitForSeconds(_time);
        Spawn23();
        StartCoroutine(Wave3(SpawnGapTime)); 
    }
    private IEnumerator Wave3(float _time)          // 3���̺�
    { 
        yield return new WaitForSeconds(_time);
        Spawn14();
        StartCoroutine(Wave4(SpawnGapTime));
    }
    private IEnumerator Wave4(float _time)          // 3���̺�
    {
        yield return new WaitForSeconds(_time);
        Spawn23();
        WaveDone = true;
    }

    public override void SpawnMonster(int _idx)
    {
        base.SpawnMonster(_idx);
        foreach (MonsterScript monster in m_monsterList)    // ����� on
        {
            if(monster.Defensing) { return; }
            monster.DefenseOn();
        }
    }

    public override void EnterRoom()                // ���� �� ��ȯ �ȵǵ��� �������̵�
    {
        PlayMgr.SetCurRoom(this);
    }

    private IEnumerator DefenseUIOn()                      // UI ����
    {
        yield return new WaitForSeconds(5.5f);
        GameObject defenseUI = Instantiate(m_defenseUIPref, PlayMgr.MainCanvasTrans);
        m_defenseUI = defenseUI.GetComponentInChildren<DefenseUIScript>();
        m_defenseUI.InitHP(DefenseObjScript.MaxHP);
    }
    private void DefenseUIOff()                     // UI ����
    {
        Destroy(m_defenseUI.gameObject);
    }

    public void SetHP(int _hp)                      // HP ����
    {
        m_defenseUI.SetHP(_hp);
    }

    public void StartDefense()                      // ����� ����
    {
        StartCoroutine(DefenseUIOn());
        CreateRoomInfoUI(EnterInfo);
        MissionFailed = false;
        WaveDone = false;
        StartCoroutine(Wave1(2));
    }

    public void DefenseFail()                       // ����� ����
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
        if (m_monsterList.Count == 0 && WaveDone)               // ���� �� �װ� ���̺� �� ��ȯ �Ȱ��� Ȯ��
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
