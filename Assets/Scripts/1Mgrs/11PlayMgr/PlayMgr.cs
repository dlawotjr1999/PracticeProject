using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMgr : MonoBehaviour
{
    public static PlayMgr Inst;

    // 플레이어 관련
    public static PlayerController Player { get; private set; }
    public static void SetCurPlayer(PlayerController _player) { Player = _player; }
    public static float PlayerDamage { get { return Player.CurDamage; } }
    public static Vector3 PlayerPos { get { return Player.transform.position; } }                                       // 플레이어 현재 위치
    public static Vector3 PlayerTarget { get { return Player.MouseTarget; } }                                           // 마우스 조준점을 y=0으로 투사
    public static void UpgradePlayerStat(EPlayerStat _stat) { Player.UpgradePlayerStat(_stat); }                        // 보상에 따른 플레이어 강화
    public static void SetCurAttack(ESkillElement _element) { Player.SelectShooting(_element); }                        // 기본공격 설정
    public static ESkillName[] PlayerSkillSlot { get { return Player.CurSkillSlot; } }                                  // 현재 스킬 슬롯


    // 카메라 관련
    private CameraMgr m_cameraMgr;
    public static CameraMgr CameraMgr { get { return Inst.m_cameraMgr; } }
    public static void SetMainCamera(Camera _camera) { CameraMgr.SetMainCamera(_camera); }



    // UI 관련
    private UIMgr m_uiMgr;
    public static UIMgr UIMgr { get { return Inst.m_uiMgr; } }
    public static Transform MainCanvasTrans { get { return UIMgr.MainCanvas.transform; } }                              // 현재 캔버스
    public static void SetMainCanvas(MainCanvasScript _canvas) { UIMgr.SetMainCanvas(_canvas); }                        // ㄴ설정
    public static Transform IngameCanvasTrans { get { return UIMgr.IngameCanvas.transform; } }                          // 인게임 캔버스
    public static void SetIngameCanvas(IngameCanvasScript _canvas) { UIMgr.SetIngameCanvas(_canvas); }                  // ㄴ설정
    public static Transform ControlCanvasTrans { get { return UIMgr.ControlCanvas.transform; } }                        // 조작중 캔버스
    public static void SetControlCanvas(ControlCanvasScript _canvas) { UIMgr.SetControlCanvas(_canvas); }               // ㄴ설정

    public static GameObject SkillCarrierPrefab { get { return UIMgr.SkillCarrierPrefab; } }                            // 스킬 캐리어 프리펍
    public static GameObject RoomInfoPrefab { get { return UIMgr.RoomInfoPrefab; } }                                    // 설명 텍스트 프리펍
    public static GameObject MonsterHPBarPrefab { get { return UIMgr.MonsterHPBarPrefab; } }                            // 몬스터 체력바
    public static GameObject InteractUIPrefab { get { return UIMgr.InteractUIPrefab; } }                                // 상호작용 표시 UI
    public static GameObject EvasionUIPreafb { get { return UIMgr.EvasionTxtPrefab; } }                                 // 회피 효과 UI
    public static Sprite GetSkillIcon(ESkillName _skill) { return UIMgr.GetSkillIcon(_skill); }                         // 스킬 아이콘
    public static Sprite GetRewardIcon(RewardInfo _reward) {
        if (_reward.IsSkill) return UIMgr.GetSkillIcon(((SkillRewardInfo)_reward).SkillName);
        return UIMgr.GetStatIcon(((StatRewardInfo)_reward).StatName); }                                                 // 보상 아이콘
    public static void InitiateValues() { UIMgr.InitiateValues(); }                                                     // HP,스태미나 초기 설정
    public static void SetMaxHP(int _max) { UIMgr.SetMaxHP(_max); }                                                     // 최대 HP 설정
    public static void SetCurHP(int _hp) { UIMgr.SetCurHP(_hp); }                                                       // HP 변화 시마다 호출
    public static void SetCurStamina(int _stamina) { UIMgr.SetCurStamina(_stamina); }                                   // 스태미나 변경 시마다 호출
    public static void SetStaminaPer(float _per) { UIMgr.SetStaminaPer(_per); }                                         // 스태미나 회복률 표시
    public static void SetSkillSlot(ESkillName _skill, int _idx) 
    { Player.SetSkillSlot(_skill, _idx); UIMgr.SetSkillSlot(_skill, _idx);  }                                           // 스킬 설정 (드래그 드롭)
    public static void StartShootingCoolTime(float _coolTime) { UIMgr.StartShootingCoolTime(_coolTime); }               // 슈팅 쿨타임 시작
    public static RectTransform[] SlotTrans { get { return UIMgr.SlotTrans; } }
    public static void StartSkillCoolTime(int _idx, float _coolTime) { UIMgr.StartSkillCoolTime(_idx, _coolTime); }     // 스킬 쿨타임 시작
    public static void PopupReward(bool _skill) { UIMgr.PopupReward(_skill); }                                          // 보상 획득 UI 띄우기


    // 방 작동 관련
    private RoomMgr m_roomMgr;
    public static RoomMgr RoomMgr { get { return Inst.m_roomMgr; } }
    public static GameRoomScript CurRoom { get { return RoomMgr.CurRoom; } }                                            // 플레이어 현재 위치 방
    public static void SetCurRoom(GameRoomScript _room) { RoomMgr.SetCurRoom(_room); }                                  // 현재 방 등록
    public static Vector3 DefenseObjPos { get { return RoomMgr.DefenseObjPos; } }


    // 보상 관련
    private RewardMgr m_rewardMgr;
    public static RewardMgr RewardMgr { get { return Inst.m_rewardMgr; } }
    public static RewardInfo GetRandomRewardInfo() { return RewardMgr.GetRandomRewardInfo(); }                          // 확률에 따라 랜덤 보상 뽑기
    public static SkillRewardInfo GetRandomSkillInfo() { return (SkillRewardInfo)RewardMgr.GetRandomSkillInfo(); }      // 스킬 정보만
    public static StatRewardInfo GetRandomStatInfo() { return (StatRewardInfo)RewardMgr.GetRandomStatInfo(); }          // 스탯 정보만
    public static void SelectReward(RewardInfo _reward) { RewardMgr.SelectReward(_reward); }                            // 보상 선택 (인벤에 추가)


    // 스킬 프리펍 관련
    private SkillPrefabs m_skillPrefabs;
    public static SkillPrefabs SkillPrefabs { get { return Inst.m_skillPrefabs; } }
    public static GameObject GetSkillPrefab(ESkillName _skill) { return SkillPrefabs.GetSkillPrefab(_skill); }          // 스킬 프리펍


    // 몬스터 프리펍 관련
    private MonsterPrefabs m_monsterPrefabs;
    public static MonsterPrefabs MonsterPrefabs { get { return Inst.m_monsterPrefabs; } }
    public static GameObject GetMonsterPrefab(EMonsterName _monster) { return MonsterPrefabs.GetMonsterPrefab(_monster); }  // 몬스터 프리펍
    public static GameObject GroupMonsterPrefab { get { return MonsterPrefabs.GroupMonsterPrefab; } }


    // 효과 프리펍 관련
    private EffectPrefabs m_effectPrefabs;
    public static EffectPrefabs EffectPrefabs { get { return Inst.m_effectPrefabs; } }      // 효과 프리펍
    public static void CreatePlayerEffect(EPlayerEffect _effect, Vector3 _pos) { EffectPrefabs.CreatePlayerEffect(_effect, _pos); }
    public static void CreatePlayerEffect(EPlayerEffect _effect, Transform _trans) { EffectPrefabs.CreatePlayerEffect(_effect, _trans); }
    public static void CreateMonsterEffect(EMonsterEffect _effect, Vector3 _pos) { EffectPrefabs.CreateMonsterEffect(_effect, _pos); }
    public static void CreateSkillEffect(ESkillEffect _effect, Vector3 _pos) { EffectPrefabs.CreateSkillEffect(_effect, _pos); }
    public static void CreateGimicEffect(EGimicEffect _effect, Vector3 _pos) { EffectPrefabs.CreateGimicEffect(_effect, _pos); }


    // 스킬 인벤토리 관련
    private SkillInvenMgr m_skillInventory;
    public static SkillInvenMgr SkillInventory { get { return Inst.m_skillInventory; } }
    public static bool HaveSkill(ESkillName _skill) { return SkillInventory.HaveSkill(_skill); }                                // 스킬 보유 여부
    public static void ObtainSkill(ESkillName _skill) { SkillInventory.ObtainSkill(_skill); UIMgr.AddSkillToInven(_skill); }    // 스킬 획득
    public static void ObtainShooting(ESkillName _skill) { UIMgr.ObtainShooting(_skill); }



    private void SetSubMgrs()
    {
        m_cameraMgr = GetComponent<CameraMgr>();
        m_uiMgr = GetComponent<UIMgr>();
        m_roomMgr = GetComponent<RoomMgr>();
        m_rewardMgr = GetComponent<RewardMgr>();
        m_skillPrefabs = GetComponent<SkillPrefabs>();
        m_monsterPrefabs = GetComponent<MonsterPrefabs>();
        m_effectPrefabs = GetComponent<EffectPrefabs>();
        m_skillInventory = GetComponent<SkillInvenMgr>();
    }

    private void Awake()
    {
        Inst = this;
        SetSubMgrs();
    }
}
