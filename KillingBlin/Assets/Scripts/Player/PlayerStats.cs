using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int level = 1; // 초기 레벨
    public int currentXP = 0; // 현재 경험치
    public int xpToNextLevel = 100; // 다음 레벨까지 필요한 경험치
    public int xpIncreasePerLevel = 50; // 레벨업 할 때마다 증가하는 경험치

    public delegate void _onLevelUp(int level);
    public event _onLevelUp OnLevelUp;

    void Start()
    {
        // 초기화 작업이 필요하면 여기에 작성합니다.
    }

    void Update()
    {
        // 테스트를 위해 경험치를 증가시키는 임시 코드
        if (Input.GetKeyDown(KeyCode.L))
        {
            GainExperience(10); // L키를 누를 때마다 10의 경험치 획득
        }
    }
    private void LevelUp()
    {
        level++;
        xpToNextLevel += xpIncreasePerLevel;
        // 레벨업 시 플레이어의 능력치를 증가시키는 코드 추가
        IncreasePlayerStats();
        Debug.Log("Level Up! New Level: " + level);
        //OnLevelUp.Invoke();
    }

    private void IncreasePlayerStats()
    {
        // 예시: 체력과 공격력을 증가시키는 코드
        // 실제로는 PlayerController 또는 Health 등 다른 컴포넌트와 연동 필요
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.maxHealth += 10; // 최대 체력 증가
            playerController.attackDamage += 2; // 공격력 증가
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
