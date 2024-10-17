using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : SingleTon<GameMgr>
{
    public static void PauseGame() { Time.timeScale = 0; }
    public static void ResumeGame() { Time.timeScale = 1; }

    // ȭ�� ����
    private ScreenMgr m_screenMgr;
    public static ScreenMgr ScreenMgr { get { return Inst.m_screenMgr; } }

    // �Ҹ� ����
    private SoundMgr m_soundMgr;
    public static SoundMgr SoundMgr { get { return Inst.m_soundMgr; } }

    // Ű �Է� ����
    private InputMgr m_input = new InputMgr();
    public static InputMgr InputMgr { get { return Inst.m_input; } }

    private void SetSubMgrs()
    {
        m_screenMgr = GetComponent<ScreenMgr>();
        m_soundMgr = GetComponent<SoundMgr>();
    }

    public override void Awake()
    {
        base.Awake();
        SetSubMgrs();
    }

    private void Update()
    {
        m_input.OnUpdate();
    }
}