using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int level = 1; // �ʱ� ����
    public int currentXP = 0; // ���� ����ġ
    public int xpToNextLevel = 100; // ���� �������� �ʿ��� ����ġ
    public int xpIncreasePerLevel = 50; // ������ �� ������ �����ϴ� ����ġ

    public delegate void _onLevelUp(int level);
    public event _onLevelUp OnLevelUp;

    void Start()
    {
        // �ʱ�ȭ �۾��� �ʿ��ϸ� ���⿡ �ۼ��մϴ�.
    }

    void Update()
    {
        // �׽�Ʈ�� ���� ����ġ�� ������Ű�� �ӽ� �ڵ�
        if (Input.GetKeyDown(KeyCode.L))
        {
            GainExperience(10); // LŰ�� ���� ������ 10�� ����ġ ȹ��
        }
    }
    private void LevelUp()
    {
        level++;
        xpToNextLevel += xpIncreasePerLevel;
        // ������ �� �÷��̾��� �ɷ�ġ�� ������Ű�� �ڵ� �߰�
        IncreasePlayerStats();
        Debug.Log("Level Up! New Level: " + level);
        //OnLevelUp.Invoke();
    }

    private void IncreasePlayerStats()
    {
        // ����: ü�°� ���ݷ��� ������Ű�� �ڵ�
        // �����δ� PlayerController �Ǵ� Health �� �ٸ� ������Ʈ�� ���� �ʿ�
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.maxHealth += 10; // �ִ� ü�� ����
            playerController.attackDamage += 2; // ���ݷ� ����
        }
    }

    public void GainExperience(int amount)
    {
        currentXP += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
    }
}
