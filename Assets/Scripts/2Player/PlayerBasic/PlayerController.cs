using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EPlayerStat             // �÷��̾� ����
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
    private Rigidbody m_rigid;          // ������ٵ�
    private Animator m_anim;            // �� �ִϸ�����
    private NavMeshAgent m_navAgent;    // �׺�Ž�

    private GameObject m_InventoryUI;   // �κ��丮

    private PlayerStateMgr m_stateMgr;  // �÷��̾� ���� ������
    private IPlayerState CurState { get { return m_stateMgr.CurState; } }       // �����ڿ� ���� ���� ����

    private IPlayerState m_idle, m_move, m_attack, m_dash, m_hit, m_die;       // ���� �������̽���                                  

    public void IdlePlayer() { m_stateMgr.ChangeToState(m_idle); }              // Idle ���·�
    public void MovePlayer() { m_stateMgr.ChangeToState(m_move); }              // Move ���·�
    public void AttackPlayer() { m_stateMgr.ChangeToState(m_attack); }          // Attack ���·�
    public void DashPlayer() { m_stateMgr.ChangeToState(m_dash); }              // Dash ���·�
    public void HitPlayer() { m_stateMgr.ChangeToState(m_hit); }                // Hit ���·�
    public void DiePlayer() { m_stateMgr.ChangeToState(m_die); }                // Die ���·�


    public Vector3 MouseTarget { get { return GetMouseTarget(); } }             // ���콺 ���� ��ǥ
    public Vector3 SkillAim { get { return GetSkillAim(); } }                   // ���콺�� ����� ��ġ


    // ���׷��̵� ���� ����
    private float m_curDamage = 1;
    private float m_curAttackSpeed = 1;
    public float CurDamage { get { if (BuffOn) return m_curDamage * 1.2f; return m_curDamage; } } // ������ ����
    private float CurMoveSpeed { get; set; } = 5;                   // �̵� �ӵ�
    private float CurAttackSpeed { get { if(BuffOn) return m_curAttackSpeed * 1.2f; return m_curAttackSpeed; } } // ���� �ӵ�
    public int MaxHP { get; set; } = 100;                           // �ִ� ü��
    public int Critical { get; set; } = 5;                          // ġ��Ÿ �����
    public int Evasion { get; set; } = 5;                           // ȸ�� �����


    // ���� ����
    public int CurHP { get; protected set; }                        // ���� ü��
    public float CurSpeed { set { m_navAgent.speed = value; } }     // �̵� �ӵ�
    private int CurStamina { get; set; }                            // ���� ���¹̳�
    public bool IsDead { get; protected set; }                      // ��� ����
    public bool InCombat { get; protected set; }                    // ���� ����
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


    private void InitiateValues()                   // �ʱ� �� ����
    {
        CurHP = MaxHP;
        CurStamina = MaxStamina;
        IsDead = false;
        PlayMgr.InitiateValues();
    }

    public void SetRotation(Vector2 _dir)           // ���� ���� ����
    {
        float deg = FunctionDefine.VecToDeg(_dir);
        Vector3 rot = transform.eulerAngles;
        rot.y = deg;
        transform.eulerAngles = rot;
    }

    public void OnKeyboard()                // Ű���� ����
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if (m_InventoryUI == null) { Debug.LogError("������ ��� ����"); return; }
            m_InventoryUI.SetActive(!m_InventoryUI.activeSelf);
        }

        // ��Ÿ Ű �ʿ�� �߰� ����
    }


    private Vector3 GetSkillAim()           // �÷��̾���� ������� ��ġ
    {
        Vector3 mouse = GetMouseTarget();
        if (mouse == Vector3.zero)
            return mouse;
        Vector3 position = transform.position;
        Vector3 aim = new(mouse.x - position.x, position.y, mouse.z - position.z);
        return aim;
    }
    private Vector3 GetMouseTarget()        // ���콺�� ����Ű�� ��ġ
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


    private void SetComps()         // ��� ����
    {
        m_rigid = GetComponent<Rigidbody>();
        m_anim = GetComponentInChildren<Animator>();
        m_navAgent = GetComponent<NavMeshAgent>();
        m_aimMgr = FindObjectOfType<AimMgr>();
        m_InventoryUI = GameObject.Find("SkillInvenUI");
        m_stateMgr = new PlayerStateMgr(this);
    }

    private void SetStates()        //  ���� ����
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

        // �ӽ� ��ų ����
        PlayMgr.ObtainSkill(ESkillName.THROW_POISON);
        PlayMgr.ObtainSkill(ESkillName.THROW_ICE);
        PlayMgr.ObtainSkill(ESkillName.AROUND_ELECTRIC);
        PlayMgr.ObtainSkill(ESkillName.SECTOR_FIRE);
        PlayMgr.ObtainSkill(ESkillName.BUFF);
    }

    private void Update()
    {
        SkillKeyChk();                          // ��ų ���� üũ
        CurState.Proceed();                     // ���� ������ Proceed �Լ�
    }
}