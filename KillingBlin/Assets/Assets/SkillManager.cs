using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    

    float meleeCooldown = 0.5f; // 근접 공격 쿨타임
    float rangedCooldown = 1.0f; // 원거리 공격 쿨타임

    float meleeTimer;
    float rangedTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        // 근접 공격 실행
        if (Input.GetKeyDown(KeyCode.Z) && meleeTimer <= 0)
        {
            PerformMeleeAttack();
            meleeTimer = meleeCooldown;
        }

        // 원거리 공격 실행
        if (Input.GetKeyDown(KeyCode.X) && rangedTimer <= 0)
        {
            PerformRangedAttack();
            rangedTimer = rangedCooldown;
        }

        // 쿨타임 감소
        meleeTimer -= Time.deltaTime;
        rangedTimer -= Time.deltaTime;
    }

    private void PerformMeleeAttack()
    {
        // 근접 공격 로직 구현
        Debug.Log("근접 공격 실행");
        // ...
    }

    private void PerformRangedAttack()
    {
        // 원거리 공격 로직 구현
        Debug.Log("원거리 공격 실행");
        // ...
    }
}
