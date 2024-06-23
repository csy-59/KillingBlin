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
    /// �ʱ�ȭ
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// ��ų ���
    /// </summary>
    /// <returns>��ų ���� ����</returns>
    public bool UseSkill()
    {
        // ��Ÿ���� ��� ��� �Ұ�
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
