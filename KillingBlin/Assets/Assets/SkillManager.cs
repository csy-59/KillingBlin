using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    

    float meleeCooldown = 0.5f; // ���� ���� ��Ÿ��
    float rangedCooldown = 1.0f; // ���Ÿ� ���� ��Ÿ��

    float meleeTimer;
    float rangedTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        // ���� ���� ����
        if (Input.GetKeyDown(KeyCode.Z) && meleeTimer <= 0)
        {
            PerformMeleeAttack();
            meleeTimer = meleeCooldown;
        }

        // ���Ÿ� ���� ����
        if (Input.GetKeyDown(KeyCode.X) && rangedTimer <= 0)
        {
            PerformRangedAttack();
            rangedTimer = rangedCooldown;
        }

        // ��Ÿ�� ����
        meleeTimer -= Time.deltaTime;
        rangedTimer -= Time.deltaTime;
    }

    private void PerformMeleeAttack()
    {
        // ���� ���� ���� ����
        Debug.Log("���� ���� ����");
        // ...
    }

    private void PerformRangedAttack()
    {
        // ���Ÿ� ���� ���� ����
        Debug.Log("���Ÿ� ���� ����");
        // ...
    }
}
