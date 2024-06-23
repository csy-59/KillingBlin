using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoSingleton<SkillTreeManager>
{
    [SerializeField] private SkillBase[] skills;

    private void CheckSkillUnlock(int level)
    {
        foreach(var s in skills)
        {
            s.IsSkillUnlocked = (s.UnlockLevel <= level);
        }
    }

    public SkillBase GetSkill(int index)
    {
        return skills[index];
    }


}
