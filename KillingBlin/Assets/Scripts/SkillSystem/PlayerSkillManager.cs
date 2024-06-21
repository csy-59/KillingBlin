using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;

// 플레이어의 스킬을 관리하는 클래스
public class PlayerSkillManager : MonoBehaviour
{
    // 캐릭터의 스탯과 잠금 해제 가능한 능력들을 저장하는 변수들
    private int _strength, _dexterity, _intelligence, _wisdom, _charisma, _consitution; // 스탯들
    private int _doublejump, _dash, _teleport;  // 잠금 해제 가능한 능력들
    private int _skillPoints; // 스킬 포인트

    // 각 스탯의 접근자 프로퍼티
    public int Strength => _strength;
    public int Dexterity => _dexterity;
    public int Intelligence => _intelligence;
    public int Wisdom => _wisdom;
    public int Charisma => _charisma;
    public int Consitution => _consitution;

    // 각 능력의 잠금 해제 여부를 반환하는 프로퍼티
    public bool DoubleJump => _doublejump > 0;
    public bool Dash => _dash > 0;
    public bool Teleport => _teleport > 0;
    public int SkillPoints => _skillPoints; // 현재 스킬 포인트

    // 스킬 포인트가 변경될 때 호출되는 이벤트
    public UnityAction OnSkillPointsChanged;

    // 잠금 해제된 스킬 목록을 저장하는 리스트
    private List<ScriptableSkill> _unlockedSkills = new List<ScriptableSkill>();

    // 초기 설정: 스킬 포인트를 10으로 설정
    private void Awake()
    {
        _skillPoints = 10;
    }

    // 스킬 포인트를 얻는 메서드
    public void GainSkillPoint()
    {
        _skillPoints++;
        OnSkillPointsChanged?.Invoke(); // 스킬 포인트 변경 이벤트 호출
    }

    // 특정 스킬을 구매할 수 있는지 확인하는 메서드
    public bool CanAffordSkill(ScriptableSkill skill)
    {
        return _skillPoints >= skill.Cost; // 스킬 포인트가 스킬 비용보다 많은지 확인
    }

    // 스킬을 잠금 해제하는 메서드
    public void UnlockSkill(ScriptableSkill skill)
    {
        if (!CanAffordSkill(skill)) // 스킬 포인트가 부족하면 실행하지 않음
            return;
        ModifyStats(skill); // 스킬이 제공하는 스탯을 수정
        _unlockedSkills.Add(skill); // 스킬을 잠금 해제된 스킬 목록에 추가
        _skillPoints -= skill.Cost; // 스킬 비용만큼 스킬 포인트를 감소
        OnSkillPointsChanged?.Invoke(); // 스킬 포인트 변경 이벤트 호출
    }

    // 스킬에 따른 스탯을 수정하는 메서드
    private void ModifyStats(ScriptableSkill skill)
    {
        foreach (UpgradeData data in skill.UpgradeData)
        {
            // 각 스탯 타입에 따라 해당 스탯을 수정
            switch (data.StatType)
            {
                case StatTypes.Strength:
                    ModifyStat(ref _strength, data);
                    break;
                case StatTypes.Dexterity:
                    ModifyStat(ref _dexterity, data);
                    break;
                case StatTypes.Intelligence:
                    ModifyStat(ref _intelligence, data);
                    break;
                case StatTypes.Wisdom:
                    ModifyStat(ref _wisdom, data);
                    break;
                case StatTypes.Charisma:
                    ModifyStat(ref _charisma, data);
                    break;
                case StatTypes.Constitution:
                    ModifyStat(ref _consitution, data);
                    break;
                case StatTypes.DoubleJump:
                    ModifyStat(ref _doublejump, data);
                    break;
                case StatTypes.Dash:
                    ModifyStat(ref _dash, data);
                    break;
                case StatTypes.Teleport:
                    ModifyStat(ref _teleport, data);
                    break;
                default:
                    throw new ArguementOutofRangeException(); // 범위를 벗어난 경우 예외 처리
            }
        }
    }

    // 특정 스킬이 잠금 해제되었는지 확인하는 메서드
    public bool IsSkillUnlocked(ScriptableSkill skill)
    {
        return _unlockedSkills.Contains(skill);
    }

    // 특정 스킬의 전제 조건이 충족되었는지 확인하는 메서드
    public bool PreReqsMet(ScriptableSkill skill)
    {
        return skill.SkillPrerequisites.Count == 0 || skill.SkillPrerequisites.All(_unlockedSkills.Contains);
    }

    // 스탯을 수정하는 메서드
    private void ModifyStat(ref int stat, UpgradeData data)
    {
        // 퍼센트 증가인지 확인하여 스탯을 수정
        if (data.IsPercentage)
            stat += (int)(stat * (data.SkillIncreaseAmount / 100f));
        else
            stat += data.SkillIncreaseAmount;
    }


}

// 범위를 벗어난 경우에 대한 사용자 정의 예외 클래스
[Serializable]
internal class ArguementOutofRangeException : Exception
{
    public ArguementOutofRangeException()
    {
    }

    public ArguementOutofRangeException(string message) : base(message)
    {
    }

    public ArguementOutofRangeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ArguementOutofRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
