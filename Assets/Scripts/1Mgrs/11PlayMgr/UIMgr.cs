using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    public MainCanvasScript MainCanvas { get; private set; }                    // ���� ĵ����
    public void SetMainCanvas(MainCanvasScript _canvas) { MainCanvas = _canvas; }
    public IngameCanvasScript IngameCanvas { get; private set; }                // �ΰ��� ĵ����
    public void SetIngameCanvas(IngameCanvasScript _canvas) { IngameCanvas = _canvas; }
    public ControlCanvasScript ControlCanvas { get; private set; }              // ������ ĵ����
    public void SetControlCanvas(ControlCanvasScript _canvas) { ControlCanvas= _canvas; }

    [SerializeField]
    private GameObject m_rewardPopupPrefab;             // ���� �˾� ������ => �ν��Ͻ�ȭ�ؼ� ȣ�� �ø��� ����
    [SerializeField]
    private GameObject m_skillCarrierPrefab;               // �κ��丮 -> ���Կ� ���Ǵ� ������
    public GameObject SkillCarrierPrefab { get { return m_skillCarrierPrefab;  } }

    [SerializeField]
    private GameObject m_skillInvenObj;                 // ��ų �κ��丮 ������Ʈ => �̸� �����ΰ� �¿���

    [SerializeField]
    private GameObject m_roomInfoPrefab;
    public GameObject RoomInfoPrefab { get { return m_roomInfoPrefab; } }

    [SerializeField]
    private GameObject m_monsterHPBarPrefab;
    public GameObject MonsterHPBarPrefab { get { return m_monsterHPBarPrefab; } }
    [SerializeField]
    private GameObject m_interactUIPrefab;              // ��ȣ�ۿ� ���� UI
    public GameObject InteractUIPrefab { get { return m_interactUIPrefab; } }

    [SerializeField]
    private GameObject m_evasionTxtPrefab;
    public GameObject EvasionTxtPrefab { get { return m_evasionTxtPrefab; } }

    [SerializeField]
    private Sprite[] m_skillIcons = new Sprite[(int)ESkillName.LAST];   // ��ų �����ܵ�
    public Sprite GetSkillIcon(ESkillName _skill) { return m_skillIcons[(int)_skill]; }

    [SerializeField]
    private Sprite[] m_statIcons = new Sprite[(int)EPlayerStat.LAST];
    public Sprite GetStatIcon(EPlayerStat _stat) { return m_statIcons[(int)_stat]; }


    private PlayerInfoUIScript m_playerInfoUI;          // �÷��̾� ���� UI
    private SetShootingUI m_setShootingUI;              // �⺻���� ���� UI
    private SkillSlotUIScript m_skillSlotUI;            // ��ų ���� UI
    public RectTransform[] SlotTrans { get { return m_skillSlotUI.HoldersTrans; } }

    public void InitiateValues() { m_playerInfoUI.InitiateValues(); }                       // �÷��̾� HP �ʱ� ����
    public void SetMaxHP(int _max) { m_playerInfoUI.SetMaxHP(_max); }
    public void SetCurHP(int _hp) { m_playerInfoUI.SetCurHP(_hp); }                       // �÷��̾� HP ���� �ø��� ����
    public void SetCurStamina(int _stamina) { m_playerInfoUI.SetCurStamina(_stamina); }     // �÷��̾� ���¹̳� ���� �ø��� ����
    public void SetStaminaPer(float _per) { m_playerInfoUI.SetStaminaPer(_per); }           // ���¹̳� ȸ���� (���� �Լ��� �ٲ㵵 �ɵ�?)

    public void ObtainShooting(ESkillName _skill) { m_setShootingUI.ObtainShooting((ESkillElement)(int)_skill); }         // �⺻ ���� ���
    public void StartShootingCoolTime(float _coolTime) { m_setShootingUI.StartShootingCoolTime(_coolTime); }

    public void SetSkillSlot(ESkillName _skill, int _idx) 
    {
        m_skillSlotUI.SetSkillSlot(_skill, _idx); 
        m_skillInvenObj.GetComponent<SkillInvenUIScript>().SlotChanged();
    }                 // ��ų ���� ����
    public void StartSkillCoolTime(int _idx, float _coolTime) { m_skillSlotUI.StartSkillCoolTime(_idx, _coolTime); }    // ��ų ��Ÿ�� ����(��� �� ȣ��)

    public void PopupReward(bool _skill) 
    {
        GameObject popup = Instantiate(m_rewardPopupPrefab, MainCanvas.transform);
        RewardPopupScript script = popup.GetComponent<RewardPopupScript>();
        if (_skill)
            script.Popup(true);
        else
            script.Popup(false);
    }

    public void AddSkillToInven(ESkillName _skill) { m_skillInvenObj.GetComponent<SkillInvenUIScript>().AddSkill(_skill); }

    private void SetComps()     // ��� �ʱ� ����
    {
        m_playerInfoUI = MainCanvas.GetComponentInChildren<PlayerInfoUIScript>();
        m_setShootingUI = MainCanvas.GetComponentInChildren<SetShootingUI>();
        m_skillSlotUI = MainCanvas.GetComponentInChildren<SkillSlotUIScript>();
    }

    private void Awake()
    {
        SetComps();
    }
}
