using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;

// �÷��̾��� ��ų�� �����ϴ� Ŭ����
public class PlayerSkillManager : MonoBehaviour
{
    // ĳ������ ���Ȱ� ��� ���� ������ �ɷµ��� �����ϴ� ������
    private int _strength, _dexterity, _intelligence, _wisdom, _charisma, _consitution; // ���ȵ�
    private int _doublejump, _dash, _teleport;  // ��� ���� ������ �ɷµ�
    private int _skillPoints; // ��ų ����Ʈ

    // �� ������ ������ ������Ƽ
    public int Strength => _strength;
    public int Dexterity => _dexterity;
    public int Intelligence => _intelligence;
    public int Wisdom => _wisdom;
    public int Charisma => _charisma;
    public int Consitution => _consitution;

    // �� �ɷ��� ��� ���� ���θ� ��ȯ�ϴ� ������Ƽ
    public bool DoubleJump => _doublejump > 0;
    public bool Dash => _dash > 0;
    public bool Teleport => _teleport > 0;
    public int SkillPoints => _skillPoints; // ���� ��ų ����Ʈ

    // ��ų ����Ʈ�� ����� �� ȣ��Ǵ� �̺�Ʈ
    public UnityAction OnSkillPointsChanged;

    // ��� ������ ��ų ����� �����ϴ� ����Ʈ
    private List<ScriptableSkill> _unlockedSkills = new List<ScriptableSkill>();

    // �ʱ� ����: ��ų ����Ʈ�� 10���� ����
    private void Awake()
    {
        _skillPoints = 10;
    }

    // ��ų ����Ʈ�� ��� �޼���
    public void GainSkillPoint()
    {
        _skillPoints++;
        OnSkillPointsChanged?.Invoke(); // ��ų ����Ʈ ���� �̺�Ʈ ȣ��
    }

    // Ư�� ��ų�� ������ �� �ִ��� Ȯ���ϴ� �޼���
    public bool CanAffordSkill(ScriptableSkill skill)
    {
        return _skillPoints >= skill.Cost; // ��ų ����Ʈ�� ��ų ��뺸�� ������ Ȯ��
    }

    // ��ų�� ��� �����ϴ� �޼���
    public void UnlockSkill(ScriptableSkill skill)
    {
        if (!CanAffordSkill(skill)) // ��ų ����Ʈ�� �����ϸ� �������� ����
            return;
        ModifyStats(skill); // ��ų�� �����ϴ� ������ ����
        _unlockedSkills.Add(skill); // ��ų�� ��� ������ ��ų ��Ͽ� �߰�
        _skillPoints -= skill.Cost; // ��ų ��븸ŭ ��ų ����Ʈ�� ����
        OnSkillPointsChanged?.Invoke(); // ��ų ����Ʈ ���� �̺�Ʈ ȣ��
    }

    // ��ų�� ���� ������ �����ϴ� �޼���
    private void ModifyStats(ScriptableSkill skill)
    {
        foreach (UpgradeData data in skill.UpgradeData)
        {
            // �� ���� Ÿ�Կ� ���� �ش� ������ ����
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
                    throw new ArguementOutofRangeException(); // ������ ��� ��� ���� ó��
            }
        }
    }

    // Ư�� ��ų�� ��� �����Ǿ����� Ȯ���ϴ� �޼���
    public bool IsSkillUnlocked(ScriptableSkill skill)
    {
        return _unlockedSkills.Contains(skill);
    }

    // Ư�� ��ų�� ���� ������ �����Ǿ����� Ȯ���ϴ� �޼���
    public bool PreReqsMet(ScriptableSkill skill)
    {
        return skill.SkillPrerequisites.Count == 0 || skill.SkillPrerequisites.All(_unlockedSkills.Contains);
    }

    // ������ �����ϴ� �޼���
    private void ModifyStat(ref int stat, UpgradeData data)
    {
        // �ۼ�Ʈ �������� Ȯ���Ͽ� ������ ����
        if (data.IsPercentage)
            stat += (int)(stat * (data.SkillIncreaseAmount / 100f));
        else
            stat += data.SkillIncreaseAmount;
    }


}

// ������ ��� ��쿡 ���� ����� ���� ���� Ŭ����
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
