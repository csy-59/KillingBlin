using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.FSMDefines;

public class MeleeSkillOne : SkillBase
{
    private Animator animator;
    private PlayerController controller;
    [SerializeField] float radios = 0.3f;
    [SerializeField] Vector2 attackPosition;

    LayerMask enemeyLayer = LayerMask.NameToLayer("Enemy");

    public override void Init()
    {
        animator = GetComponent<Animator>();
    }

    public override void Skill()
    {
        animator.SetTrigger(AnimationID.Melee_Attack);

    }

    /// <summary>
    /// �����ϴ� �ڵ�, AnimationEvent�� ȣ���
    /// </summary>
    public void OnAttack()
    {
        Collider2D collider = Physics2D.OverlapCircle(attackPosition, radios, enemeyLayer);
        if (collider != null)
        {
            // ����
            MonsterBase mb = collider.GetComponent<MonsterBase>();
            mb.TakeDamage(playerController.Status.Attack);
        }
    }

    public void OnAttackEnd()
    {
        StartCoroutine(CoolTime());
    }
}