using Defines.FSMDefines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkillThird : SkillBase
{
    Animator animator;
    [SerializeField] float radios = 0.3f;

    LayerMask enemeyLayer = LayerMask.NameToLayer("Enemy");

    public override void Init()
    {
        animator = GetComponent<Animator>();
    }

    public override void Skill()
    {
        animator.SetTrigger(AnimationID.Melee_Skill);

    }

    /// <summary>
    /// 공격하는 코드, AnimationEvent로 호출됨
    /// </summary>
    public void OnAttack()
    {
        var colliders = Physics2D.OverlapCircleAll(Vector2.zero, radios, enemeyLayer);
        foreach (var collider in colliders)
        {
            // 공격
            MonsterBase mb = collider.GetComponent<MonsterBase>();
            mb.TakeDamage(playerController.Status.Attack);
        }
    }

    public void OnAttackEnd()
    {
        StartCoroutine(CoolTime());
    }
}
