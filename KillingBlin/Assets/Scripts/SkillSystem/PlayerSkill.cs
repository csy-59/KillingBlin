using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public enum SkillType
    {
        Q,
        E,
        R,
        MAX
    }

    private SkillBase[] Skill = new SkillBase[(int)SkillType.MAX];

    /// <summary>
    /// ��ų ���
    /// </summary>
    /// <param name="skillType">� ��ų�� �������</param>
    public void SkillUse(SkillType skillType)
    {
        if (skillType == SkillType.MAX)
            return;

        if (Skill[(int)skillType] == null)
            return;

        if (Skill[(int)skillType].UseSkill() == false) // ������ ������ ��ų ��� �Ұ�
            return;
    }

    public void SetSkill(int index, SkillBase skill)
    {
        Skill[index] = skill;
        skill.SetPlayerSkill(this);
    }
}
