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
    /// 스킬 사용
    /// </summary>
    /// <param name="skillType">어떤 스킬을 사용할지</param>
    public void SkillUse(SkillType skillType)
    {
        if (skillType == SkillType.MAX)
            return;

        if (Skill[(int)skillType] == null)
            return;

        if (Skill[(int)skillType].UseSkill() == false) // 모종의 이유로 스킬 사용 불가
            return;
    }

    public void SetSkill(int index, SkillBase skill)
    {
        Skill[index] = skill;
        skill.SetPlayerSkill(this);
    }
}
