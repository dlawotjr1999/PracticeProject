using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    public MainCanvasScript MainCanvas { get; private set; }                    // 메인 캔버스
    public void SetMainCanvas(MainCanvasScript _canvas) { MainCanvas = _canvas; }
    public IngameCanvasScript IngameCanvas { get; private set; }                // 인게임 캔버스
    public void SetIngameCanvas(IngameCanvasScript _canvas) { IngameCanvas = _canvas; }
    public ControlCanvasScript ControlCanvas { get; private set; }              // 조작중 캔버스
    public void SetControlCanvas(ControlCanvasScript _canvas) { ControlCanvas= _canvas; }

    [SerializeField]
    private GameObject m_rewardPopupPrefab;             // 보상 팝업 프리펍 => 인스턴스화해서 호출 시마다 생성
    [SerializeField]
    private GameObject m_skillCarrierPrefab;               // 인벤토리 -> 슬롯에 사용되는 프리펍
    public GameObject SkillCarrierPrefab { get { return m_skillCarrierPrefab;  } }

    [SerializeField]
    private GameObject m_skillInvenObj;                 // 스킬 인벤토리 오브젝트 => 미리 만들어두고 온오프

    [SerializeField]
    private GameObject m_roomInfoPrefab;
    public GameObject RoomInfoPrefab { get { return m_roomInfoPrefab; } }

    [SerializeField]
    private GameObject m_monsterHPBarPrefab;
    public GameObject MonsterHPBarPrefab { get { return m_monsterHPBarPrefab; } }
    [SerializeField]
    private GameObject m_interactUIPrefab;              // 상호작용 정보 UI
    public GameObject InteractUIPrefab { get { return m_interactUIPrefab; } }

    [SerializeField]
    private GameObject m_evasionTxtPrefab;
    public GameObject EvasionTxtPrefab { get { return m_evasionTxtPrefab; } }

    [SerializeField]
    private Sprite[] m_skillIcons = new Sprite[(int)ESkillName.LAST];   // 스킬 아이콘들
    public Sprite GetSkillIcon(ESkillName _skill) { return m_skillIcons[(int)_skill]; }

    [SerializeField]
    private Sprite[] m_statIcons = new Sprite[(int)EPlayerStat.LAST];
    public Sprite GetStatIcon(EPlayerStat _stat) { return m_statIcons[(int)_stat]; }


    private PlayerInfoUIScript m_playerInfoUI;          // 플레이어 정보 UI
    private SetShootingUI m_setShootingUI;              // 기본공격 설정 UI
    private SkillSlotUIScript m_skillSlotUI;            // 스킬 슬롯 UI
    public RectTransform[] SlotTrans { get { return m_skillSlotUI.HoldersTrans; } }

    public void InitiateValues() { m_playerInfoUI.InitiateValues(); }                       // 플레이어 HP 초기 세팅
    public void SetMaxHP(int _max) { m_playerInfoUI.SetMaxHP(_max); }
    public void SetCurHP(int _hp) { m_playerInfoUI.SetCurHP(_hp); }                       // 플레이어 HP 변경 시마다 실행
    public void SetCurStamina(int _stamina) { m_playerInfoUI.SetCurStamina(_stamina); }     // 플레이어 스태미나 변경 시마다 실행
    public void SetStaminaPer(float _per) { m_playerInfoUI.SetStaminaPer(_per); }           // 스태미나 회복률 (내부 함수로 바꿔도 될듯?)

    public void ObtainShooting(ESkillName _skill) { m_setShootingUI.ObtainShooting((ESkillElement)(int)_skill); }         // 기본 공격 얻기
    public void StartShootingCoolTime(float _coolTime) { m_setShootingUI.StartShootingCoolTime(_coolTime); }

    public void SetSkillSlot(ESkillName _skill, int _idx) 
    {
        m_skillSlotUI.SetSkillSlot(_skill, _idx); 
        m_skillInvenObj.GetComponent<SkillInvenUIScript>().SlotChanged();
    }                 // 스킬 슬롯 설정
    public void StartSkillCoolTime(int _idx, float _coolTime) { m_skillSlotUI.StartSkillCoolTime(_idx, _coolTime); }    // 스킬 쿨타임 시작(사용 시 호출)

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

    private void SetComps()     // 요소 초기 구성
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
