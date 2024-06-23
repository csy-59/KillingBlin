using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class SkillBase : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    public Sprite SkillImage { get => sprite; set => sprite = value; }

    [SerializeField] private int unlockLevel;
    public int UnlockLevel { get => unlockLevel; private set => unlockLevel = value; }

    [SerializeField] private bool isSkillUnLocked;
    public bool IsSkillUnlocked { get => isSkillUnLocked; set => isSkillUnLocked = value; }

    [SerializeField] private float maxCoolTime = 5;
    private float coolTime_Elapsed = 0f;
    private bool isCoolTime = false;

    protected PlayerSkill playerSkill;
    protected PlayerController playerController;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 스킬 사용
    /// </summary>
    /// <returns>스킬 가능 여부</returns>
    public bool UseSkill()
    {
        // 쿨타임일 경우 사용 불가
        if (isCoolTime == false) 
            return false;

        Skill();
        return true;
    }

    public abstract void Skill();

    protected IEnumerator CoolTime()
    {
        isCoolTime = true;

        coolTime_Elapsed = 0f;
        while(coolTime_Elapsed < maxCoolTime)
        {
            coolTime_Elapsed += Time.deltaTime;
            yield return null;
        }

        isCoolTime = false;
    }

    public void SetPlayerSkill(PlayerSkill pSkill)
    {
        playerSkill = pSkill;
        playerController = pSkill.GetComponent<PlayerController>();
    }
}
