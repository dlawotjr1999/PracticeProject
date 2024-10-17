using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMgr : MonoBehaviour
{
    public static PlayMgr Inst;

    // �÷��̾� ����
    public static PlayerController Player { get; private set; }
    public static void SetCurPlayer(PlayerController _player) { Player = _player; }
    public static float PlayerDamage { get { return Player.CurDamage; } }
    public static Vector3 PlayerPos { get { return Player.transform.position; } }                                       // �÷��̾� ���� ��ġ
    public static Vector3 PlayerTarget { get { return Player.MouseTarget; } }                                           // ���콺 �������� y=0���� ����
    public static void UpgradePlayerStat(EPlayerStat _stat) { Player.UpgradePlayerStat(_stat); }                        // ���� ���� �÷��̾� ��ȭ
    public static void SetCurAttack(ESkillElement _element) { Player.SelectShooting(_element); }                        // �⺻���� ����
    public static ESkillName[] PlayerSkillSlot { get { return Player.CurSkillSlot; } }                                  // ���� ��ų ����


    // ī�޶� ����
    private CameraMgr m_cameraMgr;
    public static CameraMgr CameraMgr { get { return Inst.m_cameraMgr; } }
    public static void SetMainCamera(Camera _camera) { CameraMgr.SetMainCamera(_camera); }



    // UI ����
    private UIMgr m_uiMgr;
    public static UIMgr UIMgr { get { return Inst.m_uiMgr; } }
    public static Transform MainCanvasTrans { get { return UIMgr.MainCanvas.transform; } }                              // ���� ĵ����
    public static void SetMainCanvas(MainCanvasScript _canvas) { UIMgr.SetMainCanvas(_canvas); }                        // ������
    public static Transform IngameCanvasTrans { get { return UIMgr.IngameCanvas.transform; } }                          // �ΰ��� ĵ����
    public static void SetIngameCanvas(IngameCanvasScript _canvas) { UIMgr.SetIngameCanvas(_canvas); }                  // ������
    public static Transform ControlCanvasTrans { get { return UIMgr.ControlCanvas.transform; } }                        // ������ ĵ����
    public static void SetControlCanvas(ControlCanvasScript _canvas) { UIMgr.SetControlCanvas(_canvas); }               // ������

    public static GameObject SkillCarrierPrefab { get { return UIMgr.SkillCarrierPrefab; } }                            // ��ų ĳ���� ������
    public static GameObject RoomInfoPrefab { get { return UIMgr.RoomInfoPrefab; } }                                    // ���� �ؽ�Ʈ ������
    public static GameObject MonsterHPBarPrefab { get { return UIMgr.MonsterHPBarPrefab; } }                            // ���� ü�¹�
    public static GameObject InteractUIPrefab { get { return UIMgr.InteractUIPrefab; } }                                // ��ȣ�ۿ� ǥ�� UI
    public static GameObject EvasionUIPreafb { get { return UIMgr.EvasionTxtPrefab; } }                                 // ȸ�� ȿ�� UI
    public static Sprite GetSkillIcon(ESkillName _skill) { return UIMgr.GetSkillIcon(_skill); }                         // ��ų ������
    public static Sprite GetRewardIcon(RewardInfo _reward) {
        if (_reward.IsSkill) return UIMgr.GetSkillIcon(((SkillRewardInfo)_reward).SkillName);
        return UIMgr.GetStatIcon(((StatRewardInfo)_reward).StatName); }                                                 // ���� ������
    public static void InitiateValues() { UIMgr.InitiateValues(); }                                                     // HP,���¹̳� �ʱ� ����
    public static void SetMaxHP(int _max) { UIMgr.SetMaxHP(_max); }                                                     // �ִ� HP ����
    public static void SetCurHP(int _hp) { UIMgr.SetCurHP(_hp); }                                                       // HP ��ȭ �ø��� ȣ��
    public static void SetCurStamina(int _stamina) { UIMgr.SetCurStamina(_stamina); }                                   // ���¹̳� ���� �ø��� ȣ��
    public static void SetStaminaPer(float _per) { UIMgr.SetStaminaPer(_per); }                                         // ���¹̳� ȸ���� ǥ��
    public static void SetSkillSlot(ESkillName _skill, int _idx) 
    { Player.SetSkillSlot(_skill, _idx); UIMgr.SetSkillSlot(_skill, _idx);  }                                           // ��ų ���� (�巡�� ���)
    public static void StartShootingCoolTime(float _coolTime) { UIMgr.StartShootingCoolTime(_coolTime); }               // ���� ��Ÿ�� ����
    public static RectTransform[] SlotTrans { get { return UIMgr.SlotTrans; } }
    public static void StartSkillCoolTime(int _idx, float _coolTime) { UIMgr.StartSkillCoolTime(_idx, _coolTime); }     // ��ų ��Ÿ�� ����
    public static void PopupReward(bool _skill) { UIMgr.PopupReward(_skill); }                                          // ���� ȹ�� UI ����


    // �� �۵� ����
    private RoomMgr m_roomMgr;
    public static RoomMgr RoomMgr { get { return Inst.m_roomMgr; } }
    public static GameRoomScript CurRoom { get { return RoomMgr.CurRoom; } }                                            // �÷��̾� ���� ��ġ ��
    public static void SetCurRoom(GameRoomScript _room) { RoomMgr.SetCurRoom(_room); }                                  // ���� �� ���
    public static Vector3 DefenseObjPos { get { return RoomMgr.DefenseObjPos; } }


    // ���� ����
    private RewardMgr m_rewardMgr;
    public static RewardMgr RewardMgr { get { return Inst.m_rewardMgr; } }
    public static RewardInfo GetRandomRewardInfo() { return RewardMgr.GetRandomRewardInfo(); }                          // Ȯ���� ���� ���� ���� �̱�
    public static SkillRewardInfo GetRandomSkillInfo() { return (SkillRewardInfo)RewardMgr.GetRandomSkillInfo(); }      // ��ų ������
    public static StatRewardInfo GetRandomStatInfo() { return (StatRewardInfo)RewardMgr.GetRandomStatInfo(); }          // ���� ������
    public static void SelectReward(RewardInfo _reward) { RewardMgr.SelectReward(_reward); }                            // ���� ���� (�κ��� �߰�)


    // ��ų ������ ����
    private SkillPrefabs m_skillPrefabs;
    public static SkillPrefabs SkillPrefabs { get { return Inst.m_skillPrefabs; } }
    public static GameObject GetSkillPrefab(ESkillName _skill) { return SkillPrefabs.GetSkillPrefab(_skill); }          // ��ų ������


    // ���� ������ ����
    private MonsterPrefabs m_monsterPrefabs;
    public static MonsterPrefabs MonsterPrefabs { get { return Inst.m_monsterPrefabs; } }
    public static GameObject GetMonsterPrefab(EMonsterName _monster) { return MonsterPrefabs.GetMonsterPrefab(_monster); }  // ���� ������
    public static GameObject GroupMonsterPrefab { get { return MonsterPrefabs.GroupMonsterPrefab; } }


    // ȿ�� ������ ����
    private EffectPrefabs m_effectPrefabs;
    public static EffectPrefabs EffectPrefabs { get { return Inst.m_effectPrefabs; } }      // ȿ�� ������
    public static void CreatePlayerEffect(EPlayerEffect _effect, Vector3 _pos) { EffectPrefabs.CreatePlayerEffect(_effect, _pos); }
    public static void CreatePlayerEffect(EPlayerEffect _effect, Transform _trans) { EffectPrefabs.CreatePlayerEffect(_effect, _trans); }
    public static void CreateMonsterEffect(EMonsterEffect _effect, Vector3 _pos) { EffectPrefabs.CreateMonsterEffect(_effect, _pos); }
    public static void CreateSkillEffect(ESkillEffect _effect, Vector3 _pos) { EffectPrefabs.CreateSkillEffect(_effect, _pos); }
    public static void CreateGimicEffect(EGimicEffect _effect, Vector3 _pos) { EffectPrefabs.CreateGimicEffect(_effect, _pos); }


    // ��ų �κ��丮 ����
    private SkillInvenMgr m_skillInventory;
    public static SkillInvenMgr SkillInventory { get { return Inst.m_skillInventory; } }
    public static bool HaveSkill(ESkillName _skill) { return SkillInventory.HaveSkill(_skill); }                                // ��ų ���� ����
    public static void ObtainSkill(ESkillName _skill) { SkillInventory.ObtainSkill(_skill); UIMgr.AddSkillToInven(_skill); }    // ��ų ȹ��
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
