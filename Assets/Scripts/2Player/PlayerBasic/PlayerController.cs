using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EPlayerStat             // 플레이어 스탯
{
    DAMAGE,
    MOVESPEED,
    ATTACKSPEED,
    MAXHP,
    CRITICAL,
    EVASION,
    LAST
}

public partial class PlayerController : MonoBehaviour
{
    private Rigidbody m_rigid;          // 리지드바디
    private Animator m_anim;            // 모델 애니메이터
    private NavMeshAgent m_navAgent;    // 네브매쉬

    private GameObject m_InventoryUI;   // 인벤토리

    private PlayerStateMgr m_stateMgr;  // 플레이어 상태 관리자
    private IPlayerState CurState { get { return m_stateMgr.CurState; } }       // 관리자에 따른 현재 상태

    private IPlayerState m_idle, m_move, m_attack, m_dash, m_hit, m_die;       // 상태 인터페이스들                                  

    public void IdlePlayer() { m_stateMgr.ChangeToState(m_idle); }              // Idle 상태로
    public void MovePlayer() { m_stateMgr.ChangeToState(m_move); }              // Move 상태로
    public void AttackPlayer() { m_stateMgr.ChangeToState(m_attack); }          // Attack 상태로
    public void DashPlayer() { m_stateMgr.ChangeToState(m_dash); }              // Dash 상태로
    public void HitPlayer() { m_stateMgr.ChangeToState(m_hit); }                // Hit 상태로
    public void DiePlayer() { m_stateMgr.ChangeToState(m_die); }                // Die 상태로


    public Vector3 MouseTarget { get { return GetMouseTarget(); } }             // 마우스 세계 좌표
    public Vector3 SkillAim { get { return GetSkillAim(); } }                   // 마우스의 상대적 위치


    // 업그레이드 가능 스탯
    private float m_curDamage = 1;
    private float m_curAttackSpeed = 1;
    public float CurDamage { get { if (BuffOn) return m_curDamage * 1.2f; return m_curDamage; } } // 데미지 배율
    private float CurMoveSpeed { get; set; } = 5;                   // 이동 속도
    private float CurAttackSpeed { get { if(BuffOn) return m_curAttackSpeed * 1.2f; return m_curAttackSpeed; } } // 공격 속도
    public int MaxHP { get; set; } = 100;                           // 최대 체력
    public int Critical { get; set; } = 5;                          // 치명타 백분율
    public int Evasion { get; set; } = 5;                           // 회피 백분율


    // 현재 상태
    public int CurHP { get; protected set; }                        // 현재 체력
    public float CurSpeed { set { m_navAgent.speed = value; } }     // 이동 속도
    private int CurStamina { get; set; }                            // 현재 스태미나
    public bool IsDead { get; protected set; }                      // 사망 상태
    public bool InCombat { get; protected set; }                    // 전투 상태
    private bool BuffOn { get; set; }


    public void SetDead()
    {
        IsDead = true;
    }


    public void UpgradePlayerStat(EPlayerStat _stat)
    {
        switch(_stat)
        {
            case EPlayerStat.DAMAGE:
                m_curDamage += 0.1f;
                break;
            case EPlayerStat.MOVESPEED:
                CurMoveSpeed += 0.5f;
                CurSpeed = CurMoveSpeed;
                break;
            case EPlayerStat.ATTACKSPEED:
                m_curAttackSpeed += 0.1f;
                break;
            case EPlayerStat.MAXHP:
                MaxHP += 10;
                CurHP += 10;
                PlayMgr.SetMaxHP(MaxHP);
                PlayMgr.SetCurHP(CurHP);
                break;
            case EPlayerStat.CRITICAL:
                Critical += 1;
                break;
            case EPlayerStat.EVASION:
                Evasion += 1;
                break;
        }

        PlayMgr.CreatePlayerEffect(EPlayerEffect.STAT_UP, transform);
    }


    private void InitiateValues()                   // 초기 값 설정
    {
        CurHP = MaxHP;
        CurStamina = MaxStamina;
        IsDead = false;
        PlayMgr.InitiateValues();
    }

    public void SetRotation(Vector2 _dir)           // 보는 방향 설정
    {
        float deg = FunctionDefine.VecToDeg(_dir);
        Vector3 rot = transform.eulerAngles;
        rot.y = deg;
        transform.eulerAngles = rot;
    }

    public void OnKeyboard()                // 키셋팅 구현
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if (m_InventoryUI == null) { Debug.LogError("지정된 토글 없음"); return; }
            m_InventoryUI.SetActive(!m_InventoryUI.activeSelf);
        }

        // 기타 키 필요시 추가 예정
    }


    private Vector3 GetSkillAim()           // 플레이어와의 상대적인 위치
    {
        Vector3 mouse = GetMouseTarget();
        if (mouse == Vector3.zero)
            return mouse;
        Vector3 position = transform.position;
        Vector3 aim = new(mouse.x - position.x, position.y, mouse.z - position.z);
        return aim;
    }
    private Vector3 GetMouseTarget()        // 마우스가 가리키는 위치
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, ValueDefine.AimLayerIdx))
        {
            Vector3 mouse = hit.point;
            if((mouse - transform.position).magnitude > SkillValues.ThrowDist)
            {
                return transform.position + (mouse - transform.position).normalized * SkillValues.ThrowDist;
            }
            return mouse;
        }
        return Vector3.zero;
    }


    private void SetComps()         // 요소 설정
    {
        m_rigid = GetComponent<Rigidbody>();
        m_anim = GetComponentInChildren<Animator>();
        m_navAgent = GetComponent<NavMeshAgent>();
        m_aimMgr = FindObjectOfType<AimMgr>();
        m_InventoryUI = GameObject.Find("SkillInvenUI");
        m_stateMgr = new PlayerStateMgr(this);
    }

    private void SetStates()        //  상태 선언
    {
        m_idle = gameObject.AddComponent<PIdleState>();
        m_move = gameObject.AddComponent<PMoveState>();
        m_attack = gameObject.AddComponent<PAttackState>();
        m_dash = gameObject.AddComponent<PDashState>();
        m_hit = gameObject.AddComponent<PHitState>();
        m_die = gameObject.AddComponent<PDieState>();
    }

    private void SetValues()
    {
        CurSpeed = CurMoveSpeed;
        CurStamina = MaxStamina;
    }

    private void Awake()
    {
        SetComps();
        SetStates();
        SetValues();
        PlayMgr.SetCurPlayer(this);
    }

    private void Start()
    {
        InitiateValues();
        IdlePlayer();

        GameMgr.InputMgr.keyAction -= OnKeyboard;
        GameMgr.InputMgr.keyAction += OnKeyboard;

        // 임시 스킬 세팅
        PlayMgr.ObtainSkill(ESkillName.THROW_POISON);
        PlayMgr.ObtainSkill(ESkillName.THROW_ICE);
        PlayMgr.ObtainSkill(ESkillName.AROUND_ELECTRIC);
        PlayMgr.ObtainSkill(ESkillName.SECTOR_FIRE);
        PlayMgr.ObtainSkill(ESkillName.BUFF);
    }

    private void Update()
    {
        SkillKeyChk();                          // 스킬 설정 체크
        CurState.Proceed();                     // 현재 상태의 Proceed 함수
    }
}