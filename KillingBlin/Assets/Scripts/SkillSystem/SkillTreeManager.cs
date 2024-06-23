using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    [SerializeField] private SkillBase[] skills;

    private void CheckSkillUnlock(int level)
    {
        foreach(var s in skills)
        {
            s.IsSkillUnlocked = (s.UnlockLevel <= level);
        }
    }

    private SkillBase GetSkill(int index)
    {
        return skills[index];
    }


}
